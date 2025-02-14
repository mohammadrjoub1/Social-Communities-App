using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Website001.API.Dtos;
using Website001.API.Models;

namespace Website001.API.Data{
    public class ManagementRepo : IManagementRepo
    {
        private readonly DataContext _dataContext;

        public ManagementRepo(DataContext dataContext){
            this._dataContext = dataContext;
        }
        public async Task addCategorie(string title)
        {
            if((title!=null)||(title!="")){
                Categorie categorie = new Categorie();
                categorie.title=title;
                await _dataContext.categories.AddAsync(categorie);
            }
        }

        public async Task  deleteAuthor(int id)
        {
            Author author = new Author();
            author = await _dataContext.authors.FirstOrDefaultAsync(x=>x.id==id);
            
            if(author!=null){
            _dataContext.authors.Remove(author);
            _dataContext.posts.RemoveRange();}

        }

        public async Task<Admin> getAdmin(int id)
        {
            Admin admin=await  _dataContext.admins.FirstOrDefaultAsync(x=>x.id==id);
            if(admin==null){return null;}
            if(await _dataContext.users.AnyAsync(x=>x.id==admin.userId)){
                return admin;
            }
            return null;
        }

        public async Task<Author> getAuthor(int id)
        {
            Author author=await  _dataContext.authors.FirstOrDefaultAsync(x=>x.id==id);

            if(await _dataContext.users.AnyAsync(x=>x.id==author.userId)){
                return author;
            }
            return null;        
        }

        public async Task<Author> getAuthorByUserId(int id)
        {
            User user = new User();
            user = await _dataContext.users.FirstOrDefaultAsync(x=>x.id==id);
            if(user==null){
                return null;
            }
            Author author = new Author();
            author=await _dataContext.authors.FirstOrDefaultAsync(x=>x.userId==id);
            if(author==null){
                return null;
            }
            return  author;
        }

        public async Task<User> getUserByAuthorId(int id)
        {
            if(!await _dataContext.authors.AnyAsync(x=>x.id==id)){
                return null;
            }
            Author author=await  _dataContext.authors.FirstOrDefaultAsync(x=>x.id==id);
            User user = new User();
            if(!await _dataContext.users.AnyAsync(x=>x.id==author.userId)){
                return null;
            }
            user=await _dataContext.users.FirstOrDefaultAsync(x=>x.id==author.userId);

            return user;
        }

        public async Task addAuthor(int id)
        {
            User user = new User();
            user =await  _dataContext.users.FirstOrDefaultAsync(x=>x.id==id);
            if(user!=null){
                Author author = new Author();
                    author.userId=id;
                    author.user=user;
                _dataContext.authors.Add(author);
            }
        }

        public async Task<bool> saveAll()
        {
            return await _dataContext.SaveChangesAsync()>0;
        }


        public async Task deleteUser(int id)
        {
            if (await _dataContext.users.AnyAsync(x => x.id == id))
            {
                User user= await _dataContext.users.FirstOrDefaultAsync(x=>x.id==id);
                _dataContext.users.Remove(user);
            }
        }

        public async Task<User> getUser(int id)
        {
            return await _dataContext.users.FirstOrDefaultAsync(x=>x.id==id);
        }

        public async Task<List<User>> getUsers()
        {
           return await _dataContext.users.ToListAsync();
        }

        public Task<Categorie> getCategorie(int id)
        {
           return _dataContext.categories.FirstOrDefaultAsync(x=>x.id==id);
        }

        public Task<Categorie> getCatergorieByTitle(string title)
        {
            return _dataContext.categories.FirstOrDefaultAsync(x=>x.title==title);
        }

        public Task<Categorie> getCatergorieById(int id)
        {
            return _dataContext.categories.FirstOrDefaultAsync(x=>x.id==id);
        }

        public async Task<List<Categorie>> getCategories()
        {
            return await this._dataContext.categories.ToListAsync();
        }

        public async Task<List<Post>> getCategoriePosts(int id)
        {
            return await _dataContext.posts.Where(x=>x.categorieId==id).ToListAsync();
        }

        public async Task<List<Post>> getPostsByCategorieTitle(string title)
        {
           return await this._dataContext.posts.Include(x=>x.Author.user).Include(x=>x.Categorie).Include(x=>x.Author).Where(x=>x.Categorie.title==title).ToListAsync();
        }

        public async Task<bool> joinCategorie(int userId, int categorieId)
        {
           if( !this._dataContext.categories.Any(x=>x.id==categorieId)|| !this._dataContext.users.Any(x=>x.id==userId)){
            return false;
           }
           if(this._dataContext.categorieJoins.Where(x=>x.categorieId==categorieId).Any(x=>x.userId==userId)){
               return false;
           }
           CategorieJoin categorieJoin = new CategorieJoin();
           categorieJoin.userId=userId;
           categorieJoin.categorieId=categorieId;
           await this._dataContext.categorieJoins.AddAsync(categorieJoin);

           _dataContext.SaveChanges();
           return true;
        }
        public  bool leaveCategorie(int userId, int categorieId)
          {
           if( !this._dataContext.categories.Any(x=>x.id==categorieId)|| !this._dataContext.users.Any(x=>x.id==userId)){
            return false;
           }
           if(!this._dataContext.categorieJoins.Where(x=>x.categorieId==categorieId).Any(x=>x.userId==userId)){
               return false;
           }
           CategorieJoin categorieJoin =this._dataContext.categorieJoins.Where(x=>x.categorieId==categorieId).FirstOrDefault(x=>x.userId==userId);
           this._dataContext.categorieJoins.Remove(categorieJoin);
            this._dataContext.SaveChanges();
           return true;
        }

