import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService } from 'src/app/services/alertify.service';
import { AuthService } from 'src/app/services/auth.service';
import { CategorieService } from 'src/app/services/categorie.service';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-categorie-create',
  templateUrl: './categorie-create.component.html',
  styleUrls: ['./categorie-create.component.css']
})
export class CategorieCreateComponent implements OnInit {
  selectedFiles1:File=null;
  selectedFiles2:File=null;
  id=0;
  categorieToAdd:any={};
  destinationTitle="On My wall";
   selectedFiles:File=null;
  postId=0;
  progress=0;
  clicked =0;
  title: any;
  about: any;
  date: any;
   constructor(private route:Router,private router:ActivatedRoute, private categorieService:CategorieService,private postService:PostService, private authService:AuthService,private alertify:AlertifyService) { }
   onFilesSelected(event){
    if(this.selectedFiles1==null) this.selectedFiles1=<File>event.target.files[0];
    if(this.selectedFiles2==null) this.selectedFiles2=<File>event.target.files[1];
     console.log(event.target.files[0]);
    console.log(event.target.files[1]);
  }
 
  
  ngOnInit() {
  }

  createCategorie(){        
    this.clicked=1;
 
    this.categorieToAdd.title=this.title;
    this.categorieToAdd.about=this.about;
    this.categorieToAdd.date=this.date;
    this.categorieToAdd.image=this.selectedFiles1;
    this.categorieToAdd.cover=this.selectedFiles2;
    this.categorieToAdd.userId=+this.authService.decodedToken.nameid;
 
    

    this.categorieService.createCategorie(this.categorieToAdd).subscribe(resp => {
      this.route.navigate(['/categories/'+resp.body.categorieTitle]);

    });

  }
}
