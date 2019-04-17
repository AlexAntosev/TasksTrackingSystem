import { Role } from 'src/app/models/role.enum';

export interface Invite {
    Id: number;
    AuthorId: number;
    AuthorUserName: string;
    ProjectId: number;
    ProjectName: string;
    ReceiverId: number;
    ReceiverUserName: string;
    Time: Date;
    Role: Role;
}