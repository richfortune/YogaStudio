import { Bell, User } from 'lucide-react';
import { Link, useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import './Header.css';

export function Header() {
    const navigate = useNavigate();
    const { user, setUserRole } = useAuth();

    return (
        <header className="main-header glass-panel">
            <div className="header-brand">
                <Link to="/">
                    <h1>YogaStudio</h1>
                </Link>
            </div>
            <div className="header-actions" style={{ display: 'flex', alignItems: 'center', gap: '1rem' }}>
                <select 
                    value={user.role} 
                    onChange={(e) => setUserRole(e.target.value as 'Admin' | 'Student')}
                    style={{
                        padding: '4px 8px',
                        borderRadius: 'var(--radius-sm)',
                        border: '1px solid var(--glass-border)',
                        background: 'var(--color-surface)',
                        color: 'var(--color-primary)',
                        cursor: 'pointer',
                        fontWeight: 'bold'
                    }}
                >
                    <option value="Admin">Admin View</option>
                    <option value="Student">Student View</option>
                </select>
                <button className="btn btn-icon">
                    <Bell size={20} />
                </button>
                <button className="btn btn-icon" onClick={() => navigate('/profile')}>
                    <User size={20} />
                </button>
            </div>
        </header>
    );
}
