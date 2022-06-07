import { Component, OnInit } from '@angular/core';
import {Post, PostsService} from "../../posts.service";
import { CommonModule } from "@angular/common";

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  styleUrls: ['./post-list.component.css']
})
export class PostListComponent implements OnInit {

  posts: Post[] = []

  constructor(private postsService: PostsService) {
    this.postsService.postsChange.subscribe(value => {
      this.posts = value
    })
  }

  ngOnInit(): void {
  }

}
