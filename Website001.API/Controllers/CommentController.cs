using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website001.API.Data;
using Website001.API.Dtos;

namespace Website001.API.Controllers{
    [Authorize]
    [ApiController]
    [Controller]
    [Route("api/Comment")]
    public class CommentController : ControllerBase
    {
        public ICommentRepo _db { get; }

        public CommentController(ICommentRepo db)
        {
            _db = db;
        }

        [HttpPost("add")]
        public ActionResult<CommentDto> add(CommentDto commentDto){
            if(commentDto.userId!=(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))){
                return Unauthorized("You are not the user");
            }
            _db.add(commentDto);
                      
            return Ok(commentDto);
        }
                [AllowAnonymous]

        [HttpGet("getPostComments")]
        public ActionResult<CommentDto> getPostComments([FromQuery]int postId){
            List<CommentDto>  comments=_db.getPostComments(postId);
            if(comments==null){
                return BadRequest("No comments in this post");
            }
            return Ok(comments);
        }

        
                [AllowAnonymous]

        [HttpGet("getUserComments/{userId}")]
        public ActionResult<CommentDto> getUserComments(int userId){
            List<CommentDto>  comments=_db.getUserComments(userId);
            if(comments==null){
                return BadRequest("No comments to this user");
            }
            return Ok(comments);
        }

        [HttpGet("getComment")]
        public ActionResult<CommentDto> getComment([FromQuery]int commentId){
            CommentDto  comment=_db.getComment(commentId);
            if(comment==null){
                return BadRequest("No comments to this user");
            }
            return Ok(comment);
        }

        
        [HttpDelete("deleteComment/{commentId}")]
        public ActionResult<CommentDto> deleteComment(int commentId){
            int userId=_db.getComment(commentId).userId;
         if(userId!=(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))){
                return Unauthorized("You are not the user");
            }
        bool done = _db.deleteComment(commentId);
        if(!done){
            return BadRequest("Didn't delete");
        }
        if(!_db.saveAll()){
             return BadRequest("Didn't delete");

        }
            return Ok(true);
        }

        
        

    }
}