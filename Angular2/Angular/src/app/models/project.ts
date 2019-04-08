import { Task } from 'src/app/models/task';
import { User } from 'src/app/models/user';

export interface Project {
    Id: number;
    Name: string;
    Tag: string;
    Tasks: Task[];   
    Team: User[]; 
}
