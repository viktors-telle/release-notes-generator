import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'filter',
    pure: false
})
export class FilterPipe implements PipeTransform {
    transform(items: any[], filter: any): any {
        if (!items || !filter) {
            return items;
        }

        let propertyName = Object.keys(filter)[0];
        var result = items.filter(item => item[propertyName] === filter[propertyName]);
        return result;
    }
}