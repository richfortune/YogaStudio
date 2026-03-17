import { useEffect, useState } from 'react';
import { ApiHelper } from '../services/apiClient';
import type { ClassSessionDto } from '../types/models';
import { Activity, Users, CalendarCheck } from 'lucide-react';
import './Dashboard.css';

export function Dashboard() {
    const [sessionCount, setSessionCount] = useState(0);

    useEffect(() => {
        ApiHelper.get<ClassSessionDto[]>('/ClassSessions')
            .then(data => setSessionCount(data.length))
            .catch(err => console.error(err));
    }, []);

    return (
        <div className="dashboard-container">
            <header className="page-header">
                <h2>Panoramica Bacheca</h2>
                <p className="text-muted">Bentornato su YogaStudio Admin.</p>
            </header>

            <div className="stats-grid">
                <div className="stat-card glass-panel">
                    <div className="stat-icon" style={{ background: 'rgba(123, 44, 191, 0.1)', color: 'var(--color-primary)' }}>
                        <Activity size={24} />
                    </div>
                    <div className="stat-info">
                        <h3>Lezioni Attive</h3>
                        <p className="stat-value">{sessionCount || '-'}</p>
                    </div>
                </div>

                <div className="stat-card glass-panel">
                    <div className="stat-icon" style={{ background: 'rgba(0, 180, 216, 0.1)', color: 'var(--color-accent)' }}>
                        <Users size={24} />
                    </div>
                    <div className="stat-info">
                        <h3>Totale Studenti</h3>
                        <p className="stat-value">124</p>
                    </div>
                </div>

                <div className="stat-card glass-panel">
                    <div className="stat-icon" style={{ background: 'rgba(42, 157, 143, 0.1)', color: 'var(--color-success)' }}>
                        <CalendarCheck size={24} />
                    </div>
                    <div className="stat-info">
                        <h3>Prenotazioni Oggi</h3>
                        <p className="stat-value">18</p>
                    </div>
                </div>
            </div>
        </div>
    );
}
