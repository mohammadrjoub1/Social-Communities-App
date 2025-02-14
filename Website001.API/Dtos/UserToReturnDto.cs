using System.Collections.Generic;
using Website001.API.Models;

namespace Website001.API.Dtos{
    public class UserToReturnDto{
        public int id{set;get;}
        public string username{set;get;}
        public ICollection<Post> posts{set;get;}
    

        
    }
}