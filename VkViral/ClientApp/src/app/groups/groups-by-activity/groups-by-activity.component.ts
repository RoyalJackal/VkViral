import { Component, OnInit } from '@angular/core';
import {GroupsService} from "../../groups.service";
import {Id} from "../groups-by-name/groups-by-name.component";

@Component({
  selector: 'app-groups-by-activity',
  templateUrl: './groups-by-activity.component.html',
  styleUrls: ['./groups-by-activity.component.css']
})
export class GroupsByActivityComponent implements OnInit {

  query: string = ''
  activity: string = ''

  activities: string[] = []

  constructor(private groupsService: GroupsService) { }

  ngOnInit(): void {
    this.groupsService
      .fetchActivities()
      .subscribe(x => this.activities = x)
    console.log(this.activities)
  }

  clicked(): void {
    this.groupsService
      .fetchGroupsByActivity({query: this.query, activity: this.activity})
      .subscribe(x => this.groupsService.setGroups(x))
  }

}
