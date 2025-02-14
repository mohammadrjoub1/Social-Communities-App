using Website001.API.Models;

namespace Website001.API.Dtos{
    public class AuthorToReturnDto{
        public int id{set;get;}
        public int userId{set;get;}
        public UserToReturnDto user{set;get;}
    }
}