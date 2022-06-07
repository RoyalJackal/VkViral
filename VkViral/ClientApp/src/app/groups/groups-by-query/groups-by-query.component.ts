import { Component, OnInit } from '@angular/core';
import {GroupsService} from "../../groups.service";

@Component({
  selector: 'app-groups-by-query',
  templateUrl: './groups-by-query.component.html',
  styleUrls: ['./groups-by-query.component.css']
})
export class GroupsByQueryComponent implements OnInit {

  query: string = ''

  constructor(private groupsService: GroupsService) { }

  ngOnInit(): void {
  }

  clicked(): void {
    this.groupsService
      .fetchGroupsByQuery({query: this.query})
      .subscribe(x => this.groupsService.setGroups(x))
  }

}
