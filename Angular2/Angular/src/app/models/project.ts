import { Task } from 'src/app/models/task';

export interface Project {
    Id: number;
    Name: string;
    Tag: string;
    Tasks: Task[];    
}
