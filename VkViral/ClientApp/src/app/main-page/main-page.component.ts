import { Component, OnInit } from '@angular/core';
import {GroupsService} from "../groups.service";
import {CookieService} from "ngx-cookie-service";
import {ActivatedRoute} from "@angular/router";
import {UsersService} from "../users.service";

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css']
})
export class MainPageComponent implements OnInit {

  noGroupsSelected() {
    return this.groupsService.selectedGroups.selected.length == 0
  }

  isLoggedIn()
  {
    return this.cookies.check('VkViral') && this.cookies.get('VkViral') != ''
  }

  constructor(private groupsService: GroupsService,
              private cookies: CookieService,
              private route: ActivatedRoute,
              private usersService: UsersService) { }

  ngOnInit(): void {
    const params = this.route.snapshot.queryParams
    if (params['code']) {
      this.usersService.logIn(params['code'])
        .subscribe(x => window.location.href = window.location.origin)
    }
  }

}
