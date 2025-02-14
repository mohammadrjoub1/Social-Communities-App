using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Website001.API.Data;
using Website001.API.Dtos;
using Website001.API.Models;

namespace Website001.API.Controllers{

    [ApiController]
    [Controller]
    [Route("api/categorie")]
    public class categorieController:ControllerBase{
        private readonly IManagementRepo _db;
        public PhotoRepo _photoRepo = new PhotoRepo();

        public categorieController(IManagementRepo db)
        {
            _db = db;
        }

        [HttpGet("getCategories")]
        public async Task<ActionResult<List<Categorie>>> getCategories(){

            List<Categorie> categories = new List<Categorie>();
            categories= await _db.getCategories();
            List<CatrgorieToReturnDto> catrgorieToReturnDto = new List<CatrgorieToReturnDto>();
            
            foreach (var item in categories){
                
                CatrgorieToReturnDto tempCatrgorieToReturnDto =new CatrgorieToReturnDto();
                tempCatrgorieToReturnDto.id=item.id;
                tempCatrgorieToReturnDto.title=item.title;
                tempCatrgorieToReturnDto.date=item.date;
                tempCatrgorieToReturnDto.about=item.about;
                tempCatrgorieToReturnDto.coverUrl=item.coverUrl;
                tempCatrgorieToReturnDto.imageUrl=item.imageUrl;
                catrgorieToReturnDto.Add(tempCatrgorieToReturnDto);

            }
            {
                
            }
            return Ok(catrgorieToReturnDto);
        }

        [HttpGet("getGategoriePosts/{id}")]
        public async Task<ActionResult<List<Post>>> getGategoriePosts(int id){
            
            return Ok(await _db.getCategoriePosts(id));
        }

        [HttpGet("getPostsByCategorieTitle/{title}")]
        public async Task<ActionResult<List<Post>>> getPostsByCategorieTitle(string title){
            List<PostToReturnDto> postToReturnDto = new List<PostToReturnDto>();
            AuthorToReturnDto authorToReturnDto = new AuthorToReturnDto();
            
            List<Post> posts = new List<Post>();

            posts=await _db.getPostsByCategorieTitle(title);

            foreach (var item in posts)
            {
                
            Author author = new Author();
            UserToReturnDto userToReturnDto = new UserToReturnDto();
            PostToReturnDto tempPost = new PostToReturnDto();

            author=item.Author;
            authorToReturnDto.user=userToReturnDto;


            userToReturnDto.id= item.Author.user.id;
            userToReturnDto.posts=item.Author.user.posts;
            userToReturnDto.username=item.Author.user.username;
         
            authorToReturnDto.user=userToReturnDto;
            authorToReturnDto.id=item.authorId;
            authorToReturnDto.userId=item.Author.userId;

            tempPost.Author=authorToReturnDto;
            tempPost.authorId=item.authorId;
            tempPost.Categorie=item.Categorie;
            tempPost.categorieId=item.categorieId;
            tempPost.content=item.content;
            tempPost.date=item.date;
            tempPost.id=item.id;
            tempPost.imageUrl=item.imageUrl;
            tempPost.PostType=item.PostType;
            tempPost.postTypeId=item.postTypeId;
            tempPost.reactionSet=item.reactionSet;
            tempPost.reactionSetId=item.reactionSetId;
            tempPost.title=item.title;
            
            postToReturnDto.Add(tempPost);

            }

            return Ok(postToReturnDto);
        }
        [HttpGet("getCategorieByTitle/{title}")]
        public async Task<ActionResult<Categorie>> getCategorieByTitle(string title){
            Categorie categorie=await this._db.getCatergorieByTitle(title);
            if(categorie==null){
                return BadRequest("Categorie doesn't exist");
            }
            return Ok(categorie);
        }
        [HttpGet("getCategorie/{id}")]
        public async Task<ActionResult<Categorie>> getCategorie(int id){
            Categorie categorie=await this._db.getCategorie(id);
            if(categorie==null){
                return BadRequest("Categorie doesn't exist");
            }
            return Ok(categorie);
        }
        [HttpPost("joinCategorie")]
        public async Task<ActionResult<bool>> joinCategorie(CategorieJoiningDto categorieJoiningDto){
             if(categorieJoiningDto.userId!=int.Parse((User.FindFirst(ClaimTypes.NameIdentifier).Value))){
                return Unauthorized();
            }
           return  await _db.joinCategorie(categorieJoiningDto.userId,categorieJoiningDto.categorieId);

        }
        [HttpDelete("leaveCategorie")]
        public  ActionResult<bool> leaveCategorie([FromQuery]CategorieJoiningDto categorieJoiningDto){
            if(categorieJoiningDto.userId!=int.Parse((User.FindFirst(ClaimTypes.NameIdentifier).Value))){
                return Unauthorized();
            }
         return  _db.leaveCategorie(categorieJoiningDto.userId,categorieJoiningDto.categorieId);
      
        }
        [HttpGet("isUserJoindToCategorie")]
        public  ActionResult<bool> isUserJoindToCategorie([FromQuery]CategorieJoiningDto categorieJoiningDto){
            
           return  _db.isUserJoindToCategorie(categorieJoiningDto.userId,categorieJoiningDto.categorieId);

        }

