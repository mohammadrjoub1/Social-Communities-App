using System.ComponentModel.DataAnnotations;

namespace Website001.API.Dtos{
    public class UserToRegisterDto{
        
        [Required]
        public string username{set;get;}
        [Required]
        public string email{set;get;}

        [Required]
        [MinLength(8)]
        public string password{set;get;}
    }
}