import { Component, Input, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { PostService } from 'src/app/services/post.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-comment-card',
  templateUrl: './comment-card.component.html',
  styleUrls: ['./comment-card.component.css']
})
export class CommentCardComponent implements OnInit {
  @Input() comment:any={};
  @Input() parentComment:number;
  @Input() post:any;
  @Input() disableReplyButton:boolean;
  showReplyBoxValue=0;
  constructor(private userService:UserService,private authService:AuthService,private postService:PostService) { }
  user:any={};
  ngOnInit() {
 
    this.parentComment=this.comment.id;

    this.userService.getUser(+this.comment.userId).subscribe(
      next=>{
        
        this.user=next;
 
      }
    );
  }
  showReplyBox(){
    this.showReplyBoxValue=1;
  }


}
