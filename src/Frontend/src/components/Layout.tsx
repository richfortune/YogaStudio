import { Outlet } from 'react-router-dom';
import { Header } from './Header';
import { Sidebar } from './Sidebar';
import './Layout.css';

export function Layout() {
    return (
        <div className="app-container">
            <Header />
            <div className="app-content">
                <Sidebar />
                <main className="main-content">
                    <Outlet />
                    <footer className="main-footer">
                        <p>&copy; {new Date().getFullYear()} YogaStudio Admin. Tutti i diritti riservati.</p>
                    </footer>
                </main>
            </div>
        </div>
    );
}
