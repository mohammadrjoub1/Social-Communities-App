using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Website001.API.Dtos;
using Website001.API.Models;

namespace Website001.API.Data
{

    public class UserRepo : IUserRepo
    {
        private readonly DataContext _dataContext;

        public UserRepo(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<User> createUserStrict(string username, string password,string email)
        {
            if(username==null||password==null){
                return null;
            }
            if(await doesUserExist(username)){
                return null;
            }
            User user= new User();
            user.username=username;
            user.password=password;
            user.email=email;

            _dataContext.users.Add(user);
            return user;
            
        }



        public async Task<bool> doesUserExist(string username)
        {
            if (username == null)
            {
                return true;
            }
        if(await _dataContext.users.AnyAsync(x => x.username == username)){
            return true;
        }
        if(await _dataContext.users.AnyAsync(x => x.email == username)){
            return true;
        }
        
         return false;
        }

        public async Task<User> login(string username,string password)
        {
            User user = new User();
            if (password != null){
               if(username == null){
                    return null; 
            }
            }
            else{return null;}

      
            if(await _dataContext.users.AnyAsync(x => x.username == username)){
              user = await _dataContext.users.FirstOrDefaultAsync(x => x.username == username);
             }

            else {
                user=await _dataContext.users.FirstOrDefaultAsync(x => x.email == username);
 
                }
                
            if(user==null){
                return null;
            }
            var passwordToCompare = user.password;
            if (passwordToCompare == password)
            {
                return user;
            }


            return null;
        }
        public async Task<User> getUser(int id){
            
            return await this._dataContext.users.Include(x=>x.posts).FirstOrDefaultAsync(x=>x.id==id);
           
        }
        public async Task<User> getUserByUsername(string username){
            User user = new User();
            user =await this._dataContext.users.Include(x=>x.posts).FirstOrDefaultAsync(x=>x.username==username);
           if(user==null){
            user =await this._dataContext.users.Include(x=>x.posts).FirstOrDefaultAsync(x=>x.email==username);

           }
            return user;
        }

        public async Task<bool> SaveAll()
        {
           return await _dataContext.SaveChangesAsync()>1;
        }

        public async Task<User> createUser(string username, string password, string email)
        {
                 if(username==null||password==null){
                return null;
            }
            if(await doesUserExist(username)){
                
                     return null;
                
            }
            if(await doesUserExist(email)){
                
                     return null;
                
            }
            User user= new User();
            user.username=username;
            user.password=password;
            user.email=email;
            user.imageUrl="https://res.cloudinary.com/afdgadsfg/image/upload/v1619842130/10128-child-icon_gq80ib.png";

            _dataContext.users.Add(user);

            Author author = new Author();
            author.user=user;
            author.userId=user.id;
            _dataContext.Add(author);
            return user;
        }

    
        public bool addUserProfilePicture(profilePictureDto profilePictureDto,string url)
        {   
            User userToChange = this._dataContext.users.Include(x=>x.posts).FirstOrDefault(x=>x.id==profilePictureDto.userId);
            userToChange.imageUrl=url;
           return this._dataContext.SaveChanges()>0;

        }
 
    }
}