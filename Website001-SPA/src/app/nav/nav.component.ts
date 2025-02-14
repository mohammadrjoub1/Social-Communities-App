import { HttpClient } from '@angular/common/http';
import { ChangeDetectionStrategy, Component, Input, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from '../Models/Uesr.interface';
import { AlertifyService } from '../services/alertify.service';
import { AuthService } from '../services/auth.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
   @Input() user:User;
  userFromAuth:User;
   panelOpenState = true;
   nowIn="home";
   nowInsss="homess";
   showLoginForm=false;

  constructor(private route:Router,public fb: FormBuilder,private userService:UserService, private http:HttpClient,private authService:AuthService,private alertifyService:AlertifyService) { }
  public model:any={};
  loggedIn=false;
  menuShow=false;
  oppoSuits: any = ['Home', 'All']
  ngOnInit() {
    this.loggedIn=this.authService.logged; 
     
   }
   oppoSuitsForm = this.fb.group({
    name: ['']
  })
  onChange(e:any) {
     if(e.target.value=="Home"){
      this.route.navigate(['/']);

     }
      if(e.target.value=="All"){
        this.route.navigate(['/all']);

     }
     if(e.target.value=="Logout"){
       this.logout();
     }
  }
  login(){
    this.authService.login(this.model).subscribe(
      next=>{
        this.alertifyService.success("loggedin successfully");
        this.loggedIn=true;
        this.userService.getUserByUsername(this.model.username).subscribe(
          next=>{
            this.authService.currentUser=next;
            this.userFromAuth=next;
            this.user=next;
          })
        
   

      },
      error=>{
        this.alertifyService.error("mismatch");
        this.loggedIn=false;

      }
    );
  }
  logout(){
    
    this.authService.logout();
    this.authService.currentUser=null;
    this.alertifyService.error("logged out");
    this.loggedIn=false;
  }

 
  
  goToMyProfile(){
    this.route.navigate(['/user/'+this.user.username]);
    }
  goToHome(){
    this.nowIn="home"

      this.route.navigate(['/']);
      }
  goToAll(){
    this.nowIn="all"
        this.route.navigate(['/all']);
        }

  showTheLoginForm(){
    this.showLoginForm=true;
  }
}
