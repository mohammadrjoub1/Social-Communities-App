import { Component, OnInit } from '@angular/core';
import { Categorie } from 'src/app/Models/Categorie.interface';
import { AlertifyService } from 'src/app/services/alertify.service';
import { CategorieService } from 'src/app/services/categorie.service';

@Component({
  selector: 'app-categories-list',
  templateUrl: './categories-list.component.html',
  styleUrls: ['./categories-list.component.css']
})
export class CategoriesListComponent implements OnInit {
  categories :Categorie[];
  topCat:any[];
  constructor(private categorieService:CategorieService,private alertify:AlertifyService) { }

  ngOnInit() {
    this.categorieService.getCategories().subscribe(
      categories=>{
        this.categories=categories;
      },
      error=>{
        this.alertify.error(error);
      }
    );
   this.categorieService.getTopCategories().subscribe(
      next=>{
        console.log(next);
      },
      error=>{
        console.log(error);
      }
    );
  }

}
