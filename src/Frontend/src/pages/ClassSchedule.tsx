import { useEffect, useState, useCallback } from 'react';
import { ApiHelper } from '../services/apiClient';
import type { ClassSessionDto } from '../types/models';
import { Calendar as CalendarIcon, Clock, MapPin, Plus } from 'lucide-react';
import { format } from 'date-fns';
import { AddClassModal } from '../components/AddClassModal';
import { useAuth } from '../context/AuthContext';
import './ClassSchedule.css';

export function ClassSchedule() {
    const { isAdmin, user } = useAuth();
    const [sessions, setSessions] = useState<ClassSessionDto[]>([]);
    const [loading, setLoading] = useState(true);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [bookingStates, setBookingStates] = useState<Record<string, 'idle' | 'loading' | 'success' | 'error'>>({});

    const fetchSessions = useCallback(() => {
        setLoading(true);
        ApiHelper.get<ClassSessionDto[]>('/ClassSessions')
            .then(data => {
                setSessions(data);
                setLoading(false);
            })
            .catch(err => {
                console.error(err);
                setLoading(false);
            });
    }, []);

    useEffect(() => {
        fetchSessions();
    }, [fetchSessions]);

    const handleBook = async (sessionId: string) => {
        setBookingStates(prev => ({ ...prev, [sessionId]: 'loading' }));
        try {
            await ApiHelper.post('/Bookings', {
                sessionId: sessionId,
                studentUserId: user.id, // Now uses the active simulated user
                status: 0 // Confirmed
            });
            setBookingStates(prev => ({ ...prev, [sessionId]: 'success' }));
            
            // Revert back to idle after 3 seconds
            setTimeout(() => {
                setBookingStates(prev => ({ ...prev, [sessionId]: 'idle' }));
            }, 3000);
        } catch (error) {
            console.error(error);
            setBookingStates(prev => ({ ...prev, [sessionId]: 'error' }));
            alert('Errore: impossibile completare la prenotazione. Controlla che tu non sia già prenotato!');
            setTimeout(() => {
                setBookingStates(prev => ({ ...prev, [sessionId]: 'idle' }));
            }, 3000);
        }
    };

    if (loading) {
        return (
            <div className="loading-state">
                <div className="animate-spin loader-circle" />
                <p>Caricamento programma...</p>
            </div>
        );
    }

    return (
        <div className="schedule-container">
            <header className="page-header" style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                <div>
                    <h2>Programma Lezioni</h2>
                    <p className="text-muted">Sfoglia e prenota le prossime sessioni di yoga.</p>
                </div>
                {isAdmin && (
                    <button className="btn btn-primary" onClick={() => setIsModalOpen(true)}>
                        <Plus size={18} /> Nuova Lezione
                    </button>
                )}
            </header>

            <div className="session-list">
                {sessions.length === 0 ? (
                    <div className="glass-panel text-center">Nessuna lezione in programma.</div>
                ) : (
                    sessions.map(session => {
                        const status = bookingStates[session.id] || 'idle';
                        return (
                            <div key={session.id} className="session-card glass-panel">
                                <div className="session-time">
                                    <span className="time-large">{format(new Date(session.startTime), 'HH:mm')}</span>
                                    <span className="time-date">{format(new Date(session.startTime), 'dd MMM')}</span>
                                </div>
                                <div className="session-details">
                                    <h3>Vinyasa Flow (Esempio)</h3>
                                    <div className="session-meta">
                                        <span className="meta-badge"><Clock size={14} /> {session.durationMinutes} min</span>
                                        <span className="meta-badge"><MapPin size={14} /> Stanza {session.roomId?.substring(0, 4) || 'N.D.'}</span>
                                        <span className="meta-badge"><CalendarIcon size={14} /> {session.deliveryMode === 0 ? 'In Presenza' : 'Online'}</span>
                                    </div>
                                </div>
                                <div className="session-actions">
                                    <button 
                                        className={`btn ${status === 'success' ? 'btn-success' : 'btn-primary'}`} 
                                        onClick={() => handleBook(session.id)}
                                        disabled={status !== 'idle'}
                                        style={status === 'success' ? { backgroundColor: 'var(--color-success)', color: 'white' } : {}}
                                    >
                                        {status === 'loading' && 'Elaborazione...'}
                                        {status === 'success' && 'Prenotato ✅'}
                                        {status === 'error' && 'Errore ❌'}
                                        {status === 'idle' && 'Prenota Ora'}
                                    </button>
                                </div>
                            </div>
                        );
                    })
                )}
            </div>
            
            <AddClassModal 
                isOpen={isModalOpen} 
                onClose={() => setIsModalOpen(false)} 
                onSuccess={() => {
                    setIsModalOpen(false);
                    fetchSessions();
                }} 
            />
        </div>
    );
}
