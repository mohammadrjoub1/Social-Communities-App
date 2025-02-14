using System;
using Microsoft.AspNetCore.Http;
using Website001.API.Models;

namespace Website001.API.Dtos{

    public class PostToAddDto{
        public string title{set;get;}
        public string content{set;get;}
        public int categorieId{set;get;}
        public IFormFile image{set;get;}
        public int postTypeId{set;get;}

    }
}