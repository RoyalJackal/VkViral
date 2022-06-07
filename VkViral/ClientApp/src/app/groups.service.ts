import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Observable, Subject, throwError} from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { environment } from '../environments/environment';
import {SelectionModel} from "@angular/cdk/collections";

export interface Group {
  groupId: number,
  groupName: string,
  groupImg: string,
  groupTheme: string
}

const maxGroups = 10

@Injectable({
  providedIn: 'root'
})
export class GroupsService {

  byIdPath = '/Groups/Ids'
  byUserPath = '/Groups/CurrentUser'
  byQueryPath = '/Groups/Query'
  byActivityPath = '/Groups/Activity'
  getActivitiesPath = '/Auth/Activities'
  groups: Group[] = []
  selectedGroups = new SelectionModel<Group>(true, [])

  groupsChange: Subject<Group[]> = new Subject<Group[]>()

  constructor(private http: HttpClient) {
    this.groupsChange.subscribe((value) => {
      this.groups = value
    })
  }

  getGroups() {
    return this.groups;
  }

  setGroups(groups: Group[]) {
    this.groupsChange.next(groups)
    this.selectedGroups.clear()
  }

  fetchGroups(ids: {groupIds: string[]}) {
    const url = environment.apiUrl.concat(this.byIdPath)
    return this.http.post<Group[]>(url, ids, {withCredentials: true});
  }

  fetchSubscribedGroups() {
    const url = environment.apiUrl.concat(this.byUserPath)
    return this.http.post<Group[]>(url, {}, {withCredentials: true});
  }

  fetchGroupsByQuery(dto: {query: string}) {
    const url = environment.apiUrl.concat(this.byQueryPath)
    return this.http.post<Group[]>(url, dto, {withCredentials: true});
  }

  fetchGroupsByActivity(dto: {query: string, activity: string}) {
    const url = environment.apiUrl.concat(this.byActivityPath)
    return this.http.post<Group[]>(url, dto, {withCredentials: true});
  }

  fetchActivities() {
    const url = environment.apiUrl.concat(this.getActivitiesPath)
    return this.http.get<string[]>(url, {withCredentials: true});
  }
}
