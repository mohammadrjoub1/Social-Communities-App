import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { NumberOfPostReaction } from '../Dtos/NumberOfPostReaction';
import { PostReactionByNameToAddDto } from '../Dtos/PostReactionByNameToAddDto';
import { PostReactionToAddDto } from '../Dtos/PostReactionToAddDto';
import { Reaction } from '../Models/Reaction.interface';

@Injectable({
  providedIn: 'root'
})
export class ReactionService {
  public reactions:Reaction[];  
  public reactionsNumber:number;

  baseUrl=environment.url+ "api/reaction/"
  constructor(private http:HttpClient) { }
   headers = { 'content-type': 'application/json'}   
  addReactionToPost(userId:number,postId:number,postReactionToAddDto:PostReactionToAddDto){
    return this.http.post((this.baseUrl+userId+"/post/"+postId+"/addReactionToPost"),postReactionToAddDto);
  }
  addReactionByNameToPost(userId:number,postId:number,postReactionToAddDto:PostReactionByNameToAddDto){
    return this.http.post((this.baseUrl+userId+"/post/"+postId+"/addReactionByNameToPost"),postReactionToAddDto,{'headers':this.headers, responseType: 'text' });
  }
  
  numberOfPostReaction(userId:number,postId:number,reactionId:number):Observable<number>{

    return this.http.get<number>(this.baseUrl+userId+"/post/"+postId+"/numberOfPostReaction/"+reactionId);
  }
  
  isUserReactedToPost(userId:number,postId:number):Observable<boolean>{
    return this.http.get<boolean>(this.baseUrl+userId+"/post/"+postId+"/isUserReactedToPost/");
  }
  getUserReactionToPost(userId:number,postId:number):Observable<Reaction>{
    
    return this.http.get<Reaction>(this.baseUrl+userId+"/post/"+postId+"/getUserReactionToPost");
  }

  deleteUserReactionsOnPost(userId:number,postId:number):Observable<boolean>{
    return this.http.delete<boolean>(this.baseUrl+userId+"/post/"+postId+"/deleteUserReactionsOnPost");
  }
  numberOfReactions(userId:number,postId:number):Observable<number>{
    return this.http.get<number>(this.baseUrl+userId+"/post/"+postId+"/numberOfReactions");
  } 
  getReactions(userId:number,postId:number):Observable<Reaction[]>{
    return this.http.get<Reaction[]>(this.baseUrl+userId+"/post/"+postId+"/getReactions");
  }

}
