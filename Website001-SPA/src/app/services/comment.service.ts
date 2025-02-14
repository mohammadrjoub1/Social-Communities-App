import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  headers = { 'content-type': 'application/json'}   

  baseUrl=environment.url+"api/Comment/";
  constructor(private http:HttpClient) { }

  public add(commentDto:any):Observable<any>{
   return this.http.post<any>(this.baseUrl+"add",commentDto);
  }
  public getPostComments(postId:any):Observable<any>{
    let params =new HttpParams();
    params=params.append("postId",postId);
    return this.http.get<any[]>(this.baseUrl+"getPostComments",{observe:"response",params:params});
  }
  

  public getComment(commentId:any):Observable<any>{
    let params =new HttpParams();
    params=params.append("commentId",commentId);
    return this.http.get<any>(this.baseUrl+"getComment",{observe:"response",params:params});
  }
}
