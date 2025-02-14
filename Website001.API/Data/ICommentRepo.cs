using System.Collections.Generic;
using Website001.API.Dtos;

namespace Website001.API.Data{
    public interface ICommentRepo{
        
        public CommentDto add(CommentDto commentDto);
        public List<CommentDto> getPostComments(int postId);
        public CommentDto getComment(int commentId);
        public List<CommentDto> getUserComments(int userId);
        public bool deleteComment(int commentId);

        public bool saveAll();
    }
}