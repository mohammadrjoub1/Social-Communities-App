import { Component, Input, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { CategorieJoiningDto } from 'src/app/Dtos/CategorieJoiningDto';
import { Post } from 'src/app/Models/Post.interface';
import { AlertifyService } from 'src/app/services/alertify.service';
import { AuthService } from 'src/app/services/auth.service';
import { CategorieService } from 'src/app/services/categorie.service';
import { CommentService } from 'src/app/services/comment.service';
import { PostService } from 'src/app/services/post.service';
 @Component({
  selector: 'app-post-card',
  templateUrl: './post-card.component.html',
  styleUrls: ['./post-card.component.css']
})
export class PostCardComponent implements OnInit {
  @Input() showJoiningToCategorieOption:boolean;
  @Input() isJoind:boolean;
  @Input() post:Post;

  posta:Post
  dontDeleteMine=false;

  categorieName="#<^>!#$";
  numberOfComments=0;
   c:any={};    
    


  constructor(private commentService:CommentService,private alertify:AlertifyService,private postService:PostService, public authService:AuthService,private categorieService:CategorieService) { }
  ngOnInit() {
    this.posta=this.post;
    console.log("*(**********************************");
    console.log("From here "+this.post);
    console.log("From hereaaa "+this.posta);
    console.log("*(**********************************");


    this.categorieService.getCategorie(this.post.categorieId).subscribe(
      next=>{
        this.categorieName=next.title;
        this.commentService.getPostComments(this.post.id).subscribe(
          next=>{
            this.numberOfComments=next.body.length;
          }
        );

      }
    )
    if(!this.authService.logged){
      this.showJoiningToCategorieOption=false;

    }
    else{

      this.c.userId=this.authService.decodedToken.nameid;
      this.c.categorieId=this.post.categorieId;
      this.categorieService.isUserJoindToCategorie(this.c).subscribe(
        next=>{
          this.isJoind=next;      

        },
      
      );

    }
  }

  loggedIn(){
    return this.authService.logged;

  }
  joinCategorie(){
    this.categorieService.joinCategorie(this.c).subscribe(
                  next=>{
        if(next==true){
        this.alertify.success("Joind Successfuly");
        this.categorieService.subject.next(this.categorieName);
        this.categorieService.nowJoind=this.categorieService.subject.getValue();
        this.dontDeleteMine=true;
        this.isJoind=true;
        }
        if(next==false){
        this.alertify.error("Coudn't join");
      }
      }
    );
  }
  leaveCategorie(){
    this.categorieService.leaveCategorie(this.c).subscribe(
      next=>{
        if(next==true){
          this.categorieService.subject.next("Q#$%Q@#T$<QW E >@#$<!#@");
          this.categorieService.nowJoind=this.categorieService.subject.getValue();
          this.dontDeleteMine=false;
          this.isJoind=false;
          this.alertify.error("Left Successfuly");

        }

      }
    );
  }
}
