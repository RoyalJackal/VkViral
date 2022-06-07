import { Component, OnInit } from '@angular/core';
import {PostsService} from "../../posts.service";
import {GroupsService} from "../../groups.service";

@Component({
  selector: 'app-posts-form',
  templateUrl: './posts-form.component.html',
  styleUrls: ['./posts-form.component.css']
})
export class PostsFormComponent implements OnInit {

  sortType = 'Likes'

  clicked(): void {
    const groupIds = this.groupsService.selectedGroups.selected.map(x => x.groupId.toString())
    this.postsService
      .fetchPosts({groupIds: groupIds, sortType: this.sortType})
      .subscribe(x => this.postsService.setPosts(x))
  }

  noGroupsSelected() {
    return this.groupsService.selectedGroups.selected.length == 0
  }

  constructor(private postsService: PostsService, private groupsService: GroupsService) { }

  ngOnInit(): void {
  }

}
