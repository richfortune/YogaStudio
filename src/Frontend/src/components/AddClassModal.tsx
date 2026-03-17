import { useState, useEffect } from 'react';
import { ApiHelper } from '../services/apiClient';
import type { YogaClassDto, RoomDto, UserDto, CreateClassSessionDto } from '../types/models';
import { X } from 'lucide-react';

interface AddClassModalProps {
    isOpen: boolean;
    onClose: () => void;
    onSuccess: () => void;
}

export function AddClassModal({ isOpen, onClose, onSuccess }: AddClassModalProps) {
    const [classes, setClasses] = useState<YogaClassDto[]>([]);
    const [rooms, setRooms] = useState<RoomDto[]>([]);
    const [instructors, setInstructors] = useState<UserDto[]>([]);
    const [loading, setLoading] = useState(false);

    // Form state
    const [formData, setFormData] = useState<CreateClassSessionDto>({
        yogaClassId: '',
        roomId: '',
        instructorUserId: '',
        startTime: '',
        durationMinutes: 60,
        maxCapacity: 20,
        deliveryMode: 0,
        meetingUrl: ''
    });

    useEffect(() => {
        if (!isOpen) return;
        
        // Fetch required lookup data
        Promise.all([
            ApiHelper.get<YogaClassDto[]>('/YogaClasses'),
            ApiHelper.get<RoomDto[]>('/Rooms'),
            ApiHelper.get<UserDto[]>('/Users')
        ]).then(([classesData, roomsData, usersData]) => {
            setClasses(classesData);
            setRooms(roomsData);
            setInstructors(usersData); // In a real app, filter for Role = Instructor
            
            // Set first defaults if available
            setFormData(prev => ({
                ...prev,
                yogaClassId: classesData[0]?.id || '',
                roomId: roomsData[0]?.id || '',
                instructorUserId: usersData[0]?.id || ''
            }));
        }).catch(err => console.error(err));
    }, [isOpen]);

    if (!isOpen) return null;

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setLoading(true);
        try {
            const payload = {
                ...formData,
                roomId: formData.roomId === '' ? null : formData.roomId,
                meetingUrl: formData.meetingUrl === '' ? null : formData.meetingUrl,
                durationMinutes: Number(formData.durationMinutes),
                maxCapacity: Number(formData.maxCapacity),
                deliveryMode: Number(formData.deliveryMode)
            };
            await ApiHelper.post('/ClassSessions', payload);
            onSuccess(); // Close and refresh
        } catch (error) {
            console.error('Error creating session:', error);
            alert('Errore durante la creazione della sessione.');
        } finally {
            setLoading(false);
        }
    };

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        const { name, value } = e.target;
        setFormData(prev => ({ ...prev, [name]: value }));
    };

    return (
        <div className="modal-overlay">
            <div className="modal-content">
                <div className="modal-header">
                    <h3>Aggiungi Nuova Lezione</h3>
                    <button className="btn-icon" onClick={onClose} aria-label="Chiudi"><X size={20} /></button>
                </div>
                <form onSubmit={handleSubmit}>
                    <div className="form-group">
                        <label>Tipo di Corso</label>
                        <select className="form-control" name="yogaClassId" value={formData.yogaClassId} onChange={handleChange} required>
                            <option value="">Seleziona un corso</option>
                            {classes.map(c => <option key={c.id} value={c.id}>{c.name}</option>)}
                        </select>
                    </div>

                    <div className="form-group">
                        <label>Istruttore</label>
                        <select className="form-control" name="instructorUserId" value={formData.instructorUserId} onChange={handleChange} required>
                            <option value="">Seleziona un istruttore</option>
                            {instructors.map(i => <option key={i.id} value={i.id}>{i.firstName} {i.lastName}</option>)}
                        </select>
                    </div>

                    <div className="flex-gap">
                        <div className="form-group" style={{ flex: 1 }}>
                            <label>Stanza</label>
                            <select className="form-control" name="roomId" value={formData.roomId || ''} onChange={handleChange}>
                                <option value="">Nessuna Stanza</option>
                                {rooms.map(r => <option key={r.id} value={r.id}>{r.name}</option>)}
                            </select>
                        </div>
                        <div className="form-group" style={{ flex: 1 }}>
                            <label>Modalità</label>
                            <select className="form-control" name="deliveryMode" value={formData.deliveryMode} onChange={handleChange}>
                                <option value={0}>In Presenza</option>
                                <option value={1}>Online</option>
                                <option value={2}>Ibrida</option>
                            </select>
                        </div>
                    </div>

                    <div className="flex-gap">
                        <div className="form-group" style={{ flex: 2 }}>
                            <label>Data e Ora Inizio</label>
                            <input type="datetime-local" className="form-control" name="startTime" value={formData.startTime} onChange={handleChange} required />
                        </div>
                        <div className="form-group" style={{ flex: 1 }}>
                            <label>Durata (min)</label>
                            <input type="number" className="form-control" name="durationMinutes" value={formData.durationMinutes} onChange={handleChange} required min="15" step="15" />
                        </div>
                    </div>

                    <div style={{ marginTop: '1.5rem', display: 'flex', justifyContent: 'flex-end', gap: '1rem' }}>
                        <button type="button" className="btn btn-outline" onClick={onClose} style={{ border: '1px solid var(--glass-border)', background: 'transparent', color: 'var(--color-text-main)' }}>Annulla</button>
                        <button type="submit" className="btn btn-primary" disabled={loading}>
                            {loading ? 'Salvataggio...' : 'Salva Lezione'}
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
}
