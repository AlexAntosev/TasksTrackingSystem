import { Injectable, Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common';

@Injectable()
@Pipe({ name: 'formatDatePipe', pure: true })
export class FormatDatePipe extends DatePipe implements PipeTransform {
    transform(value: any, format: string): string {
        value = new Date(value);
        return super.transform(value, format);
    }
}