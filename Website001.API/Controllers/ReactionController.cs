using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website001.API.Data;
using Website001.API.Dtos;
using Website001.API.Models;

namespace Website001.API.Controllers{
    [Authorize]
    [ApiController]
    [Controller]
    [Route("api/reaction/{userId}/post/{postId}")]
    
    public class ReactionController:ControllerBase{
        private readonly IReactionRepo _db;

        public ReactionController(IReactionRepo db)
        {
            _db = db;
        }
        
        [HttpDelete("deleteUserReactionsOnPost")]
        public async Task<ActionResult> deleteUserReactionsOnPost(int userId,int postId){
             if(userId!=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)){
                return Unauthorized("You are not the user");
            }
            bool result=await this._db.deleteUserReactionsOnPost(userId,postId);
            if(!result){
                return BadRequest("Couldn't delete reaction");
            }
            if(!await _db.saveAll()){
                return BadRequest("Couldn't Save Action :[deleteUserReactionsOnPost]");
            }

            return Ok("Deleted");
        }
        [AllowAnonymous]

        [HttpGet("getUserReactionToPost")]
        public async Task<ActionResult<Reaction>> getUserReactionToPost(int userId,int postId){
             if(userId!=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)){
                return Unauthorized("You are not the user");
            }

            Reaction reaction= await this._db.getUserReactionToPost(userId,postId);
            if(reaction==null){
                return NoContent();
            }

            return Ok(reaction);
        }[AllowAnonymous]
        [HttpGet("numberOfReactions")]
        public async Task<ActionResult> numberOfReactions(){
            
            return Ok(await this._db.numberOfReactions());
        }

        [HttpGet("isUserReactedToPost")]
        public async Task<IActionResult> isUserReactedToPost(int userId,int postId){
             if(userId!=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)){
                return Unauthorized("You are not the user");
            }
            return Ok(await this._db.isUserReactedToPost(userId,postId));
        }
        [AllowAnonymous]
        [HttpGet("numberOfPostReaction/{reactionId}")]
        public async Task<ActionResult> numberOfPostReaction(int userId,int postId,int reactionId){
            int number=await _db.numberOfPostReaction(postId,reactionId);
            return Ok(number);
        }
        [AllowAnonymous]
        [HttpGet("getReactions")]
        public async Task<ActionResult<List<Reaction>>> getReactions(){
            return Ok(await this._db.getReactions());
        }
        
        [HttpPost("addReactionToPost")]
        public async Task<IActionResult> addReactionToPost(int userId,int postId,PostReactionToAddDto postReactionToAddDto){
             
             if(userId!=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)){
                return Unauthorized("You are not the user");
            }
            

            await this._db.addReactionToPost(userId,postReactionToAddDto.postId,postReactionToAddDto.reactionId);
            if(!await _db.saveAll()){return BadRequest("Something bad happened");}
            return Ok("Added");
        }

         [HttpPost("addReactionByNameToPost")]
        public async Task<IActionResult> addReactionByName(int userId,int postId,PostReactionByNameToAddDto postReactionByNameToAddDto){

             if(userId!=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)){
                return Unauthorized("You are not the user");
            }
            

            await this._db.addReactionByNameToPost(userId,postReactionByNameToAddDto.postId,postReactionByNameToAddDto.reactionName);
            if(!await _db.saveAll()){return BadRequest("Something bad happened");}
            return Ok(postReactionByNameToAddDto.reactionName+" was Added");
        }
        
    }
}