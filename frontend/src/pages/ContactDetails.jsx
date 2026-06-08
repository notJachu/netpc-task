import React, { useState, useEffect } from 'react';
import { useParams, Link, useNavigate } from 'react-router-dom';

export default function ContactDetails() {
    const { id } = useParams();
    const navigate = useNavigate();
    const [contact, setContact] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [deleting, setDeleting] = useState(false);

    useEffect(() => {
        fetch(`http://localhost:5158/Contacts/${id}`, {
            credentials: 'include' 
        })
            .then(async response => {
                if (response.status === 401 || response.status === 403) {
                    throw new Error('Unauthorized.');
                }
                if (!response.ok) {
                    if (response.status === 404) throw new Error('Contact not found.');
                    throw new Error('Unhandled error: ' + response.status);
                }
                return response.json();
            })
            .then(data => {
                setContact(data);
                setLoading(false);
            })
            .catch(err => {
                setError(err.message);
                setLoading(false);
            });
    }, [id]);

    const handleDelete = async () => {
        if (!window.confirm('Are you sure you want to delete this contact?')) {
            return;
        }

        setDeleting(true);
        try {
            const response = await fetch(`http://localhost:5158/Contacts/${id}`, {
                method: 'DELETE',
                credentials: 'include'
            });

            if (response.ok) {
                navigate('/contacts');
            } else {
                const errText = await response.text();
                setError(errText || 'An error occurred while deleting.');
                setDeleting(false);
            }
        } catch (err) {
            setError(err.message);
            setDeleting(false);
        }
    };

    if (loading) return <div className="mt-5 text-center">Loading details</div>;
    if (error) return <div className="mt-5 alert alert-danger">Error {error}</div>;
    if (!contact) return <div className="mt-5 alert alert-warning">Nothing to display</div>;

    const isLoggedIn = !!localStorage.getItem('isLoggedIn');

    return (
        <div className="mt-4">
            <h2>Contact details</h2>
            <div className="card mt-3 shadow-sm">
                <div className="card-body p-0">
                    <table className="table table-bordered mb-0">
                        <tbody>
                            <tr>
                                <th className="w-25 bg-light px-3 py-2">Name</th>
                                <td className="px-3 py-2">{contact.firstName}</td>
                            </tr>
                            <tr>
                                <th className="bg-light px-3 py-2">Surname</th>
                                <td className="px-3 py-2">{contact.lastName}</td>
                            </tr>
                            <tr>
                                <th className="bg-light px-3 py-2">Email</th>
                                <td className="px-3 py-2">{contact.email}</td>
                            </tr>
                            <tr>
                                <th className="bg-light px-3 py-2">Phone</th>
                                <td className="px-3 py-2">{contact.phone}</td>
                            </tr>
                            <tr>
                                <th className="bg-light px-3 py-2">Birth date</th>
                                <td className="px-3 py-2">{contact.birthDate}</td>
                            </tr>
                            <tr>
                                <th className="bg-light px-3 py-2">Category</th>
                                <td className="px-3 py-2">{contact.category || '-'}</td>
                            </tr>
                            <tr>
                                <th className="bg-light px-3 py-2">Subcategory</th>
                                <td className="px-3 py-2">{contact.subcategory || '-'}</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div className="card-footer d-flex justify-content-between bg-white">
                    <Link to="/contacts" className="btn btn-secondary">Back to list</Link>
                    <div>
                        {isLoggedIn && (
                            <button onClick={handleDelete} className="btn btn-danger me-2" disabled={deleting}>
                                {deleting ? 'Deleting...' : 'Delete'}
                            </button>
                        )}
                        {isLoggedIn && (
                            <Link to={`/contacts/${id}/edit`} className="btn btn-primary">Edit</Link>
                        )}
                    </div>
                </div>
            </div>
        </div>
    );
}