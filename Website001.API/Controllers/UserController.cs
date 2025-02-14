using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Website001.API.Data;
using Website001.API.Dtos;
using Website001.API.Models;

namespace Website001.API.Controllers{
    [ApiController]
    [Controller]
    [Route("api/user")]
    public class UserController:ControllerBase{
        private readonly IUserRepo _userRepo;
        private readonly IConfiguration _configuration;

        public PhotoRepo _photoRepo = new PhotoRepo();

        public UserController(IUserRepo userRepo,IConfiguration configuration)
        {
            this._userRepo = userRepo;
            this._configuration = configuration;
         }

        [HttpPost("login")]
        public async Task<IActionResult> login(UserToLoginDto userToLoginDto){
            User user = new User();
            user= await _userRepo.login(userToLoginDto.username,userToLoginDto.password);
            if(user==null){
                return BadRequest("No match");
            }

               
            var claims = new []{
                new Claim(ClaimTypes.NameIdentifier,user.id.ToString()),
                new Claim(ClaimTypes.Name,user.username)
            };
      string Me="asdfasdfa34gtqa3wy4gq";

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Me));
            
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
       
            var tokenDescriptor = new SecurityTokenDescriptor();
            tokenDescriptor.Subject= new ClaimsIdentity(claims);
            tokenDescriptor.Expires=DateTime.Now.AddDays(1);
            tokenDescriptor.SigningCredentials=creds;

            var tokenHandler =new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);


            return Ok(    new {
                   token= tokenHandler.WriteToken(token)
                });


        }
        
        [HttpPost("register")]
        public async Task<IActionResult> register(UserToRegisterDto userToRegisterDto){
            User user = await _userRepo.createUser(userToRegisterDto.username, userToRegisterDto.password, userToRegisterDto.email);
            if(user==null){
                return BadRequest("User with duplicated data allready exists");
            }
            await _userRepo.SaveAll();
            return Ok("Registration completed");
            
        }
        [HttpGet("getUser/{id}")]
        public async Task<ActionResult<User>> getUser(int id){
          User user=await  this._userRepo.getUser(id);
            if(user==null){
                return BadRequest("No such user");
            }
            user.password="";
            return Ok(user);
        }
        [HttpGet("getUserByUsername/{username}")]
        public async Task<ActionResult<User>> getUserByUsername(string username){
          User user=await  this._userRepo.getUserByUsername(username);
            if(user==null){
                return BadRequest("No such user");
            }
            user.password="";
            user.email="";
            return Ok(user);
        }
        [HttpPut("addUserProfilePicture")]
        public ActionResult<String> addUserProfilePicture([FromForm]profilePictureDto profilePictureDto){
             Console.WriteLine(profilePictureDto.userId+"  asdasdasd");
             Console.WriteLine(profilePictureDto.file+"  f");
             if(profilePictureDto.userId!=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)){
                return Unauthorized("You are not the User");
            }
            string photoUrl=this._photoRepo.addPhoto(profilePictureDto.file);
            if(photoUrl!=null||photoUrl!=""){
               
               if(!this._userRepo.addUserProfilePicture(profilePictureDto,photoUrl)){
                   return("Couldn't upload photo");
               }
            }
            return photoUrl;
        }

    }
}
