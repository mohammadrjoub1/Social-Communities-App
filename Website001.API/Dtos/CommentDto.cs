using System;

namespace Website001.API.Dtos{
    public class CommentDto{

        public int id{set;get;}
        public int userId{set;get;}
        public int postId{set;get;}
        public int categorieId{set;get;}
        public string comment{set;get;}
        public DateTime date{set;get;}
        public bool prime{set;get;}
        public int parentCommentId{set;get;}

    }
}