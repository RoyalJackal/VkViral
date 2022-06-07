import {Component, Input, OnInit} from '@angular/core';
import {Post} from "../../posts.service";

@Component({
  selector: 'app-media-box',
  templateUrl: './media-box.component.html',
  styleUrls: ['./media-box.component.css']
})
export class MediaBoxComponent implements OnInit {

  constructor() { }

  @Input() post!: Post

  ngOnInit(): void {
    console.log(this.post)
  }

}
