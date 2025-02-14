using System.Threading.Tasks;
using Website001.API.Dtos;
using Website001.API.Models;

namespace Website001.API.Data{
    public interface IUserRepo{
        public Task<User> createUser(string username,string password,string email);
        public Task<User> login(string username,string password);
      
        public  Task<User> getUser(int id);

        public Task<bool> doesUserExist(string username);
        public  Task<bool> SaveAll();
        public Task<User> getUserByUsername(string username); 
        bool addUserProfilePicture(profilePictureDto profilePictureDto, string photoUrl);
    }
}