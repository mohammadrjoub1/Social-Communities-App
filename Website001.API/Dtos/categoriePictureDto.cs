
using Microsoft.AspNetCore.Http;

namespace Website001.API.Dtos{
    public class categoriePictureDto{
        public IFormFile file{set;get;}
        public int userId{set;get;}
        public int categorieId{set;get;}
    }
}