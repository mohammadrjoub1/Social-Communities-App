
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { CategorieJoiningDto } from '../Dtos/CategorieJoiningDto';
import { CategorieToAddDto } from '../Dtos/CategorieToAddDto';
import { categoriePictureDto } from '../Dtos/categoriePictureDto';
import { Categorie } from '../Models/Categorie.interface';
import { Post } from '../Models/Post.interface';

@Injectable({
  providedIn: 'root'
})
export class CategorieService {
  nowJoind="@#%!@#%:$:!@$:!@";
  baseUrl=environment.url+"api/categorie/";
  headers = { 'content-type': 'application/json'}   

  constructor(private http:HttpClient) { }

  getCategories():Observable<Categorie[]>{
    return this.http.get<Categorie[]>(this.baseUrl+"getCategories");
  }

  getGategoriePosts(id:number):Observable<Post[]>{
    return this.http.get<Post[]>(this.baseUrl+"getGategoriePosts/"+id);
  }

  getPostsByCategorieTitle(title:string):Observable<Post[]>{
    return this.http.get<Post[]>(this.baseUrl+"getPostsByCategorieTitle/"+title);
  }
  getCategorieByTitle(title:string):Observable<Categorie>{
    return this.http.get<Categorie>(this.baseUrl+"getCategorieByTitle/"+title);
  }
  getCategorie(id:number):Observable<Categorie>{
    return this.http.get<Categorie>(this.baseUrl+"getCategorie/"+id);
  }
  
  createCategorie(categorieToAddDto:CategorieToAddDto){
    const formData = new FormData();
    
       formData.append("title",""+categorieToAddDto.title);
      formData.append("about",categorieToAddDto.about);
      formData.append("date",""+categorieToAddDto.date);
      formData.append("image",categorieToAddDto.image);
      formData.append("cover",categorieToAddDto.cover);
      formData.append("userId",""+categorieToAddDto.userId);

    return this.http.post<any>(this.baseUrl+"createCategorie",formData,{observe: 'response'});
  }

  leaveCategorie(categorieJoiningDto:CategorieJoiningDto){
    let params = new HttpParams();
    params=params.append("userId",""+categorieJoiningDto.userId);
    params=params.append("categorieId",""+categorieJoiningDto.categorieId);
    return this.http.delete<boolean>(this.baseUrl+"leaveCategorie",{observe:"response",params:params}).pipe(
      map(response=>{
        return response.body;
      })
    );
  }
  isUserJoindToCategorie(categorieJoiningDto:CategorieJoiningDto):Observable<boolean>{
    let params = new HttpParams();
    params=params.append("userId",""+categorieJoiningDto.userId);
    params=params.append("categorieId",""+categorieJoiningDto.categorieId);
    return this.http.get<boolean>(this.baseUrl+"isUserJoindToCategorie",{observe:"response",params:params}).pipe(
      map(response=>{
        return response.body;
      })
    ); 
  }
  joinCategorie(categorieJoiningDto:CategorieJoiningDto):Observable<boolean>{
    return this.http.post<boolean>(this.baseUrl+"joinCategorie",categorieJoiningDto);
  }
  getNumberOfMembersOfCategorie(categorieId:number):Observable<number>{
    return this.http.get<number>(this.baseUrl+"getNumberOfMembersOfCategorie/"+categorieId).pipe(
      response=>{
        return response;
      }
    );
  }

  addCategorieProfilePicture(categoriePictureDto:categoriePictureDto):Observable<any>{
    const formData = new FormData();
    
    formData.append("file",categoriePictureDto.file);
    formData.append("userId",""+categoriePictureDto.userId);
    formData.append("categorieId",""+categoriePictureDto.categorieId);
    return this.http.put(this.baseUrl+"addCategorieProfilePicture",formData,{responseType: 'text' });   }
  
    addCategorieCoverPicture(categoriePictureDto:categoriePictureDto):Observable<any>{
      const formData = new FormData();
      
      formData.append("file",categoriePictureDto.file);
      formData.append("userId",""+categoriePictureDto.userId);
      formData.append("categorieId",""+categoriePictureDto.categorieId);
      return this.http.put(this.baseUrl+"addCategorieCoverPicture",formData,{responseType: 'text' });   }
  

    getTopCategories():Observable<any>{
        return this.http.get<any>(this.baseUrl+"getTopCategories",{observe:"response"});
      }

  subject = new BehaviorSubject(this.nowJoind);


}
