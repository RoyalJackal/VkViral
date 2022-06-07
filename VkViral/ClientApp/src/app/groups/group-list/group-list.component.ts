import { Component, OnInit } from '@angular/core';
import {Group, GroupsService} from "../../groups.service";

@Component({
  selector: 'app-group-list',
  templateUrl: './group-list.component.html',
  styleUrls: ['./group-list.component.css']
})
export class GroupListComponent implements OnInit {

  groups: Group[] = []
  query: string = ''

  toggleGroup(group: Group) {
    this.groupsService.selectedGroups.toggle(group)
  }

  isGroupSelected(group: Group) {
    return this.groupsService.selectedGroups.isSelected(group)
  }

  constructor(private groupsService: GroupsService) {
    this.groupsService.groupsChange.subscribe(value => {
      this.groups = value
    })
  }

  ngOnInit(): void {
  }

}
