import { Component, OnInit } from '@angular/core';
import {User, UsersService} from "../users.service";
import {CookieService} from "ngx-cookie-service";

@Component({
  selector: 'app-user-box',
  templateUrl: './user-box.component.html',
  styleUrls: ['./user-box.component.css']
})
export class UserBoxComponent implements OnInit {

  user: User = {name: '', image: ''}

  constructor(private userService: UsersService, private cookies: CookieService) { }

  ngOnInit(): void {
    this.userService
      .getCurrentUser()
      .subscribe(x => this.user = x)
  }

  clicked(): void {
    this.cookies.delete('VkViral')
    window.location.reload()
  }

}
