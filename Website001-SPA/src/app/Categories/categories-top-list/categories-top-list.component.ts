import { Component, OnInit } from '@angular/core';
import { CategorieService } from 'src/app/services/categorie.service';

@Component({
  selector: 'app-categories-top-list',
  templateUrl: './categories-top-list.component.html',
  styleUrls: ['./categories-top-list.component.css']
})
export class CategoriesTopListComponent implements OnInit {
  public categories:any=[];
 
  constructor(public categorieService:CategorieService) { }

  ngOnInit() {
    this.categorieService.getTopCategories().subscribe(
      next=>{
        this.categories=next.body;
        console.log(next);
 
      },
      error=>{
        console.log(error);
      }
    );
  }

}
