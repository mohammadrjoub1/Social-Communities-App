using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Website001.API.Dtos{
    public class CategorieToAddDto{
        public int id{set;get;}
        [Required]
        public string title{set;get;}
        public string about{set;get;}
        public string date{set;get;}
        
        public IFormFile image{set;get;}
        public IFormFile cover{set;get;}
        public int userId{set;get;}
    }
}