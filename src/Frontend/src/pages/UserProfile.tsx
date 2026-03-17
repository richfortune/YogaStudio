import { useEffect, useState } from 'react';
import { ApiHelper } from '../services/apiClient';
import type { UserDto } from '../types/models';
import { Mail, UserCircle, Phone } from 'lucide-react';
import { useAuth } from '../context/AuthContext';

export function UserProfile() {
    const { user: authUser } = useAuth();
    const [user, setUser] = useState<UserDto | null>(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        setLoading(true);
        ApiHelper.get<UserDto>(`/Users/${authUser.id}`)
            .then(data => {
                setUser(data);
                setLoading(false);
            })
            .catch(err => {
                console.error(err);
                setLoading(false);
            });
    }, [authUser.id]);

    if (loading) {
        return (
            <div className="loading-state">
                <div className="animate-spin loader-circle" />
                <p>Caricamento profilo...</p>
            </div>
        );
    }

    if (!user) {
        return (
            <div className="glass-panel text-center">
                <p>Nessun profilo trovato.</p>
            </div>
        );
    }

    return (
        <div style={{ maxWidth: '600px', margin: '0 auto' }}>
            <header className="page-header" style={{ textAlign: 'center' }}>
                <UserCircle size={64} color="var(--color-primary)" style={{ marginBottom: 'var(--spacing-md)' }} />
                <h2>Il mio Profilo</h2>
                <p className="text-muted">Gestisci le tue informazioni personali</p>
            </header>

            <div className="glass-panel">
                <div className="form-group">
                    <label>Nome Completo</label>
                    <div className="form-control" style={{ background: 'rgba(0,0,0,0.02)', fontWeight: 500 }}>
                        {user.firstName} {user.lastName}
                    </div>
                </div>

                <div className="form-group">
                    <label>Email</label>
                    <div className="form-control" style={{ background: 'rgba(0,0,0,0.02)', display: 'flex', alignItems: 'center', gap: '0.5rem' }}>
                        <Mail size={16} className="text-muted" /> {user.email}
                    </div>
                </div>

                <div className="form-group">
                    <label>Telefono</label>
                    <div className="form-control" style={{ background: 'rgba(0,0,0,0.02)', display: 'flex', alignItems: 'center', gap: '0.5rem' }}>
                        <Phone size={16} className="text-muted" /> {user.phone || 'Non specificato'}
                    </div>
                </div>

                <div className="form-group" style={{ marginTop: 'var(--spacing-lg)' }}>
                    <label>Stato Account</label>
                    <div>
                        {user.isActive ? (
                            <span style={{ background: 'var(--color-success)', color: 'white', padding: '4px 8px', borderRadius: '12px', fontSize: '0.8rem', fontWeight: 'bold' }}>ATTIVO</span>
                        ) : (
                            <span style={{ background: 'var(--color-danger)', color: 'white', padding: '4px 8px', borderRadius: '12px', fontSize: '0.8rem', fontWeight: 'bold' }}>INATTIVO</span>
                        )}
                    </div>
                </div>
            </div>
        </div>
    );
}
