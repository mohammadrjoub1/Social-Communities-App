
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FileHolder } from 'angular2-image-upload';
import { PostToAddDto } from 'src/app/Dtos/PostToAddDto';
import { Categorie } from 'src/app/Models/Categorie.interface';
import { AlertifyService } from 'src/app/services/alertify.service';
import { AuthService } from 'src/app/services/auth.service';
import { CategorieService } from 'src/app/services/categorie.service';
import { PostService } from 'src/app/services/post.service';
import { FileUploader } from 'ng2-file-upload';
import { HttpEventType } from '@angular/common/http';
import { Post } from 'src/app/Models/Post.interface';
@Component({
  selector: 'app-post-add',
  templateUrl: './post-add.component.html',
  styleUrls: ['./post-add.component.css']
})
export class PostAddComponent implements OnInit {
   constructor(private route:Router,private router:ActivatedRoute, private categorieService:CategorieService,private postService:PostService, private authService:AuthService,private alertify:AlertifyService) { }
  id=0;
  postToAddDto:any={};
  destinationTitle="On My wall";
  postTypeId=1;
  selectedFiles:File=null;
  postId=0;
  progress=0;
  post:Post;
  clicked =0;
  imgURL:any;
  onFilesSelected(event){
    this.selectedFiles=<File>event.target.files[0];
    console.log(event.target.files[0]);
    var reader = new FileReader();
    reader.readAsDataURL(event.target.files[0]);
    reader.onload = (_event) => { 
      this.imgURL = reader.result; 
    }
  }
  
  ngOnInit() {

     
        this.categorieService.getCategorieByTitle(this.router.snapshot.paramMap.get("title")).subscribe(next=>{
          this.destinationTitle=next.title;
          this.postToAddDto.categorieId=next.id;
            
        },error=>{
          this.alertify.error(error);
        });
  }

  addPost(){        
    this.clicked=1;

    if(this.postTypeId==1){    
      this.postToAddDto.image=" ";
      this.postToAddDto.postTypeId=1;
}
    if(this.postTypeId==2){
      this.postToAddDto.content=" ";
      this.postToAddDto.postTypeId=2;
       this.postToAddDto.image=this.selectedFiles;
    }

    this.postService.addPost(+this.authService.decodedToken.nameid,this.postToAddDto,2).subscribe(resp => {
      this.route.navigate(['/categories/'+this.destinationTitle+'/post/'+resp.body.num]);

    });

  }
 
 
  
}
