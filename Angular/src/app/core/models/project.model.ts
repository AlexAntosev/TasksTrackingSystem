import { Task } from 'src/app/core/models/task.model';

export class Project {
    constructor(
        public Id : number,    
        public Name : string,    
        public Tag : string,
    ){}
    Tasks: Task[];
}
