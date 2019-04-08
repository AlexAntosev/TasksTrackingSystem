import { UserRole } from 'src/app/models/user-role';
import { Project } from 'src/app/models/project';
import { Task } from 'src/app/models/task';

export interface User {
    Id: number;
    UserName: string;
    FirstName: string;
    LastName: string;
    Position: string;
    Email: string;
    Role: UserRole;
    Projects: Project[];
    CreatedTasks: Task[];
    TasksInProcess: Task[];
}