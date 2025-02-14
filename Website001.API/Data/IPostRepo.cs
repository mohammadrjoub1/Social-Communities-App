using System.Collections.Generic;
using System.Threading.Tasks;
using Website001.API.Helpers;
using Website001.API.Models;

namespace Website001.API.Data{

    public interface IPostRepo{
        public Task<int> add(string title, string content, int authorId,int categorieId,int postTypeId,string image);
        public Task delete(int postId);

        public Task<Categorie> getCatergorieByTitle(string title);
        public Task<Categorie> getCatergorieById(int id);

        public Task<bool> saveAll();
        public Task<Author> GetAuthorByUserId(int id);
        public  Task<Post> getPost(int id);
        public   Task<List<Post>> getPostOfAnAuthor(int id);
        public  Task<List<Post>> getPostsOfAUser(int id);
        public  Task<List<Post>> getPosts();
        public  PageList<Post> getPostsStrict(PostParams postParams);
        public  PageList<Post> getPostsByTopUserCategorie(PostParams postParams);
        public  PageList<Post> getCategoriePosts(PostParams postParams);
        public  PageList<Post> getPostsPaging(PostParams postParams);

        
    }
}