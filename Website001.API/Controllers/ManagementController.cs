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
    [Route("api/Management/{adminId}")]
    public class ManagementController:ControllerBase{
        private readonly IManagementRepo _management;

        public ManagementController(IManagementRepo management){
            this._management = management;
        }

        [HttpPost("users/MakeAuthor/{id}")]
        public async Task<ActionResult> addAuthor(int adminId ,int id){
             Admin admin = new Admin();
            admin=await _management.getAdmin(adminId);
            if(admin==null){
                return Unauthorized("You don't have the privlieges");
            }
            User user =await  _management.getUser(admin.userId);
            if(user==null){
                return BadRequest("user doesn't exist");
            }
            if(user.id!=(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))){
                return Unauthorized("This is not your account");
            }
            await _management.addAuthor(id);
            bool answer= await _management.saveAll();

            if(!answer){
                return BadRequest("something went wrong");}

            return Ok("Author has been added");
        }
        [HttpPost("categories/add")]
        public async Task<ActionResult> addCategorie(int adminId ,CategorieToAddDto cocategorieToAddDto){
           Admin admin = new Admin();
            admin=await _management.getAdmin(adminId);
            if(admin==null){
                return Unauthorized("You don't have the privlieges");
            }
            User user =await  _management.getUser(admin.userId);
            if(user==null){
                return BadRequest("user doesn't exist");
            }
            if(user.id!=(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))){
                return Unauthorized("This is not your account");
            }
             Categorie categorie = new Categorie();
             categorie=await _management.getCatergorieByTitle(cocategorieToAddDto.title);
            if(categorie!=null){
                return BadRequest("categorie allready exists");
            }
          await _management.addCategorie(cocategorieToAddDto.title);
          if(!await _management.saveAll()){
              return BadRequest("something went wrong");
            }

        return Ok("Categorie added successfuly");
        }
        [HttpDelete("users/authors/deleteAuthor/{id}")]
        public async Task<ActionResult> deleteAuthor(int adminId ,int id){

            Admin admin = new Admin();
            admin=await _management.getAdmin(adminId);
            if(admin==null){
                return Unauthorized("You don't have the privlieges");
            }
            User user =await  _management.getUserByAuthorId(adminId);
            if(user==null){
                return BadRequest("user doesn't exist");
            }
            if(user.id!=(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))){
                return Unauthorized("This is not your account");
            }
            await _management.deleteAuthor(id);
            if(! await _management.saveAll()){
                return BadRequest("something went wrong");
            }
            return Ok("Author deleted");
        }
        
        [HttpDelete("users/deleteUser/{id}")]
        public async Task<ActionResult> deleteUser(int adminId ,int id){

            Admin admin = new Admin();
            admin=await _management.getAdmin(adminId);
            if(admin==null){
                return Unauthorized("You don't have the privlieges");
            }
            User user =await  _management.getUser(adminId);
            if(user==null){
                return BadRequest("user doesn't exist");
            }
            if(user.id!=(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))){
                return Unauthorized("This is not your account");
            }
              if(_management.getUser(id)==null){
                return BadRequest("the user to be deleted doesn't exist");
            }
            await _management.deleteUser(id);
            if(! await _management.saveAll()){
                return BadRequest("something went wrong");
            }
            return Ok("Author deleted");
        }
        

      

    }
}