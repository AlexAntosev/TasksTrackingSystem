import { UserRole } from 'src/app/models/user-role';

export interface User {
    Id: number;
    UserName: string;
    FirstName: string;
    LastName: string;
    Position: string;
    Email: string;
    Role: UserRole;
}