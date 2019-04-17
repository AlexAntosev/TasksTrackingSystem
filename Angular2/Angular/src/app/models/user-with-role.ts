import { User } from 'src/app/models/user';
import { Role } from 'src/app/models/role.enum';

export interface UserWithRole {
    Id: number;
    User: User;
    Role: Role;
}