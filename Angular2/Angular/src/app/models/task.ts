import { Priority } from 'src/app/models/priority.enum';
import { Status } from 'src/app/models/status.enum';
import { Type } from 'src/app/models/type.enum';
import { formatDate } from '@angular/common';

export interface Task {
    Id: number;
    Name: string;
    Description: string;
    Priority: Priority;
    Status: Status;
    Type: Type;
    ProjectId: number;
    CreatorId: number;
    CreatorUserName: string;
    ExecutorId: number;
    ExecutorUserName: string;
    Created: Date;
    Updated: Date;
    Deadline: Date;
}