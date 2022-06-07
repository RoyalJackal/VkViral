import { Component, OnInit } from '@angular/core';
import {Id} from "../groups-by-name/groups-by-name.component";
import {GroupsService} from "../../groups.service";

@Component({
  selector: 'app-groups-by-user',
  templateUrl: './groups-by-user.component.html',
  styleUrls: ['./groups-by-user.component.css']
})
export class GroupsByUserComponent implements OnInit {

  clicked(): void {
    this.groupsService
      .fetchSubscribedGroups()
      .subscribe(x => this.groupsService.setGroups(x))
  }

  constructor(private groupsService: GroupsService) { }

  ngOnInit(): void {
  }

}
