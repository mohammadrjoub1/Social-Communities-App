import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { profilePictureDto } from '../Dtos/profilePictureDto';
import { User } from '../Models/Uesr.interface';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private baseUrl=environment.url+"api/user/";
  constructor(private authService:AuthService,private http:HttpClient) { }
  headers = { 'content-type': 'application/json'}   

  
  getUser(id:number):Observable<User>{
    return this.http.get<User>(this.baseUrl+"getUser/"+id);
  }
  getUserByUsername(username:string):Observable<User>{
    return this.http.get<User>(this.baseUrl+"getUserByUsername/"+username);
  }
  register(userDto:any={}):Observable<any>{
      return this.http.post(this.baseUrl+"register",userDto,{'headers':this.headers, responseType: 'text' });

  }
  addUserProfilePicture(profilePictureDto:profilePictureDto):Observable<any>{
    const formData = new FormData();
    
    formData.append("file",profilePictureDto.file);
    formData.append("userId",""+profilePictureDto.userId);
    return this.http.put(this.baseUrl+"addUserProfilePicture",formData,{responseType: 'text' });   }
}
