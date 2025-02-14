using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Website001.API.Data{
    public interface IPhotoRepo{
        public string addPhoto(IFormFile file);
    }
}