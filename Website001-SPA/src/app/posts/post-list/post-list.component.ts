import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Pagination, PaginationResult } from 'src/app/Models/Pagination';
import { Post } from 'src/app/Models/Post.interface';
import { AlertifyService } from 'src/app/services/alertify.service';
import { AuthService } from 'src/app/services/auth.service';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  styleUrls: ['./post-list.component.css']
})
export class PostListComponent implements OnInit {
  posts:Post[];
  pageNumber=1;
  pageSize=2;
  continueLoading=true;
  showJoiningToCategorieOption=true;
  showRedirictButton=false;

  constructor(private navigate: Router,private postService:PostService,private alertifi:AlertifyService,private authService:AuthService) { }
  dontDeleteMine=false;
  sortBy="Top";
  showNoMorePosts=true;;
  ngOnInit() {
     this.pageNumber=1;

    if(!this.authService.logged){
      this.showJoiningToCategorieOption=false;
      this.postService.getPostsPaging("Home",1,2,this.sortBy,0,0,0).subscribe(
        next=>{
          this.posts=next.result;
          if(next[0]==null){  this.continueLoading=false;   }
          else {  this.continueLoading=true;   }


        },
        error=>{
          console.log(error);
        }
      );
     }
     else{
      this.postService.getPostsPaging("Home",1,2,this.sortBy,0,+this.authService.decodedToken.nameid,0).subscribe(
        next=>{
        this.posts=next.result;
        console.log(next.result+"bjk.bhnjk.bjnk");

         if(next.result==null||next.result.length==0){ 
          this.showRedirictButton=true;
          this.showNoMorePosts=false;
          this.continueLoading=false;  


        }
        else{
          this.showRedirictButton=false;
          this.continueLoading=true;  

        }
       
      },
      error=>{
        console.log(error);
      }
    );}
  }
  addPosts(){
    this.ngOnInit();
    
  }
   
  addMorePosts(){
    if(this.posts!=null){
    let p:Pagination;

    if(this.authService.logged&&this.authService.decodedToken.nameid!=null){

      this.pageNumber=this.pageNumber+1;
      this.postService.getPostsPaging("Home",this.pageNumber,2,this.sortBy,0,this.authService.decodedToken.nameid,0).subscribe(
        next=>{
 
          p=next.pagination;
          this.posts=(this.posts.concat(next.result));
          if(next.result.length==0){  
                this.continueLoading=false;      
          }
          else{
            this.showRedirictButton=false;
            this.continueLoading=true;  
  
          }
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
    this.postService.getPostsPaging("Home",this.pageNumber,2,this.sortBy,0,0,0).subscribe(
      next=>{
        
        p=next.pagination;
        this.posts=(this.posts.concat(next.result));
        if(next.result.length==0){  
          this.continueLoading=false;   }

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
     this.addMorePosts();
  
  }


  dirictMeToAll(){
    this.navigate.navigate(['/all']);

    }
}

