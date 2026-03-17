import { useEffect, useState, useCallback } from 'react';
import { ApiHelper } from '../services/apiClient';
import type { BookingDto, ClassSessionDto } from '../types/models';
import { CheckCircle2, XCircle, AlertCircle } from 'lucide-react';
import { format, differenceInMinutes } from 'date-fns';
import { useAuth } from '../context/AuthContext';
import './MyBookings.css';

export function MyBookings() {
    const { user } = useAuth();
    const [bookings, setBookings] = useState<(BookingDto & { session?: ClassSessionDto })[]>([]);
    const [loading, setLoading] = useState(true);

    const fetchData = useCallback(async () => {
        setLoading(true);
        try {
            // Fetch both in parallel
            const [books, sessions] = await Promise.all([
                ApiHelper.get<BookingDto[]>('/Bookings'),
                ApiHelper.get<ClassSessionDto[]>('/ClassSessions')
            ]);
            
            // Re-map them matching ID and user
            const activeUserBooks = books
                .filter(b => b.studentUserId === user.id)
                .map(b => ({
                    ...b,
                    session: sessions.find(s => s.id === b.sessionId)
                }));
            
            setBookings(activeUserBooks);
            setLoading(false);
        } catch (error) {
            console.error(error);
            setLoading(false);
        }
    }, [user.id]);

    useEffect(() => {
        fetchData();
    }, [fetchData]);

    const handleCancel = async (bookingId: string, sessionId: string) => {
        try {
            await ApiHelper.put(`/Bookings/${bookingId}`, {
                sessionId: sessionId,
                studentUserId: user.id,
                status: 1 // 1 is Cancelled in the Backend Enum
            });
            await fetchData(); // Refresh data cleanly
        } catch (error) {
            console.error("Error cancelling booking", error);
            alert("Non è stato possibile cancellare la prenotazione.");
        }
    };

    if (loading) {
        return (
            <div className="loading-state">
                <div className="animate-spin loader-circle" />
                <p>Caricamento prenotazioni...</p>
            </div>
        );
    }

    return (
        <div className="bookings-container">
            <header className="page-header">
                <h2>Le mie prenotazioni</h2>
                <p className="text-muted">Gestisci le tue prenotazioni future e passate.</p>
            </header>

            <div className="bookings-grid">
                {bookings.length === 0 ? (
                    <div className="glass-panel text-center full-width">Non hai ancora nessuna prenotazione.</div>
                ) : (
                    bookings.map(booking => {
                        const isCancelled = booking.status === 1;
                        const sessionTime = booking.session ? new Date(booking.session.startTime) : null;
                        const minutesUntilStart = sessionTime ? differenceInMinutes(sessionTime, new Date()) : null;
                        const isTooLate = minutesUntilStart !== null && minutesUntilStart <= 30 && minutesUntilStart > 0;
                        const isPast = minutesUntilStart !== null && minutesUntilStart < 0;
                        
                        return (
                            <div key={booking.id} className="booking-card glass-panel" style={{ opacity: isCancelled || isPast ? 0.6 : 1 }}>
                                <div className="booking-header">
                                    <h3>ID Lezione {booking.sessionId.substring(0, 8)}</h3>
                                    {sessionTime && <span className="id-badge">{format(sessionTime, 'dd MMM - HH:mm')}</span>}
                                </div>
                                <div className="booking-status" style={{ marginTop: '0.5rem' }}>
                                    {booking.status === 0 ? (
                                        <span className="status-badge success"><CheckCircle2 size={16} /> Confermata</span>
                                    ) : (
                                        <span className="status-badge danger"><XCircle size={16} /> Cancellata</span>
                                    )}
                                </div>
                                
                                <div className="booking-actions" style={{ marginTop: '1rem', display: 'flex', flexDirection: 'column', alignItems: 'flex-start', gap: '0.5rem' }}>
                                    {!isCancelled && !isPast && (
                                        <button 
                                            className="btn btn-outline" 
                                            onClick={() => handleCancel(booking.id, booking.sessionId)}
                                            disabled={isTooLate}
                                            style={isTooLate ? { opacity: 0.5, cursor: 'not-allowed' } : {}}
                                        >
                                            Cancella Prenotazione
                                        </button>
                                    )}
                                    {isTooLate && !isCancelled && (
                                        <div style={{ fontSize: '0.8rem', color: 'var(--color-danger)', display: 'flex', alignItems: 'center', gap: '4px' }}>
                                            <AlertCircle size={14} /> Mancano meno di 30 min, impossibile cancellare.
                                        </div>
                                    )}
                                    {isPast && !isCancelled && (
                                        <div style={{ fontSize: '0.8rem', color: 'var(--color-text-muted)' }}>
                                            Sessione Terminata
                                        </div>
                                    )}
                                </div>
                            </div>
                        );
                    })
                )}
            </div>
        </div>
    );
}
