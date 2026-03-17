import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { Layout } from './components/Layout';
import { AuthProvider } from './context/AuthContext';

import { Dashboard } from './pages/Dashboard';
import { ClassSchedule } from './pages/ClassSchedule';
import { MyBookings } from './pages/MyBookings';
import { UserProfile } from './pages/UserProfile';
import { UsersList } from './pages/UsersList';

function App() {
  return (
    <AuthProvider>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Layout />}>
            <Route index element={<Dashboard />} />
            <Route path="classes" element={<ClassSchedule />} />
            <Route path="bookings" element={<MyBookings />} />
            <Route path="profile" element={<UserProfile />} />
            <Route path="users" element={<UsersList />} />
            <Route path="*" element={<Navigate to="/" replace />} />
          </Route>
        </Routes>
      </BrowserRouter>
    </AuthProvider>
  );
}

export default App;
