import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../Models/Uesr.interface';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl=environment.url+"api/user/";
  logged=false;;
  decodedToken=null;
  currentUser:User=null ;
  helper = new JwtHelperService();

  constructor(private http:HttpClient) {

   }

   login(model:any={}){
    return this.http.post(this.baseUrl+"login",model).pipe(
      map((theToken:any)=>{
        if(theToken!=null){
          const token=theToken.token;
          localStorage.setItem("token",token);
          
          this.logged=true;

          this.decodedToken=this.helper.decodeToken(localStorage.getItem('token'));
        }
        else{
          this.decodedToken=null;
          this.logged=false;
          this.currentUser=null;
          localStorage.removeItem("token");
        }
      })
    );}


   register(model={}){
    return this.http.post(this.baseUrl+"register",model);}

 
  logout(){
    this.decodedToken=null;
    this.logged=false;
    this.currentUser=null;
    localStorage.removeItem("token");
    
  }
  
}


