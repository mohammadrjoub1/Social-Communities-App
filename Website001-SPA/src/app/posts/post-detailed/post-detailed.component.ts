import { Component, OnInit } from '@angular/core';
import {  ActivatedRoute, ActivatedRouteSnapshot } from '@angular/router';
import { Post } from 'src/app/Models/Post.interface';
import { CommentService } from 'src/app/services/comment.service';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-post-detailed',
  templateUrl: './post-detailed.component.html',
  styleUrls: ['./post-detailed.component.css']
})
export class PostDetailedComponent implements OnInit {

  post:any;
  comments:any=[];
  parentComment:number;
  disableReplyButton:boolean=true;
  constructor(private postService:PostService,private route: ActivatedRoute,private commentService:CommentService) { }

  ngOnInit() {
 
    this.route.data.subscribe(data=>{
      this.post=data['post'];
    });

    this.commentService.getPostComments(this.post.id).subscribe(
      next=>{
        this.comments=next.body;
         console.log(next.body);
      }
    );
  
  }

}
