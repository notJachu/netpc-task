import './App.css'
import {BrowserRouter, Link, Route, Routes, useNavigate} from "react-router-dom";
import ProtectedRoute from "./components/ProtectedRoute.jsx";
import Home from './pages/Home.jsx';
import Contacts from './pages/Contacts.jsx';
import Login from './pages/Login.jsx';
import ContactDetails from './pages/ContactDetails.jsx';
import NotFound from './pages/NotFound.jsx';
import EditContact from "./pages/EditContact.jsx";

function Navigation() {
    const isLoggedIn = !!localStorage.getItem('isLoggedIn');
    const navigate = useNavigate();

    const handleLogout = () => {
        localStorage.removeItem('isLoggedIn');
        navigate('/login');
    };

    return (
        <nav className="navbar navbar-expand-lg navbar-dark bg-dark px-3">
          <Link to="/" className="navbar-brand">DEMO APP</Link>
          <div className="collapse navbar-collapse">
            <ul className="navbar-nav me-auto">
                <li className="nav-item">
                    <Link to="/contacts" className="nav-link">Contacts</Link>
                </li>
            </ul>
            <ul className="navbar-nav">
                {isLoggedIn ? (
                    <li className="nav-item">
                        <button onClick={handleLogout} className="btn btn-link nav-link">Log out</button>
                    </li>
                ) : (
                    <li className="nav-item">
                        <Link to="/login" className="nav-link">Log in</Link>
                    </li>
                )}
            </ul>
          </div>
        </nav>
    );
}

function App() {

  return (
    <>
      <BrowserRouter>
          <Navigation />
        <div className="container mt-4">
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/contacts" element={<Contacts />} />
            <Route path="/login" element={<Login />} />
            <Route
              path="/contacts/:id"
              element={
                <ProtectedRoute>
                  <ContactDetails />
                </ProtectedRoute>
              }
            />
            <Route
              path="/contacts/:id/edit"
              element={
                <ProtectedRoute>
                  <EditContact />
                </ProtectedRoute>
              }
            />
            <Route path="*" element={<NotFound />} />
          </Routes>
        </div>
      </BrowserRouter>
    </>
  )
}

export default App
