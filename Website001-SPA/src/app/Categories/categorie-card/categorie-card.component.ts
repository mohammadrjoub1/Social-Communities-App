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
  selector: 'app-categorie-card',
  templateUrl: './categorie-card.component.html',
  styleUrls: ['./categorie-card.component.css']
})
export class CategorieCardComponent implements OnInit {

  @Input() categorie:Categorie;
  categoriePictureDto:any={};
  showSpiner=false;
  user:any={};

  public posts:any[];
  constructor(private navigate: Router,public postService:PostService,public authService:AuthService,private router:ActivatedRoute,private alertify:AlertifyService ,private categorieService:CategorieService) { }

  public numberOfMembersOfCategorie:number;
  public isUserJoined:boolean;
  public categorieJoiningDto:any={};
  public sortBy="Top";
  public PageSize=2;
  public pageNumber=1;
  public categorieId;  
  public continueLoading=true;

  ngOnInit() {
 
   
        this.categorieService.getNumberOfMembersOfCategorie(this.categorie.id).subscribe(
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
              this.isUserJoined=next;})};
     
    
 
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

}
