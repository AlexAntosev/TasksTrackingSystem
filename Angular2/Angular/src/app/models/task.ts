import { Priority } from 'src/app/models/priority.enum';

export interface Task{
    Id : number,   
    Name : string,
    Description : string,
    Priority : Priority
}