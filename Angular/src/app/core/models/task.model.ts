export class Task {
    constructor(
        public Id : number,   
        public Name : string,
        public Description : string,
        public Priority : Priority
    ){}
    
    
    
    //CreatorName : string;
    //DeveloperName : string;
    //Deadline : string;
}

enum Priority
{
    Low,
    Middle,
    High,
    UltraHigh
}
