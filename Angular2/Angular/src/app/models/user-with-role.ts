import { User } from 'src/app/models/user';

export interface UserWithRole {
    Id: number;
    UserId: number;
    User: User;
    Role: number;
}