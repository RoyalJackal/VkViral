import { Component, OnInit } from '@angular/core';
@Component({
  selector: 'app-groups-form',
  templateUrl: './groups-form.component.html',
  styleUrls: ['./groups-form.component.css']
})
export class GroupsFormComponent implements OnInit {

  selected = 'byName';

  constructor() { }

  ngOnInit(): void {
  }
}
