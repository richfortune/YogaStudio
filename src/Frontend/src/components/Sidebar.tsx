import { Calendar, LayoutDashboard, CalendarDays, Users } from 'lucide-react';
import { NavLink } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import './Sidebar.css';

export function Sidebar() {
    const { isAdmin } = useAuth();
    
    return (
        <aside className="main-sidebar glass-panel">
            <nav className="sidebar-nav">
                <NavLink to="/" className={({ isActive }) => `nav-item ${isActive ? 'active' : ''}`}>
                    <LayoutDashboard size={20} />
                    <span>Bacheca</span>
                </NavLink>
                <NavLink to="/classes" className={({ isActive }) => `nav-item ${isActive ? 'active' : ''}`}>
                    <CalendarDays size={20} />
                    <span>Lezioni</span>
                </NavLink>
                <NavLink to="/bookings" className={({ isActive }) => `nav-item ${isActive ? 'active' : ''}`}>
                    <Calendar size={20} />
                    <span>Le mie prenotazioni</span>
                </NavLink>
                {isAdmin && (
                    <NavLink to="/users" className={({ isActive }) => `nav-item ${isActive ? 'active' : ''}`}>
                        <Users size={20} />
                        <span>Gestione Utenti</span>
                    </NavLink>
                )}
            </nav>
        </aside>
    );
}
