import { Component, OnInit } from '@angular/core';
import {COMMA, ENTER} from '@angular/cdk/keycodes';
import {MatChipInputEvent} from '@angular/material/chips';
import {GroupsService} from "../../groups.service";

export interface Id {
  value: string;
}

@Component({
  selector: 'app-groups-by-name',
  templateUrl: './groups-by-name.component.html',
  styleUrls: ['./groups-by-name.component.css']
})
export class GroupsByNameComponent implements OnInit {

  addOnBlur = true;
  readonly separatorKeysCodes = [ENTER, COMMA] as const;
  ids: Id[] = [];

  add(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();

    if (value) {
      this.ids.push({value: value});
    }

    event.chipInput!.clear();
  }

  remove(item: Id): void {
    const index = this.ids.indexOf(item);

    if (index >= 0) {
      this.ids.splice(index, 1);
    }
  }

  clicked(): void {
    const dto = {groupIds: this.ids.map((x: Id) => x.value)}
    this.groupsService
      .fetchGroups(dto)
      .subscribe(x => this.groupsService.setGroups(x))
  }

  constructor(private groupsService: GroupsService) { }

  ngOnInit(): void {
  }

}
