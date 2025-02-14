using System;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Website001.API.Data{
    public class PhotoRepo : IPhotoRepo
    {        


        public string addPhoto(IFormFile file)
        {
            if(file!=null){
             if(file.Length>0){
        // ImageUploadResult Upload(ImageUploadParams parameters);


            Account account = new Account(
            "afdgadsfg",
            "181875582868216",
            "1Uobwt8BiaBR-uMEECP7IC1TjH8");

        Cloudinary cloudinary = new Cloudinary(account);

            var stream = file.OpenReadStream();
            
            var uploadParams = new ImageUploadParams(             )
            {
                
                File = new FileDescription(file.Name,stream)
            };

             var uploadResult = cloudinary.Upload(uploadParams);

             string theUrl=uploadResult.Url.ToString();
            theUrl=theUrl.Replace("http","https");
            return theUrl;}
            else{return "";}}          

            else{   Console.WriteLine("22222"); return "";}
            
        }
    }
}