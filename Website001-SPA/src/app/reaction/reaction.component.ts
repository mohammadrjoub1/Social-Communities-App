import { Component, Input, OnInit } from '@angular/core';
import { NumberOfPostReaction } from '../Dtos/NumberOfPostReaction';
import { PostReactionToAddDto } from '../Dtos/PostReactionToAddDto';
import { AlertifyService } from '../services/alertify.service';
import { AuthService } from '../services/auth.service';
import { ReactionService } from '../services/reaction.service';

@Component({
  selector: 'app-reaction',
  templateUrl: './reaction.component.html',
  styleUrls: ['./reaction.component.css']
})
export class ReactionComponent implements OnInit {
  @Input() postId:number;
  constructor(private reactionService:ReactionService,private authService:AuthService,private alertify:AlertifyService) { }
  reaction:any={};
  numberOfReactions=this.reactionService.reactionsNumber;
  reactions=this.reactionService.reactions;
  postReactionByNameToAddDto:any={};
  numberOfReactionsOnThePost:any={};
  reactionsnumberonthepost=0;  
reactionsnumberonthepostsss=0;
  arrayOfReacion:any={};
  array1:any=[];
  array2:any=[];
  array3:any=[];
  
  reactionId=1;
  length=0;       
   numOfReacts=0;

  a="das";
  reactedTo="sadas";
  ngOnInit() {
    

    this.theWash();
    }
    theWash(){

      this.reactionService.getReactions(0,this.postId).subscribe(
        x=>{
  
          this.length=x.length;
        
      for (let i = 0; i < x.length; i++) {
        this.array1[i]=x[i].id; 
        this.array2[i]=x[i].name;
        
        this.reactionService.numberOfPostReaction(0,this.postId,x[i].id).subscribe(
          s=>{ this.array3[i]=s}   );}
          this.a="dasdasdsaas";
         
        }
      )
      if(this.loggedIn()){
      this.reactionService.getUserReactionToPost(+this.authService.decodedToken.nameid,this.postId).subscribe(
        reaction=>{if(reaction!=null)this.reactedTo=reaction.name}
      ); }
      this.numberOfReactionsOnThePost.reactionId=this.reactionId;
      this.numberOfReactionsOnThePost.postId=this.postId; 
      this.reactionService.numberOfPostReaction(0,this.postId,this.reactionId).subscribe(
        result=>{this.reactionsnumberonthepost=result;}
      );
  
    }
    numberOfPostReaction(reactionId:number){
      this.numberOfReactionsOnThePost.reactionId=reactionId;
      this.numberOfReactionsOnThePost.postId=this.postId; 
      this.reactionService.numberOfPostReaction(0,this.postId,reactionId).subscribe(
        result=>{this.reactionsnumberonthepost=result;}
      );

    }
    loggedIn(){
      return this.authService.logged;
    }
  react(reactionName:string){
    if(this.loggedIn()){
       this.reactedTo=reactionName;
        }
    this.postReactionByNameToAddDto.reactionName=reactionName;
    this.postReactionByNameToAddDto.postId=this.postId;
   
 
     this.reactionService.addReactionByNameToPost((+this.authService.decodedToken.nameid),this.postId,this.postReactionByNameToAddDto).subscribe(
      next=>{
        
  this.reactionService.getReactions(+this.authService.decodedToken.nameid,this.postId).subscribe(
    x=>{

    
  for (let i = 0; i < x.length; i++) {
    
    this.reactionService.numberOfPostReaction(this.authService.decodedToken.nameid,this.postId,x[i].id).subscribe(
      s=>{ this.array3[i]=s});}
    if(this.loggedIn()){
      this.reactionService.getUserReactionToPost(+this.authService.decodedToken.nameid,this.postId).subscribe(
        reaction=>{if(reaction!=null)this.reactedTo=reaction.name;
                  else{this.reactedTo="null"}}
      ); }
    }
  )

      },
      error=>{
        this.alertify.error(error);
      }

    ) 
  


}

}