        [HttpGet("getNumberOfMembersOfCategorie/{categorieId}")]
        public ActionResult<int> getNumberOfMembersOfCategorie(int categorieId){
            return Ok(this._db.getNumberOfMembersOfCategorie(categorieId));
        }

        [HttpPost("createCategorie")]
        public async Task<ActionResult> createCategorie([FromForm]CategorieToAddDto categorieToAddDto){
             if(categorieToAddDto.userId!=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)){
                return Unauthorized("You are not the User cliamd to be");
            }
            string title=" ";
             Author author = new Author();
            author=await _db.getAuthorByUserId(categorieToAddDto.userId);
            if(author==null){
                return Unauthorized("You are not an author");
            }
        string imageUrl ="";
        string coverUrl ="";
            PhotoRepo photoRepo =new PhotoRepo();
            if(categorieToAddDto.image!=null){
                imageUrl=photoRepo.addPhoto(categorieToAddDto.image);}
                else{
                    imageUrl="https://res.cloudinary.com/afdgadsfg/image/upload/v1620223243/dbujq3h-13f8d636-9028-474c-b0fa-2a588511ac5f_dcy4lo.png";
                }
            if(categorieToAddDto.cover!=null){
                coverUrl=photoRepo.addPhoto(categorieToAddDto.cover);}
                else{
                    coverUrl="https://res.cloudinary.com/afdgadsfg/image/upload/v1620222961/final-image-diverse-people-1_svynwn.jpg";
                }

              title=  await _db.createCategorie(categorieToAddDto,imageUrl,coverUrl);

          
          
       
            if(title==" "){
                return BadRequest("didn't create");
            }
             return Ok(new{ categorieTitle=title});
        }
        [HttpPut("addCategorieProfilePicture")]
        public  async Task<ActionResult<string>> addCategorieProfilePicture([FromForm]categoriePictureDto categoriePictureDto){
 
             if(categoriePictureDto.userId!=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)){
                return Unauthorized("You are not the User");
            }
            Categorie categorie =await _db.getCategorie(categoriePictureDto.categorieId);
            if(categorie==null){
                return BadRequest("Categorie doesn't exist");
            }
            User user = await _db.getUser(categoriePictureDto.userId);
            if(categorie==null){
                return BadRequest("Categorie doesn't exist");
            }
             if(user==null){
                return BadRequest("this user doesn't exist");
            }
           if(categorie.userId!=user.id){
                return BadRequest("You need to be the admin in order to be able to make a change");
            }

            string photoUrl=this._photoRepo.addPhoto(categoriePictureDto.file);
            if(photoUrl!=null||photoUrl!=""){
               
               if(!this._db.addCategorieProfilePicture(categoriePictureDto,photoUrl)){
                   BadRequest("Couldn't upload photo");
               }
            }
            return photoUrl;
        }
       [HttpPut("addCategorieCoverPicture")]
        public  async Task<ActionResult<string>> addCategorieCoverPicture([FromForm]categoriePictureDto categoriePictureDto){
 
             if(categoriePictureDto.userId!=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)){
                return Unauthorized("You are not the User");
            }
            Categorie categorie =await _db.getCategorie(categoriePictureDto.categorieId);
            if(categorie==null){
                return BadRequest("Categorie doesn't exist");
            }
            User user = await _db.getUser(categoriePictureDto.userId);
            if(categorie==null){
                return BadRequest("Categorie doesn't exist");
            }
             if(user==null){
                return BadRequest("this user doesn't exist");
            }
           if(categorie.userId!=user.id){
                return BadRequest("You need to be the admin in order to be able to make a change");
            }

            string photoUrl=this._photoRepo.addPhoto(categoriePictureDto.file);
            if(photoUrl!=null||photoUrl!=""){
               
               if(!this._db.addCategorieCoverPicture(categoriePictureDto,photoUrl)){
                   BadRequest("Couldn't upload photo");
               }
            }
            return photoUrl;
        }
        [HttpGet("getTopCategories")]
         public  async Task<ActionResult> getTopCategories(){
            return Ok(await this._db.getTopCategories());
        }

    }
}