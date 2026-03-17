import { createContext, useContext, useState, type ReactNode } from 'react';

// Hardcoded Seeded Users from Backend Migration for Demo Purposes
export const MOCK_USERS = {
    ADMIN: {
        id: '11111111-1111-1111-1111-111111111111',
        name: 'Admin User',
        role: 'Admin'
    },
    STUDENT: {
        id: '33333333-3333-3333-3333-333333333333',
        name: 'Mario Rossi',
        role: 'Student'
    }
};

export type AuthUser = typeof MOCK_USERS.ADMIN;

interface AuthContextType {
    user: AuthUser;
    setUserRole: (role: 'Admin' | 'Student') => void;
    isAdmin: boolean;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export function AuthProvider({ children }: { children: ReactNode }) {
    const [user, setUser] = useState<AuthUser>(MOCK_USERS.ADMIN);

    const setUserRole = (role: 'Admin' | 'Student') => {
        setUser(role === 'Admin' ? MOCK_USERS.ADMIN : MOCK_USERS.STUDENT);
    };

    return (
        <AuthContext.Provider value={{ user, setUserRole, isAdmin: user.role === 'Admin' }}>
            {children}
        </AuthContext.Provider>
    );
}

// Custom hook to easily consume auth state
export function useAuth() {
    const context = useContext(AuthContext);
    if (context === undefined) {
        throw new Error('useAuth must be used within an AuthProvider');
    }
    return context;
}
