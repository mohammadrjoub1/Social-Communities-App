namespace Website001.API.Models{
    public class Admin{
        public int id{set;get;}
        public int userId{set;get;}
        public int privilegeLevel{set;get;}

        public User user{set;get;}
        
    }
}