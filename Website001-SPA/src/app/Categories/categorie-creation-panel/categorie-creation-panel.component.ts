import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-categorie-creation-panel',
  templateUrl: './categorie-creation-panel.component.html',
  styleUrls: ['./categorie-creation-panel.component.css']
})
export class CategorieCreationPanelComponent implements OnInit {

  constructor(private navigate: Router,public authService:AuthService) { }

  ngOnInit() {
  }
  createCategorie(){

    this.navigate.navigate(['categories/create']);
     
    }
}
