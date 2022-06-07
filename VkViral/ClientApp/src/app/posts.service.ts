import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../environments/environment";
import {Subject} from "rxjs";

export interface Post {
  groupName: string,
  groupImg: string,
  publicationDate: Date,
  text: string,
  images: string[],
  videos: string[],
  audios: string[],
  likes: number,
  comments: number,
  reposts: number,
  views: number,
  virality: number,
  groupPostCount: number,
  groupMemberCount: number
}

@Injectable({
  providedIn: 'root'
})
export class PostsService {

  postsPath = '/Posts/InGroups'

  posts: Post[] = []
  postsChange: Subject<Post[]> = new Subject<Post[]>()

  constructor(private http: HttpClient) {
    this.postsChange.subscribe((value) => {
      this.posts = value
    })
  }

  setPosts(posts: Post[]) {
    this.postsChange.next(posts)
  }

  fetchPosts(dto: {groupIds: string[], sortType: string}) {
    const url = environment.apiUrl.concat(this.postsPath)
    return this.http.post<Post[]>(url, dto, {withCredentials: true});
  }
}
