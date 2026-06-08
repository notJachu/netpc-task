import './App.css'
import {BrowserRouter, Link, Route, Routes} from "react-router-dom";
import ProtectedRoute from "./components/ProtectedRoute.jsx";
import Home from './pages/Home.jsx';
import Contacts from './pages/Contacts.jsx';
import Login from './pages/Login.jsx';
import ContactDetails from './pages/ContactDetails.jsx';
import NotFound from './pages/NotFound.jsx';

function App() {

  return (
    <>
      <BrowserRouter>
        <nav className="navbar navbar-expand-lg navbar-dark bg-dark px-3">
          <Link to="/" className="navbar-brand">Contacts App</Link>
          <div className="collapse navbar-collapse">
            <ul className="navbar-nav">
                <li className="nav-item">
                    <Link to="/contacts" className="nav-link">Contacts</Link>
                </li>
                <li className="nav-item">
                    <Link to="/login" className="nav-link">Login</Link>
                </li>
            </ul>
          </div>
        </nav>
        
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
            <Route path="*" element={<NotFound />} />
          </Routes>
        </div>
      </BrowserRouter>
    </>
  )
}

export default App
