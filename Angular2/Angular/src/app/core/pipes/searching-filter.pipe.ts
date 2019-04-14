import { Pipe, PipeTransform } from '@angular/core';
import { Task } from 'src/app/models/task';
import { User } from 'src/app/models/user';

@Pipe({
    name: 'searchingFilter'
})
export class SearchingFilterPipe implements PipeTransform {
    transform(items: any[], searchText: string): any[] {
        if (!items) return [];
        if (!searchText) return items;
        searchText = searchText.toLowerCase();
        return items.filter(it => {
            if(it.UserName !== undefined)
                return it.UserName.toLowerCase().includes(searchText);
            if(it.Name !== undefined)
                return it.Name.toLowerCase().includes(searchText);
            
        });
    }
}