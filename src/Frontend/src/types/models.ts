// Models reflecting Backend DTOs
export interface RoomDto {
    id: string;
    name: string;
    capacity?: number;
    address?: string;
}

export interface ClassSessionDto {
    id: string;
    yogaClassId: string;
    roomId?: string;
    instructorUserId: string;
    startTime: string; // ISO DateTimeOffset
    durationMinutes: number;
    maxCapacity: number;
    deliveryMode: number;
    meetingUrl?: string;
}

export interface BookingDto {
    id: string;
    sessionId: string;
    studentUserId: string;
    status: number;
}

export interface UserDto {
    id: string;
    firstName: string;
    lastName: string;
    email: string;
    phone?: string;
    isActive: boolean;
}

export interface YogaClassDto {
    id: string;
    name: string;
    description: string;
    difficultyLevel: number;
}

export interface CreateClassSessionDto {
    yogaClassId: string;
    roomId?: string | null;
    instructorUserId: string;
    startTime: string;
    durationMinutes: number;
    maxCapacity: number;
    deliveryMode: number;
    meetingUrl?: string | null;
}
