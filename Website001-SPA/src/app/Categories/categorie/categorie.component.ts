import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Categorie } from 'src/app/Models/Categorie.interface';
import { Pagination } from 'src/app/Models/Pagination';
import { Post } from 'src/app/Models/Post.interface';
import { AlertifyService } from 'src/app/services/alertify.service';
import { AuthService } from 'src/app/services/auth.service';
import { CategorieService } from 'src/app/services/categorie.service';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-categorie',
  templateUrl: './categorie.component.html',
  styleUrls: ['./categorie.component.css']
})
export class CategorieComponent implements OnInit {
  categoriePictureDto:any={};
  showSpiner=false;
  user:any={};

  public posts:any[];
  constructor(private navigate: Router,public postService:PostService,public authService:AuthService,private router:ActivatedRoute,private alertify:AlertifyService ,private categorieService:CategorieService) { }

  public title:string=this.router.snapshot.paramMap.get("title");
  public categorie:any={}
  public numberOfMembersOfCategorie:number;
  public isUserJoined:boolean;
  public categorieJoiningDto:any={};
  public sortBy="Top";
  public PageSize=2;
  public pageNumber=1;
  public categorieId;  
  public continueLoading=true;

  ngOnInit() {
    this.pageNumber=1;

    this.categorieService.getCategorieByTitle(this.title).subscribe(
      next=>{
        this.addPosts();
        this.categorie=next;
        this.categorieId=next.id;
        this.categorieService.getNumberOfMembersOfCategorie(next.id).subscribe(
          next=>{
            if(next!=null){
              this.numberOfMembersOfCategorie=next;

            }

          }
        );
        if(this.authService.logged){
          this.user=this.authService.currentUser;
          this.categorieJoiningDto.userId=this.authService.decodedToken.nameid;
          this.categorieJoiningDto.categorieId=this.categorie.id;
          this.categorieService.isUserJoindToCategorie(this.categorieJoiningDto).subscribe(
            next=>{
              this.isUserJoined=next;
            }
          );
        }
      },
      error=>{
        this.navigate.navigate(['/']);

      }
    );
 
    
    // this.categorieService.getPostsByCategorieTitle(this.title).subscribe(
    //   posts=>{
        
    //     this.posts=posts;
    //   },
    //   error=>{this.alertify.error(error);}
    // );

    
  }
  addPosts(){ 

    this.pageNumber=1;
    this.categorieService.getCategorieByTitle(this.title).subscribe(
      next=>{
        this.postService.getPostsPaging("Categorie",this.pageNumber,this.PageSize,this.sortBy,next.id,0,0).subscribe(
          next=>{
                      if(next[0]==null){  this.continueLoading=false;   }

            if(next.result!=null){
            this.posts=next.result;}
            else{
              this.continueLoading=false;
            }
          
    
          },
          error=>{
            this.alertify.error("something went wrong,enternet is down");
          }
        );
      });
   
  }
  addMorePosts(){
    if(this.post!=null){

    this.categorieService.getCategorieByTitle(this.title).subscribe(
      next=>{
        this.categorie=next;
        this.categorieId=next.id;

        this.pageNumber=this.pageNumber+1;

        this.postService.getPostsPaging("Categorie",this.pageNumber,this.PageSize,this.sortBy,next.id,0,0).subscribe(
          next=>{
            let p:Pagination;
            if(next[0]==null){  this.continueLoading=false;   }

            p=next.pagination;
            this.posts=(this.posts.concat(next.result));
            if(p.totalPages==p.pageNumber){
              this.continueLoading=false;
            }
    
          },
          error=>{
            this.alertify.error("something went wrong,enternet is down");
          }
        );
      }
        );
    }
  }
  joinCategorie(){
    this.categorieService.joinCategorie(this.categorieJoiningDto).subscribe(
      next=>{this.isUserJoined=true;
    this.alertify.success("Joind");
    this.categorieService.getNumberOfMembersOfCategorie(this.categorie.id).subscribe(
      next=>this.numberOfMembersOfCategorie=next
    );
  }
    );
   
  }
  leaveCategorie(){
    this.categorieService.leaveCategorie(this.categorieJoiningDto).subscribe(
      next=>{this.isUserJoined=false;
      this.alertify.success("Left");
      this.categorieService.getNumberOfMembersOfCategorie(this.categorie.id).subscribe(
        next=>{this.numberOfMembersOfCategorie=next;}
      );
    } );

   
  }

  post(){

  this.navigate.navigate(['/posts/add',this.title]);
   
  }
  onScroll() {    
    this.addMorePosts();}
 

    onFilesSelectedProfilePicture(event){
      this.categoriePictureDto.userId=this.user.id;
      this.categoriePictureDto.file=event.target.files[0];
      this.categoriePictureDto.categorieId=this.categorieId;

        console.log(  this.categoriePictureDto.file+"  this.data.file");
        this.showSpiner=true;
  
      this.categorieService.addCategorieProfilePicture(this.categoriePictureDto).subscribe(
        next=>{
          this.categorie.imageUrl=next;
          this.showSpiner=false; }
      )
    }
    onFilesSelectedCoverPicture(event){
      this.categoriePictureDto.userId=this.user.id;
      this.categoriePictureDto.file=event.target.files[0];
      this.categoriePictureDto.categorieId=this.categorieId;

        console.log(  this.categoriePictureDto.file+"  this.data.file");
        this.showSpiner=true;
  
      this.categorieService.addCategorieCoverPicture(this.categoriePictureDto).subscribe(
        next=>{
          this.categorie.coverUrl=next;
          this.showSpiner=false; }
      )
    }
}
