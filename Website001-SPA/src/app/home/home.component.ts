import { HttpClient } from '@angular/common/http';
import { ChangeDetectionStrategy,ViewEncapsulation, Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
  

})
export class HomeComponent implements OnInit {
  background="green";
  encapsulation: ViewEncapsulation.None;
  constructor(private authService:AuthService) { }
  model:any={};
  ngOnInit() {

  }

}
