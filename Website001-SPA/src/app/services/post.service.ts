import { HttpClient, HttpEvent, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
  import { Observable, pipe } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PostToAddDto } from '../Dtos/PostToAddDto';
import { Pagination, PaginationResult } from '../Models/Pagination';
import { Post } from '../Models/Post.interface';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  baseUrl=environment.url+"api/post/"
  constructor(private http:HttpClient) { }
 
   headers = { 'content-type': 'application/json'}   

  getPost(id:number):Observable<any>{
    return this.http.get<any>(this.baseUrl+"getPost/"+id);
  }
  getUserPosts(id:number):Observable<any[]>{
    return this.http.get<any[]>(this.baseUrl+"getUserPosts/"+id);
  }
  

  addPost(userId:number,postToAddDto:PostToAddDto,postTypeId:number){
    const formData = new FormData();
    
       formData.append("categorieId",""+postToAddDto.categorieId);
      formData.append("content",postToAddDto.content);
      formData.append("postTypeId",""+postToAddDto.postTypeId);
      formData.append("title",postToAddDto.title);
      formData.append("image",postToAddDto.image);
    return this.http.post<any>(this.baseUrl+"add/"+userId,formData,{observe: 'response'});
  }

  getPostsPaging(pagingAplicant?,pageNumber?,PageSize?,sortBy?,categorieId?,userId?,userToViewId?):Observable<PaginationResult<Post[]>>{
    let params =new HttpParams();
    const pagination:PaginationResult<any[]> = new PaginationResult<any[]>();

    params=params.append("pagingAplicant",pagingAplicant);
    params=params.append("pageNumber",pageNumber);
    params=params.append("PageSize",PageSize);
    params=params.append("sortBy",sortBy);
    params=params.append("categorieId",categorieId);
    params=params.append("userId",userId);
    params=params.append("userToViewId",userToViewId);
    return this.http.get<any[]>(this.baseUrl+"getPostsPaging",{observe:"response",params:params}).pipe(
      map(response=>{
        pagination.result=response.body;
        pagination.pagination=JSON.parse(response.headers.get('Pagination'));
        return pagination;
         
      })
    )
  }
}
