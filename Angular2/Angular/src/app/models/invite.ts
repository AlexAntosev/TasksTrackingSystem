export interface Invite {
    Id: number;

    AuthorId: number;
    AuthorUserName: string;

    ProjectId: number;
    ProjectName: string;

    ReceiverId: number;
    ReceiverUserName: string;

    Time: Date;
}