using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Website001.API.Models;

namespace Website001.API.Data{
    public class ReactionRepo : IReactionRepo
    {
        private readonly DataContext _db;

        public ReactionRepo(DataContext db)
        {
            this._db = db;
        }
        
        public async Task<int> numberOfPostReaction(int postId,int reactionId){
            if(!await this._db.reactions.AnyAsync(x=>x.id==reactionId)){
               if(!await this._db.posts.AnyAsync(x=>x.id==postId)){

               return 0;
               }
            }
            List<ReactionSet> reactionSet= await _db.reactionSets.Where(x=>(x.postId==postId)&&(x.reactionId==reactionId)).ToListAsync();
               int nunberOfReactionOnPost=reactionSet.ToArray<ReactionSet>().Length;
               return nunberOfReactionOnPost;

        }
        public async Task<bool> isUserReactedToPost(int userId,int postId){
           bool answer=await this._db.reactionSets.Where(x=>((x.postId==postId)&&(x.userId==userId))).AnyAsync();
           return answer;
        }
         public async Task<Reaction> getUserReactionToPost(int userId,int postId){
            if(!await this._db.users.AnyAsync(x=>x.id==userId)){
               if(!await this._db.posts.AnyAsync(x=>x.id==postId)){
                     return null;
               }
            }
            ReactionSet reactionSet = new ReactionSet();
            reactionSet=await this._db.reactionSets.Include(x=>x.reaction).FirstOrDefaultAsync(x=>((x.postId==postId)&&(x.userId==userId)));
           Reaction reaction = new Reaction();
           if(reactionSet==null){return null;}
           reaction= reactionSet.reaction;
           return reaction;
        }
        public async Task<bool> deleteUserReactionsOnPost(int userId,int postId){
            if(!await this._db.posts.AnyAsync(x=>x.id==postId)){
               if(!await this._db.users.AnyAsync(x=>x.id==userId)){
                  return false;
               }}
             ReactionSet reactionSet = await _db.reactionSets.FirstOrDefaultAsync(x=>((x.userId==userId)&&(x.postId==postId)));
            if(reactionSet==null){
               return false;
            }
            _db.Remove(reactionSet);
           return true;
        }
        
        public async Task addReactionToPost(int userId,int postId,int reactionId)
        {
         if(await this._db.reactions.AnyAsync(x=>x.id==reactionId)){
            if(await this._db.posts.AnyAsync(x=>x.id==postId)){
               if(await this._db.users.AnyAsync(x=>x.id==userId)){

                  ReactionSet reactionSet = new ReactionSet();
                  Reaction reaction = new Reaction();
                  
                  reactionSet.postId=postId;
                  reactionSet.reactionId=reactionId;
                  reactionSet.userId=userId;
                  reactionSet.date=DateTime.Now;
                  if(!this._db.reactionSets.Any(x=>((x.postId==postId)&&(x.userId==userId)))){
                     reactionSet.reaction= await this._db.reactions.FirstOrDefaultAsync(x=>x.id==reactionId);
                     this._db.Add(reactionSet);
                  }}
               }
            }
         }
         

        public async Task<bool> saveAll()
        {
           return await  this._db.SaveChangesAsync()>0;
        }

        public async Task<List<Reaction>> getReactions()
        {
            return await this._db.reactions.ToListAsync();
        }
          public async Task<int> numberOfReactions()
        {
            List<Reaction> reactions= await this._db.reactions.ToListAsync();
            if(reactions==null){return 0;}
            int number =reactions.ToArray<Reaction>().Length;
            return number;
            }

        public async Task addReactionByNameToPost(int userId, int postId, string reactionName)
        {           

            if(await this._db.reactions.AnyAsync(x=>x.name==reactionName)){
            if(await this._db.posts.AnyAsync(x=>x.id==postId)){
               if(await this._db.users.AnyAsync(x=>x.id==userId)){

                  ReactionSet reactionSet = new ReactionSet();
                  Reaction reaction = new Reaction();
                  Reaction reactionToGetId=await this._db.reactions.FirstOrDefaultAsync(x=>x.name==reactionName);
                  reactionSet.postId=postId;
                  reactionSet.reactionId=reactionToGetId.id;
                  reactionSet.userId=userId;
                  reactionSet.date=DateTime.Now;

                  if(!this._db.reactionSets.Any(x=>((x.postId==postId)&&(x.userId==userId)&&(x.reaction.name== reactionName)))){
                    
                     reactionSet.reaction= await this._db.reactions.FirstOrDefaultAsync(x=>x.id==reactionToGetId.id);
                     this._db.Add(reactionSet);
                  }
             
                  if(this._db.reactionSets.Any(x=>((x.postId==postId)&&(x.userId==userId)&&(x.reaction.name== reactionName)))){
                    
                     await this.deleteUserReactionsOnPost(userId,postId);
                     reactionSet.reaction= await this._db.reactions.FirstOrDefaultAsync(x=>x.id==reactionToGetId.id);
                  }
                  else  if(this._db.reactionSets.Any(x=>((x.postId==postId)&&(x.userId==userId)))){
                     await this.deleteUserReactionsOnPost(userId,postId);
                     reactionSet.reaction= await this._db.reactions.FirstOrDefaultAsync(x=>x.id==reactionToGetId.id);
                     this._db.Add(reactionSet);
                  }
                  } 
               }
            }        }


        public async Task addReactionByNameToPostStric(int userId, int postId, string reactionName)
        {           

            if(await this._db.reactions.AnyAsync(x=>x.name==reactionName)){
            if(await this._db.posts.AnyAsync(x=>x.id==postId)){
               if(await this._db.users.AnyAsync(x=>x.id==userId)){

                  ReactionSet reactionSet = new ReactionSet();
                  Reaction reaction = new Reaction();
                  Reaction reactionToGetId=await this._db.reactions.FirstOrDefaultAsync(x=>x.name==reactionName);
                  reactionSet.postId=postId;
                  reactionSet.reactionId=reactionToGetId.id;
                  reactionSet.userId=userId;
                  reactionSet.date=DateTime.Now;

                  if(!this._db.reactionSets.Any(x=>((x.postId==postId)&&(x.userId==userId)&&(x.reaction.name==reactionName)))){
                    
                     
                     reactionSet.reaction= await this._db.reactions.FirstOrDefaultAsync(x=>x.id==reactionToGetId.id);
                     this._db.Add(reactionSet);
                  }} 
               }
            }        }
    }
}