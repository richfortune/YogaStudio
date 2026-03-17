import { useEffect, useState } from 'react';
import { ApiHelper } from '../services/apiClient';
import type { UserDto } from '../types/models';
import { Mail, Phone, ShieldCheck, User as UserIcon } from 'lucide-react';
import { useAuth } from '../context/AuthContext';
import { Navigate } from 'react-router-dom';

export function UsersList() {
    const { isAdmin } = useAuth();
    const [users, setUsers] = useState<UserDto[]>([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        if (!isAdmin) return;
        ApiHelper.get<UserDto[]>('/Users')
            .then(data => {
                setUsers(data);
                setLoading(false);
            })
            .catch(err => {
                console.error(err);
                setLoading(false);
            });
    }, [isAdmin]);

    if (!isAdmin) {
        return <Navigate to="/" replace />;
    }

    if (loading) {
        return (
            <div className="loading-state">
                <div className="animate-spin loader-circle" />
                <p>Caricamento utenti...</p>
            </div>
        );
    }

    return (
        <div className="schedule-container">
            <header className="page-header">
                <h2>Elenco Utenti</h2>
                <p className="text-muted">Gestisci gli utenti registrati alla piattaforma.</p>
            </header>

            <div className="glass-panel" style={{ overflowX: 'auto', padding: '0' }}>
                <table style={{ width: '100%', borderCollapse: 'collapse', textAlign: 'left' }}>
                    <thead>
                        <tr style={{ borderBottom: '1px solid var(--glass-border)', background: 'rgba(255,255,255,0.02)' }}>
                            <th style={{ padding: '1rem' }}>Nome</th>
                            <th style={{ padding: '1rem' }}>Email</th>
                            <th style={{ padding: '1rem' }}>Telefono</th>
                            <th style={{ padding: '1rem' }}>Stato</th>
                            <th style={{ padding: '1rem' }}>Ruolo</th>
                        </tr>
                    </thead>
                    <tbody>
                        {users.map(u => (
                            <tr key={u.id} style={{ borderBottom: '1px solid var(--glass-border)' }}>
                                <td style={{ padding: '1rem', fontWeight: 500, display: 'flex', alignItems: 'center', gap: '0.5rem' }}>
                                    {u.id.startsWith('1111') ? <ShieldCheck size={18} color="var(--color-primary)" /> : <UserIcon size={18} className="text-muted" />}
                                    {u.firstName} {u.lastName}
                                </td>
                                <td style={{ padding: '1rem', color: 'var(--color-text-muted)' }}>
                                    <div style={{ display: 'flex', alignItems: 'center', gap: '0.5rem' }}>
                                        <Mail size={14} /> {u.email}
                                    </div>
                                </td>
                                <td style={{ padding: '1rem', color: 'var(--color-text-muted)' }}>
                                    <div style={{ display: 'flex', alignItems: 'center', gap: '0.5rem' }}>
                                        <Phone size={14} /> {u.phone || 'N/A'}
                                    </div>
                                </td>
                                <td style={{ padding: '1rem' }}>
                                    {u.isActive ? (
                                        <span style={{ background: 'var(--color-success)', color: 'white', padding: '2px 8px', borderRadius: '12px', fontSize: '0.75rem' }}>Attivo</span>
                                    ) : (
                                        <span style={{ background: 'var(--color-danger)', color: 'white', padding: '2px 8px', borderRadius: '12px', fontSize: '0.75rem' }}>Inattivo</span>
                                    )}
                                </td>
                                <td style={{ padding: '1rem', color: 'var(--color-text-muted)', fontSize: '0.9rem' }}>
                                    {u.id.startsWith('1111') ? 'Admin' : 'Studente'}
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
}
