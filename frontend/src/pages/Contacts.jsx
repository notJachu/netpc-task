import React, {useState, useEffect} from 'react';
import {Link} from 'react-router-dom';

export default function Contacts() {
    const [contacts, setContacts] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        fetch('http://localhost:5158/Contacts/list')
            .then(response => {
                if (!response.ok) {
                    throw new Error('Request failed with status ' + response.status);
                }
                return response.json();
            })
            .then(data => {
                setContacts(data);
                setLoading(false);
            })
            .catch(err => {
                setError(err.message);
                setLoading(false);
            });
    }, []);

    if (loading) return <div className="mt-5 text-center">Loading contacts...</div>;
    if (error) return <div className="mt-5 alert alert-danger">Error: {error}</div>;

    const isLoggedIn = !!localStorage.getItem('isLoggedIn');

    return (
        <div className="mt-4">
            <h2>Contact List</h2>
            <table className="table table-striped table-hover mt-3">
                <thead className="table-dark">
                    <tr>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>Email</th>
                        <th>Phone</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {contacts.length === 0 ? (
                        <tr><td colSpan="5" className="text-center">No contacts found.</td></tr>
                    ) : (
                        contacts.map(contact => (
                            <tr key={contact.id}>
                                <td>{contact.firstName}</td>
                                <td>{contact.lastName}</td>
                                <td>{contact.email}</td>
                                <td>{contact.phone}</td>
                                <td>
                                    <Link to={`/contacts/${contact.id}`} className="btn btn-sm btn-primary">
                                        Details
                                    </Link>
                                </td>
                            </tr>
                        ))
                    )}
                </tbody>
            </table>
            {isLoggedIn && (
                <div className="mt-3">
                    <Link to="/contacts/add" className="btn btn-success">Add New Contact</Link>
                </div>
            )}
        </div>
    );
    }