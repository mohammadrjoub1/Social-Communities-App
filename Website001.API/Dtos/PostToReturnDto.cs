using System;
using System.Collections.Generic;
using Website001.API.Models;

namespace Website001.API.Dtos{
    public class PostToReturnDto{
         public int id{set;get;}
        public string title{set;get;}
        public string content{set;get;}
        public int authorId{set;get;}
        public DateTime date{set;get;}
        public int categorieId{set;get;}
        public string imageUrl{set;get;}
        public int reactionSetId{set;get;}
        public int postTypeId{set;get;}
        public AuthorToReturnDto Author{set;get;}

        public Categorie Categorie{set;get;}
        public PostType PostType{set;get;}
        
        public ICollection<ReactionSet> reactionSet{set;get;}


    }
}