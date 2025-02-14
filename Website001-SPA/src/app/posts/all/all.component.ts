import { Component, OnInit } from '@angular/core';
import { Pagination } from 'src/app/Models/Pagination';
import { AlertifyService } from 'src/app/services/alertify.service';
import { AuthService } from 'src/app/services/auth.service';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-all',
  templateUrl: './all.component.html',
  styleUrls: ['./all.component.css']
})
export class AllComponent implements OnInit {  posts:any[];
  pageNumber=1;
  pageSize=2;
  continueLoading=true;
  showJoiningToCategorieOption=false;
  constructor(private postService:PostService,private alertifi:AlertifyService,private authService:AuthService) { }
  dontDeleteMine=false;
  sortBy="Top";
  ngOnInit() {
    this.pageNumber=1;

    if(this.authService.logged)    this.showJoiningToCategorieOption=true;
      this.postService.getPostsPaging("Home",1,2,this.sortBy,0,0,0).subscribe(
        next=>{
          this.posts=next.result;
          if(next[0]==null){  this.continueLoading=false;   }


        },
        error=>{
          console.log(error);
        }
      );
   
  }

  addPosts(){
    this.pageNumber=1;

    if(this.authService.logged)    this.showJoiningToCategorieOption=true;
      this.postService.getPostsPaging("Home",1,2,this.sortBy,0,0,0).subscribe(
        next=>{
          this.posts=next.result;
          if(next[0]==null){  this.continueLoading=false;   }

        },
        error=>{
          console.log(error);
        }
      );
  
  }
  addMorePosts(){
    if(this.posts!=null){
    let p:Pagination;

 
    this.pageNumber=this.pageNumber+1;
    this.postService.getPostsPaging("Home",this.pageNumber,2,this.sortBy,0,0,0).subscribe(
      next=>{
        
        p=next.pagination;
        this.posts=(this.posts.concat(next.result));
        if(next[0]==null){  this.continueLoading=false;   }

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
 
  onScroll() {    
    this.addMorePosts(); }

}
