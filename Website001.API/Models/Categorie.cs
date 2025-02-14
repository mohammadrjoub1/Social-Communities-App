using System;
using System.Collections.Generic;

namespace Website001.API.Models{
    public class Categorie{
        public int id{set;get;}
        public string title{set;get;}
        public string about{set;get;}
        public DateTime date{set;get;}
        
        public string imageUrl{set;get;}
        public string coverUrl{set;get;}
        public int userId{set;get;}


        
    }
}