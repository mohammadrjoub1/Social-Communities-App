using System;
using System.Collections.Generic;
using System.Linq;
using Website001.API.Dtos;

namespace Website001.API.Data{
    public class CommentRepo : ICommentRepo
    {
        private readonly DataContext _db;

        public CommentRepo(DataContext db)
        {
            _db = db;
        }

 
        public CommentDto add(CommentDto commentDto)
        {
            Comment comment = new Comment();
            
            if(commentDto.comment==null||commentDto.comment==""){
                return null;
            }

            if(!_db.categories.Any(x=>x.id==commentDto.categorieId)){
                return null;
            }
            
            if(!_db.users.Any(x=>x.id==commentDto.userId)){
                return null;
            }
            if(!_db.posts.Any(x=>x.id==commentDto.postId)){
                return null;
            }
            
      
            
            comment.categorieId=commentDto.categorieId;
            comment.comment=commentDto.comment;
            comment.date=DateTime.Now;
            comment.postId=commentDto.postId;
            comment.prime=false;
            comment.userId=commentDto.userId;
            comment.parentCommentId=commentDto.parentCommentId;
            if(commentDto.parentCommentId>0){
              Comment commentToChange=_db.comments.FirstOrDefault(x=>x.id==comment.parentCommentId);
              commentToChange.prime=true;

            }
             _db.comments.Add(comment);
             _db.SaveChanges();

           Comment a= _db.comments.FirstOrDefault(
                x=>(
                (x.categorieId==comment.categorieId)&&
                (x.comment==comment.comment)&&
                (x.date==comment.date)&&
                (x.postId==comment.postId)&&
                (x.parentCommentId==comment.parentCommentId)&&
                (x.userId==comment.userId))
            );
             
            commentDto.id=a.id;
            commentDto.postId=a.postId;
            commentDto.prime=a.prime;
            commentDto.userId=a.userId;
            commentDto.categorieId=a.categorieId;
            commentDto.date=a.date;
            commentDto.comment=a.comment;
            commentDto.parentCommentId=a.parentCommentId;
            return commentDto;

        }

        public bool deleteComment(int commentId)
        {
            Comment comennt = new Comment();
            comennt=_db.comments.FirstOrDefault(x=>x.id==commentId);
            if(comennt==null){return false;}
            _db.comments.Remove(comennt);
            return(true);


        }

        public CommentDto getComment(int commentId)
        {
            Comment comennt = new Comment();
            CommentDto commentDto = new CommentDto();
            comennt=_db.comments.FirstOrDefault(x=>x.id==commentId);
            
            if(comennt==null){return null;}
            
            commentDto.id=comennt.id;
            commentDto.postId=comennt.postId;
            commentDto.prime=comennt.prime;
            commentDto.userId=comennt.userId;
            commentDto.categorieId=comennt.categorieId;
            commentDto.comment=comennt.comment;
            commentDto.parentCommentId=comennt.parentCommentId;

            return commentDto;
        }

        public List<CommentDto> getPostComments(int postId)
        {
           if(!_db.posts.Any(x=>x.id==postId)){
               return null;
           }
        
           List<Comment> comments = _db.comments.Where(x=>x.postId==postId).ToList();
           List<CommentDto> commentsDto = new List<CommentDto>();
            if(comments==null){return null;}
            foreach (var item in comments)
            {
                CommentDto commentDto = new CommentDto();
                commentDto.id=item.id;
                commentDto.categorieId=item.categorieId;
                commentDto.comment=item.comment;
                commentDto.date=item.date;
                commentDto.prime=item.prime;
                commentDto.userId=item.userId;
                commentDto.parentCommentId=item.parentCommentId;
                commentsDto.Add(commentDto);

            }
        
        return commentsDto;
        }

        public List<CommentDto> getUserComments(int userId)
        {
        if(!_db.users.Any(x=>x.id==userId)){
               return null;
           }
        
           List<Comment> comments = new List<Comment>();
           List<CommentDto> commentsDto = new List<CommentDto>();
           comments = _db.comments.Where(x=>x.userId==userId).ToList();
         foreach (var item in comments)
            {
                CommentDto commentDto = new CommentDto();
                commentDto.id=item.id;
                commentDto.categorieId=item.categorieId;
                commentDto.comment=item.comment;
                commentDto.date=item.date;
                commentDto.prime=item.prime;
                commentDto.userId=item.userId;
                commentDto.parentCommentId=item.parentCommentId;
                commentsDto.Add(commentDto);

            }
        
        return commentsDto;        }

        public bool saveAll()
        {
            return this._db.SaveChanges()>0;
        }
    }
}