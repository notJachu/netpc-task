import React, { useState, useEffect } from 'react';
import { useParams, useNavigate, Link } from 'react-router-dom';

export default function EditContact() {
    const { id } = useParams();
    const navigate = useNavigate();
    
    const [categories, setCategories] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [saving, setSaving] = useState(false);

    const [formData, setFormData] = useState({
        firstName: '',
        lastName: '',
        email: '',
        phone: '',
        birthDate: '',
        categoryId: '',
        subcategoryId: '',
        customSubcategory: '',
        password: ''
    });

    useEffect(() => {
        const fetchData = async () => {
            try {
                const [contactRes, catsRes] = await Promise.all([
                    fetch(`http://localhost:5158/Contacts/${id}`, { credentials: 'include' }),
                    fetch(`http://localhost:5158/Contacts/categories`)
                ]);

                if (contactRes.status === 401 || contactRes.status === 403) {
                    throw new Error('Unauthorized.');
                }
                
                if (!contactRes.ok || !catsRes.ok) {
                    throw new Error('Error fetching data.');
                }

                const contactData = await contactRes.json();
                const catsData = await catsRes.json();

                setCategories(catsData);
                setFormData({
                    firstName: contactData.firstName || '',
                    lastName: contactData.lastName || '',
                    email: contactData.email || '',
                    phone: contactData.phone || '',
                    birthDate: contactData.birthDate || '',
                    categoryId: contactData.categoryId || '',
                    subcategoryId: contactData.subcategoryId || '',
                    customSubcategory: contactData.customSubcategory || '',
                    password: ''
                });
                setLoading(false);
            } catch (err) {
                setError(err.message);
                setLoading(false);
            }
        };

        fetchData();
    }, [id]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData(prev => ({ ...prev, [name]: value }));
    };

    const handleCategoryChange = (e) => {
        const newCatId = e.target.value;
        setFormData(prev => ({
            ...prev,
            categoryId: newCatId,
            subcategoryId: '',
            customSubcategory: ''
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setSaving(true);
        setError(null);

        const payload = {
            firstName: formData.firstName,
            lastName: formData.lastName,
            email: formData.email,
            phone: formData.phone,
            birthDate: formData.birthDate,
            categoryId: parseInt(formData.categoryId, 10),
            subcategoryId: formData.subcategoryId ? parseInt(formData.subcategoryId, 10) : null,
            customSubcategory: formData.customSubcategory || null,
            password: formData.password || undefined
        };

        try {
            const response = await fetch(`http://localhost:5158/Contacts/${id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                credentials: 'include',
                body: JSON.stringify(payload)
            });

            if (response.ok) {
                navigate(`/contacts/${id}`);
            } else {
                const errText = await response.text();
                setError(errText || 'Error saving contact.');
            }
        } catch (err) {
            setError(err.message);
        } finally {
            setSaving(false);
        }
    };

    if (loading) return <div className="mt-5 text-center">Loading data</div>;
    if (error) return <div className="mt-5 alert alert-danger">Error: {error}</div>;

    const selectedCategory = categories.find(c => c.id.toString() === formData.categoryId.toString());
    const isOtherCategory = selectedCategory && (selectedCategory.name.toLowerCase() === 'inny' || selectedCategory.subcategories.length === 0);

    return (
        <div className="mt-4">
            <h2>Edit contact</h2>
            <div className="card mt-3 shadow-sm">
                <div className="card-body">
                    <form onSubmit={handleSubmit}>
                        <div className="row mb-3">
                            <div className="col-md-6">
                                <label className="form-label">Name</label>
                                <input type="text" className="form-control" name="firstName" value={formData.firstName} onChange={handleChange} required maxLength="50" />
                            </div>
                            <div className="col-md-6">
                                <label className="form-label">Surname</label>
                                <input type="text" className="form-control" name="lastName" value={formData.lastName} onChange={handleChange} required maxLength="50" />
                            </div>
                        </div>
                        
                        <div className="row mb-3">
                            <div className="col-md-6">
                                <label className="form-label">Email</label>
                                <input type="email" className="form-control" name="email" value={formData.email} onChange={handleChange} required />
                            </div>
                            <div className="col-md-6">
                                <label className="form-label">Phone</label>
                                <input type="text" className="form-control" name="phone" value={formData.phone} onChange={handleChange} required maxLength="20" />
                            </div>
                        </div>

                        <div className="row mb-3">
                            <div className="col-md-6">
                                <label className="form-label">Birth date</label>
                                <input type="date" className="form-control" name="birthDate" value={formData.birthDate} onChange={handleChange} required />
                            </div>
                            <div className="col-md-6">
                                <label className="form-label">New password (leave empty to not change)</label>
                                <input type="password" className="form-control" name="password" value={formData.password} onChange={handleChange} />
                            </div>
                        </div>

                        <div className="row mb-4">
                            <div className="col-md-6">
                                <label className="form-label">Category</label>
                                <select className="form-select" name="categoryId" value={formData.categoryId} onChange={handleCategoryChange} required>
                                    <option value="" disabled>Pick a category...</option>
                                    {categories.map(c => (
                                        <option key={c.id} value={c.id}>{c.name}</option>
                                    ))}
                                </select>
                            </div>
                            <div className="col-md-6">
                                {selectedCategory && (
                                    <>
                                        <label className="form-label">Subcategory</label>
                                        {isOtherCategory ? (
                                            <input 
                                                type="text" 
                                                className="form-control" 
                                                name="customSubcategory" 
                                                value={formData.customSubcategory} 
                                                onChange={handleChange} 
                                                placeholder="Enter subcategory" 
                                                required 
                                                maxLength="255" 
                                            />
                                        ) : (
                                            <select 
                                                className="form-select" 
                                                name="subcategoryId" 
                                                value={formData.subcategoryId || ''} 
                                                onChange={handleChange} 
                                                required
                                            >
                                                <option value="" disabled>Pick a subcategory...</option>
                                                {selectedCategory.subcategories.map(s => (
                                                    <option key={s.id} value={s.id}>{s.name}</option>
                                                ))}
                                            </select>
                                        )}
                                    </>
                                )}
                            </div>
                        </div>

                        <div className="d-flex justify-content-between">
                            <Link to={`/contacts/${id}`} className="btn btn-secondary">Discard</Link>
                            <button type="submit" className="btn btn-primary" disabled={saving}>
                                {saving ? 'Saving...' : 'Save changes'}
                            </button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    );
}