import { Pipe, PipeTransform } from '@angular/core';
import {Group} from "./groups.service";

@Pipe({
  name: 'search'
})
export class SearchGroupPipePipe implements PipeTransform {

  transform(value: Group[], query: string) {
    if (!query) return value;
    return (value || []).filter(item => item.groupName.toLowerCase().startsWith(query.toLowerCase()))
  }

}
