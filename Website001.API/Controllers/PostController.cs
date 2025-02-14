using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website001.API.Data;
using Website001.API.Dtos;
using Website001.API.Helpers;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Website001.API.Models;

namespace Website001.API.Controllers{
    [Authorize]
    [ApiController]
    [Controller]
    [Route("api/post")]
    public class PostController:ControllerBase{
        private readonly IPostRepo _postContext;

        public PostController(IPostRepo postContext){
            this._postContext = postContext;
        }
        [HttpPost("addStrict/{userId}")]

            public async Task<ActionResult> addStrict(int userId,PostToAddDto postToAddDto){
            if(postToAddDto.content==null){
                return BadRequest("there is no content");
            }
            if(userId!=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)){
                return Unauthorized("You are not the Author");
            }

           Categorie categorie=await  _postContext.getCatergorieById(postToAddDto.categorieId);

            Author author = new Author();
            author=await _postContext.GetAuthorByUserId(userId);
            if(author==null){
                return Unauthorized("You are not an author");
            }
            if(categorie==null){
                return  BadRequest("categorie doesn't exist");
            }

           // await _postContext.add(postToAddDto.title,postToAddDto.content,author.id,categorie.id,postToAddDto.postTypeId,postToAddDto.imageUrl);
            if(! await _postContext.saveAll()){
                return BadRequest("Couldn't add post");
            }

            return Ok("post added");
        }
        [HttpPost("add/{userId}")]

            public async Task<ActionResult> add(int userId,[FromForm]PostToAddDto postToAddDto){
             if(userId!=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)){
                return Unauthorized("You are not the Author");
            }
            int postId=0;
             Categorie categorie=await  _postContext.getCatergorieById(postToAddDto.categorieId);
            Author author = new Author();
            author=await _postContext.GetAuthorByUserId(userId);
            if(author==null){
                return Unauthorized("You are not an author");
            }
            if(categorie==null){
                return  BadRequest("categorie doesn't exist");
            }
            PhotoRepo photoRepo =new PhotoRepo();
            if(postToAddDto.postTypeId==1){
              postId=  await _postContext.add(postToAddDto.title,postToAddDto.content,author.id,categorie.id,postToAddDto.postTypeId," ");

            }
            if(postToAddDto.postTypeId==2){
                             Console.WriteLine(postToAddDto.image+"  f");

                string imageUrl = photoRepo.addPhoto(postToAddDto.image);

              postId=  await _postContext.add(postToAddDto.title," ",author.id,categorie.id,postToAddDto.postTypeId,imageUrl);

            }
       
            if(postId==0){
                return BadRequest("didn't post");
            }
            Console.WriteLine("postNumber : "+postId);
            return Ok(new{ num=postId});
        }
        [AllowAnonymous]
        [HttpGet("getpost/{id}")]
        public async Task<ActionResult> getPost(int id){
            Post post = new Post();
            post=await _postContext.getPost(id);
            if(post==null){
                return BadRequest("Post doesn't exist");
            }
            
            return Ok(post);
        }
        [AllowAnonymous]
        [HttpGet("getAuthorPosts/{id}")]
        public async Task<ActionResult> getAuthorPosts(int id){
            List<Post> posts = new List<Post>();
            posts=await _postContext.getPostOfAnAuthor(id);
            if(posts==null){
                return BadRequest("Author doesn't have posts");
            }
            
            return Ok(posts);
        }
        [AllowAnonymous]
        [HttpGet("getUserPosts/{id}")]
        public async Task<ActionResult> getUserPosts(int id){
            List<Post> posts = new List<Post>();
            posts=await _postContext.getPostsOfAUser(id);
            if(posts==null){
                return BadRequest("Author doesn't have posts");
            }
            
            return Ok(posts);
        }
        [AllowAnonymous]
        [HttpGet("getPostsStrict")]
        public async Task<ActionResult> getPostsStrict(){
            List<Post> posts = new List<Post>();
            posts=await _postContext.getPosts();
            if(posts==null){
                return BadRequest("there are no posts to view");
            }
            return Ok(posts);
        }

        [AllowAnonymous]
        [HttpGet("getPosts")]
        public  ActionResult getPosts([FromQuery]PostParams postParams){
            var posts= _postContext.getPostsStrict(postParams);

            if(posts.Items==null){
                return BadRequest("there are no posts to view");
            }
            Response.AddApplicationPagination(posts.PageSize,posts.PageNumber,posts.totalPages,posts.count);
      
            return Ok(posts.Items);
        }

                [AllowAnonymous]

        [HttpGet("getPostsByTopUserCategorie")]
        public  ActionResult getPostsByTopUserCategorie([FromQuery]PostParams postParams){

             var posts= _postContext.getPostsByTopUserCategorie(postParams);

            if(posts.Items==null){
                return BadRequest("there are no posts to view");
            }
            Response.AddApplicationPagination(posts.PageSize,posts.PageNumber,posts.totalPages,posts.count);
      
            return Ok(posts.Items);
        }

        [AllowAnonymous]

        [HttpGet("getPostsPaging")]
        public  ActionResult getPostsPaging([FromQuery]PostParams postParams){

             var posts= _postContext.getPostsPaging(postParams);
                
            if(posts.Items==null){
                return BadRequest("there are no posts to view");
            }
            Response.AddApplicationPagination(posts.PageSize,posts.PageNumber,posts.totalPages,posts.count);
      
            return Ok(posts.Items);
        }

        
    }
}