        public bool isUserJoindToCategorie(int userId, int categorieId)
        {
           if( !this._dataContext.categories.Any(x=>x.id==categorieId)|| !this._dataContext.users.Any(x=>x.id==userId)){
            return false;
           }
           if(this._dataContext.categorieJoins.Where(x=>x.categorieId==categorieId).Any(x=>x.userId==userId)){
               return true;
           }    
           return false ; }

        public int getNumberOfMembersOfCategorie(int categorieId)
        {
            return this._dataContext.categorieJoins.Where(x => x.categorieId == categorieId).ToArray().Length;
        }

        public async Task<string> createCategorie(CategorieToAddDto categorieToAddDto,string imageUrl,string coverUrl)
        {
             Categorie categorie = new Categorie();
            if(!(((categorieToAddDto.title=="")||(categorieToAddDto.title==null)))){


                 User user=await getUser(categorieToAddDto.userId);
                
              
            
             
                categorie.title=categorieToAddDto.title;
                categorie.userId=categorieToAddDto.userId;
                categorie.about=categorieToAddDto.about;
                categorie.date=DateTime.Now;
                categorie.coverUrl=coverUrl;
                categorie.imageUrl=imageUrl;



                    await _dataContext.categories.AddAsync(categorie);
                    this._dataContext.SaveChanges();
                    Categorie categorieToReturn = this._dataContext.categories.FirstOrDefault(x=>(
                        x.title==categorie.title &&
                        x.userId==categorie.userId &&
                        x.about==categorie.about &&
                        x.date==categorie.date &&
                        x.coverUrl==categorie.coverUrl &&
                        x.imageUrl==categorie.imageUrl 
                    ));
                    
                    return categorieToReturn.title;
                
                
                
            }
            
            return "";
         }

    public bool addUserProfilePicture(categoriePictureDto categoriePictureDto,string url)
        {   
            User user = this._dataContext.users.FirstOrDefault(x=>x.id==categoriePictureDto.userId);
          
            Categorie categorie = this._dataContext.categories.FirstOrDefault(x=>x.id==categoriePictureDto.categorieId);
              if(user==null||categorie==null){
                return false;
            }
            
            categorie.imageUrl=url;
           return this._dataContext.SaveChanges()>0;

        
    }
 
        public bool addCategorieCoverPicture(categoriePictureDto categoriePictureDto,string url)
        {   
            User user = this._dataContext.users.FirstOrDefault(x=>x.id==categoriePictureDto.userId);
          
            Categorie categorie = this._dataContext.categories.FirstOrDefault(x=>x.id==categoriePictureDto.categorieId);
              if(user==null||categorie==null){
                return false;
            }
            
            categorie.coverUrl=url;
           return this._dataContext.SaveChanges()>0;

        }
    public bool addCategorieProfilePicture(categoriePictureDto categoriePictureDto,string url)
        {   
            User user = this._dataContext.users.FirstOrDefault(x=>x.id==categoriePictureDto.userId);
          
            Categorie categorie = this._dataContext.categories.FirstOrDefault(x=>x.id==categoriePictureDto.categorieId);
              if(user==null||categorie==null){
                return false;
            }
            
            categorie.imageUrl=url;
           return this._dataContext.SaveChanges()>0;

        }

    
    public async Task<List<Categorie>> getTopCategories(){
        
        
        if(await this._dataContext.categories.AnyAsync()==false){
            return null;
        }

        int maxListItems=5;
        List<Categorie> categoriesToReturn = new List<Categorie>();
        List<Categorie> categories = new List<Categorie>();
        categories=await this._dataContext.categories.ToListAsync();
        int[,] theSavior = new int[2,categories.ToArray().Length];
        int z=0;
        foreach (var cat in categories)
        {
            List<CategorieJoin> categorieJoins = new List<CategorieJoin>();
            categorieJoins= await this._dataContext.categorieJoins.Where(x=>x.categorieId==cat.id).ToListAsync();
            theSavior[0,z]=cat.id;
            theSavior[1,z]=categorieJoins.ToArray().Length;
            z=z+1;
        }
       for(int i=0;i<categories.ToArray().Length;i++){
           for(int a = 0 ; a<categories.ToArray().Length;a++){
               if(theSavior[1,a]<theSavior[1,i]){
                   int temp1=theSavior[1,a];
                   int temp2=theSavior[0,a];
                   theSavior[1,a]=theSavior[1,i];
                   theSavior[0,a]=theSavior[0,i];
                   theSavior[1,i]=temp1;
                   theSavior[0,i]=temp2;
               }
           }
       }
       
       for(int i=0;i<categories.ToArray().Length;i++){
           if(maxListItems==i+1){
               break;
           }
           Categorie c = new Categorie();
           c=await this._dataContext.categories.FirstOrDefaultAsync(x=>x.id==theSavior[0,i]);
           categoriesToReturn.Add(c);
           Console.WriteLine("cat num "+i+" :"+ c);
       } 

        return categoriesToReturn;
    }
    }

}