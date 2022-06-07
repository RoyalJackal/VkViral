import { Component, OnInit } from '@angular/core';
import {UsersService} from "../users.service";

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {

  constructor(private usersService: UsersService) { }

  ngOnInit(): void {
  }

  clicked() {
    window.location.href = 'https://oauth.vk.com/authorize?client_id=8129246&redirect_uri=' + window.location.origin + '/&display=page&scope=groups'
  }
}
