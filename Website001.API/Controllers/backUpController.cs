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
    [Route("api/backUpContoller/{userId}")]
    
    public class backUpContoller:ControllerBase{
        private readonly IReactionRepo _db;

        public backUpContoller(IReactionRepo db)
        {
            _db = db;
        }
        
        [HttpDelete("deleteUserReactionsOnPost/{postId}")]
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

        [HttpGet("getUserReactionToPost/{postId}")]
        public async Task<ActionResult<Reaction>> getUserReactionToPost(int userId,int postId){
             if(userId!=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)){
                return Unauthorized("You are not the user");
            }

            Reaction reaction= await this._db.getUserReactionToPost(userId,postId);
            if(reaction==null){
                return NoContent();
            }

            return Ok(reaction);
        }
        [HttpGet("numberOfReactions")]
        public async Task<ActionResult> numberOfReactions(){
            
            return Ok(await this._db.numberOfReactions());
        }

        [HttpGet("isUserReactedToPost/{postId}")]
        public async Task<IActionResult> isUserReactedToPost(int userId,int postId){
             if(userId!=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)){
                return Unauthorized("You are not the user");
            }
            return Ok(await this._db.isUserReactedToPost(userId,postId));
        }
        [HttpGet("numberOfPostReaction")]
        public async Task<ActionResult> numberOfPostReaction(NumberOfPostReaction numberOfPostReaction){
            int number=await _db.numberOfPostReaction(numberOfPostReaction.postId,numberOfPostReaction.reactionId);
            return Ok(number);
        }
        [HttpGet("getReactions")]
        public async Task<ActionResult<List<Reaction>>> getReactions(){
            return Ok(await this._db.getReactions());
        }
        
        [HttpPost("addReactionToPost")]
        public async Task<IActionResult> addReactionToPost(int userId,PostReactionToAddDto postReactionToAddDto){
             
             if(userId!=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)){
                return Unauthorized("You are not the user");
            }
            

            await this._db.addReactionToPost(userId,postReactionToAddDto.postId,postReactionToAddDto.reactionId);
            if(!await _db.saveAll()){return BadRequest("Something bad happened");}
            return Ok("Added");
        }
        
    }
}