using Microsoft.AspNetCore.Http;

namespace Website001.API.Dtos{
    public class profilePictureDto{
        public IFormFile file{set;get;}
        public int userId{set;get;}
    }
}