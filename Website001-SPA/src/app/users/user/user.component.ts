import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ActivatedRouteSnapshot } from '@angular/router';
import { Pagination } from 'src/app/Models/Pagination';
import { Post } from 'src/app/Models/Post.interface';
import { User } from 'src/app/Models/Uesr.interface';
import { AlertifyService } from 'src/app/services/alertify.service';
import { AuthService } from 'src/app/services/auth.service';
import { PostService } from 'src/app/services/post.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  constructor( private _route: ActivatedRoute,private postService:PostService,private userService:UserService,private alertify:AlertifyService,private authService:AuthService) { }

  posts:any[];
  pageNumber=1;
  pageSize=2;
  continueLoading=true;
  showJoiningToCategorieOption=true;
   dontDeleteMine=false;
  sortBy="Top";
  Type="User"
  user:User;
  userId=0;
   ngOnInit() {       

    this.userService.getUserByUsername(this._route.snapshot.paramMap.get('username')).subscribe(
      userr=>{
        this.user=userr;
        this.userId=userr.id;
      console.log(this.userId);
      
      this.pageNumber=1;

      if(!this.authService.logged){
        this.showJoiningToCategorieOption=false;
        this.postService.getPostsPaging(this.Type,1,2,this.sortBy,0,0,this.userId).subscribe(
          next=>{
            if(next[0]==null){  this.continueLoading=false;   }

            this.posts=next.result;
          },
          error=>{
            console.log(error);
          }
        );
       }
       else{
        this.postService.getPostsPaging(this.Type,1,2,this.sortBy,0,+this.authService.decodedToken.nameid,this.userId).subscribe(
          next=>{
            if(next[0]==null){  this.continueLoading=false;   }

          this.posts=next.result;
        },
        error=>{
          console.log(error);
        }
      );}
    
    },

        error=>{
        this.alertify.error(error);

      } );
    
    
  }

  addPosts(){
    this.pageNumber=1;

    if(!this.authService.logged){
      this.showJoiningToCategorieOption=false;
      this.postService.getPostsPaging(this.Type,1,2,this.sortBy,0,0,this.userId).subscribe(
        next=>{
          if(next[0]==null){  this.continueLoading=false;   }

          this.posts=next.result;
        },
        error=>{
          console.log(error);
        }
      );
     }
     else{
      this.postService.getPostsPaging(this.Type,1,2,this.sortBy,0,+this.authService.decodedToken.nameid,this.userId).subscribe(
        next=>{
          if(next[0]==null){  this.continueLoading=false;   }

        this.posts=next.result;
      },
      error=>{
        console.log(error);
      }
    );}
  }
  addMorePosts(){
    if(this.posts!=null){
    let p:Pagination;

    if(this.authService.logged&&this.authService.decodedToken.nameid!=null){

      this.pageNumber=this.pageNumber+1;
      this.postService.getPostsPaging(this.Type,this.pageNumber,2,this.sortBy,0,this.authService.decodedToken.nameid,this.userId).subscribe(
        next=>{
          if(next[0]==null){  this.continueLoading=false;   }

          p=next.pagination;
          this.posts=(this.posts.concat(next.result));
          if(p.totalPages==p.pageNumber){
            this.continueLoading=false;
          }
  
        },
        error=>{
          console.log(error);
        }
      );
    }
    else{

    this.pageNumber=this.pageNumber+1;
    this.postService.getPostsPaging(this.Type,this.pageNumber,2,this.sortBy,0,0,this.userId).subscribe(
      next=>{
        if(next[0]==null){  this.continueLoading=false;   }

        p=next.pagination;
        this.posts=(this.posts.concat(next.result));
        console.log(this.posts);
        if(p.totalPages==p.pageNumber){
          this.continueLoading=false;
        }

      },
      error=>{
        console.log(error);
      }
    );
    }
    }
  }

  onScroll() {    
    this.addMorePosts(); }

}


















 