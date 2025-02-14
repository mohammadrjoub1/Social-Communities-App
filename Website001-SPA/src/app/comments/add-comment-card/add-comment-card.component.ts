import { Component, Input, OnInit } from '@angular/core';
import { Post } from 'src/app/Models/Post.interface';
import { AlertifyService } from 'src/app/services/alertify.service';
import { AuthService } from 'src/app/services/auth.service';
import { CommentService } from 'src/app/services/comment.service';

@Component({
  selector: 'app-add-comment-card',
  templateUrl: './add-comment-card.component.html',
  styleUrls: ['./add-comment-card.component.css']
})
export class AddCommentCardComponent implements OnInit {
  comment:any={};
 @Input() post: Post;
 @Input() parentComment:number;
  constructor(private alertify:AlertifyService, private commentService:CommentService,private authService:AuthService) { }
  added=false;
  ngOnInit() {
 
  }

  add(){

    if(this.authService.logged){
     this.comment.userId=this.authService.decodedToken.nameid;;
    this.comment.postId=this.post.id;
    this.comment.categorieId=this.post.categorieId;
    this.comment.comment=this.comment.comment;
    this.comment.prime=true;
    this.comment.parentCommentId=this.parentComment;
    this.comment.date=this.comment.date;
    this.commentService.add(this.comment).subscribe(
      next=>{
        this.alertify.success("Commented Successfuly");
        this.comment=next;

      this.added=true;


        
      }
      ,error=>{
        this.alertify.error(error);
      }
    );
    }
    else{
      this.alertify.error("You should be logged in");
    }
  }

}
