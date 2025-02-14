using System.Collections.Generic;

namespace Website001.API.Models{

    public class User{
        
        public int id{set;get;}
        public string username{set;get;}
        public string email{set;get;}
        public string password{set;get;}
        public string imageUrl{set;get;}
        public ICollection<Post> posts{set;get;}
    

        
    }
}