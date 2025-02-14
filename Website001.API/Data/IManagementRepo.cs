using System.Collections.Generic;
using System.Threading.Tasks;
using Website001.API.Dtos;
using Website001.API.Models;

namespace Website001.API.Data{

    public interface IManagementRepo{

        public Task  addAuthor(int id);
        public Task addCategorie(string title);
        public Task<Categorie> getCategorie(int id);
        public Task deleteAuthor(int id);
        public Task<Author> getAuthorByUserId(int id);
        public Task<User> getUserByAuthorId(int id);
        public Task<Author> getAuthor(int id);
        public Task<Admin> getAdmin(int id);
        public Task<bool> saveAll();
        public Task deleteUser(int id);
        public Task<User> getUser(int id);
        public Task<List<User>> getUsers();
        public Task<Categorie> getCatergorieByTitle(string title);
        public Task<Categorie> getCatergorieById(int id);
        public Task<List<Categorie>> getCategories();
        public Task<List<Post>> getCategoriePosts(int id);
        public Task<List<Post>> getPostsByCategorieTitle(string title);
        public int getNumberOfMembersOfCategorie(int categorieId);
        public Task<bool> joinCategorie(int userId,int categorieId);
        public bool leaveCategorie(int userId,int categorieId);
        public bool isUserJoindToCategorie(int userId,int categorieId);
        Task<string> createCategorie(CategorieToAddDto categorieToAddDto,string imageUrl,string coverUrl);
         bool addCategorieCoverPicture(categoriePictureDto categoriePictureDto, string photoUrl);
        bool addCategorieProfilePicture(categoriePictureDto categoriePictureDto, string photoUrl);
        public Task<List<Categorie>> getTopCategories();

    }
}