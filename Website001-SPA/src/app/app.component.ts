import { HttpClient } from '@angular/common/http';
import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Reaction } from './Models/Reaction.interface';
import { User } from './Models/Uesr.interface';
import { AuthService } from './services/auth.service';
import { ReactionService } from './services/reaction.service';
import { UserService } from './services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Website001-SPA';
  constructor(private reactionService:ReactionService, private http:HttpClient,private authService:AuthService,private userService:UserService){

  }
   helper = new JwtHelperService();

   public user:any={};

  ngOnInit(): void {
  
  
    this.loggedIn();
    
    this.reactionService.getReactions(0,0).subscribe(
      reactions=>{this.reactionService.reactions=reactions}
    );
     
    this.reactionService.numberOfReactions(0,0).subscribe(
      numberOfReactions=>{this.reactionService.reactionsNumber=numberOfReactions;}
    );
  }
   loggedIn(){ 

    if(localStorage.getItem("token")!=null){
      if(!this.helper.isTokenExpired(localStorage.getItem("token")))
      this.authService.logged=true;
      this.userService.getUser(+this.helper.decodeToken(localStorage.getItem('token')).nameid).subscribe(
        user=>{this.authService.currentUser=user;this.user=user;}

      );
    this.authService.decodedToken=this.helper.decodeToken(localStorage.getItem('token'));}

    else{
      this.authService.logged=false;
      this.authService.currentUser=null;
      }
  }

  

}
