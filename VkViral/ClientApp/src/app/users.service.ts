import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../environments/environment";
import {Post} from "./posts.service";
import {ActivatedRoute} from "@angular/router";

export interface User {
  name: string,
  image: string
}

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  userPath = '/Auth/User'
  logInPath = '/Auth/Authorize'
  authCodeUrl = 'https://oauth.vk.com/authorize'

  constructor(private http: HttpClient) { }

  getCurrentUser() {
    const url = environment.apiUrl.concat(this.userPath)
    return this.http.get<User>(url, {withCredentials: true});
  }

  logIn(code: string) {
    const url = environment.apiUrl.concat(this.logInPath)
    return this.http.get(url, {withCredentials: true, params: {redirectUri: window.location.origin, code: code}});
  }

  getAuthCode() {
    this.http.get(this.authCodeUrl, {withCredentials: true, params:
        {
          client_id: '8129246',
          redirect_uri: window.location.origin,
          display: 'page',
          scope: 'groups'
        }});
  }
}
