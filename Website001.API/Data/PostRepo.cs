using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Website001.API.Helpers;
using Website001.API.Models;

namespace Website001.API.Data{
    public class PostRepo : IPostRepo
    {
        private readonly DataContext _dataContext;

        IManagementRepo _managementRepo;
        public PostRepo(DataContext dataContext,IManagementRepo managementRepo){
            this._dataContext = dataContext;
            this._managementRepo=managementRepo;
        }

        public async Task<int> add(string title, string content, int authorId,int categorieId,int postTypeId,string imageUrl)
        {
             Post post = new Post();
            if(!(((title=="")||(title==null)))){


                Author author = new Author();
                author=await _managementRepo.getAuthor(authorId);
                
                User user = new User();
                if(author!=null){
                    user=await _managementRepo.getUser(author.userId); }
                Categorie categorie = new Categorie();
                 if(postTypeId==1){
                post.content=content;
                post.imageUrl=" ";

             }

                if(postTypeId==2){
                post.imageUrl=imageUrl;
                post.content=" ";


                }
                post.title=title;
                post.authorId=authorId;
                post.Author=author; 
                post.date=DateTime.Now;
                post.postTypeId=postTypeId;

                post.Author.user=user;


                categorie =await _managementRepo.getCategorie(categorieId);
                if(categorie!=null){
                    post.categorieId=categorieId;
                    post.Categorie=categorie;
                    await _dataContext.posts.AddAsync(post);
                    this._dataContext.SaveChanges();
                    Post postToReturn = this._dataContext.posts.FirstOrDefault(x=>(x.Author.user.id==post.Author.user.id&&x.date==post.date));
                    
                    return postToReturn.id;
                }
                
                
            }
            
            return  0;
        }   

        public Task delete(int postId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Author> GetAuthorByUserId(int id)
        {
            return _managementRepo.getAuthorByUserId(id);
        }

        public async Task<Categorie> getCatergorieById(int id)
        {
            return await _managementRepo.getCatergorieById(id);
        }

        public async Task<Categorie> getCatergorieByTitle(string title)
        {
            return await _managementRepo.getCatergorieByTitle(title);
        }

        public async Task<Post> getPost(int id){
           Post post = new Post();
           post =await _dataContext.posts.Include(x=>x.Author).Include(x=>x.Categorie).Include(x=>x.Author.user).FirstOrDefaultAsync(x=>x.id==id);
           post.Author.user.password="";
           post.Author.user.email="";
           return post;
        }

        public async Task<List<Post>> getPostOfAnAuthor(int id)
        {
            Author author = new Author();
            author=await _managementRepo.getAuthor(id);
            if(author==null){
                return null;
            }
            return await _dataContext.posts.Where(x=>x.authorId==id).ToListAsync();
            
        }
        public async Task<List<Post>> getPostsOfAUser(int id)
        {
            Author author = new Author();
            author=await _managementRepo.getAuthorByUserId(id);
            int theId=author.id;
            if(author==null){
                return null;
            }
            return await _dataContext.posts.Where(x=>x.authorId==theId).Include(x=>x.Author).Include(x=>x.Categorie).ToListAsync();
            
        }

        public async Task<List<Post>> getPosts(){
           
           return await _dataContext.posts.Include(p=>p.Author.user).Include(p=>p.Author).Include(p=>p.Categorie).ToListAsync();
        }

        public PageList<Post> getPostsStrict(PostParams postParams){
            List<Post> posts=  _dataContext.posts.OrderByDescending(x=>x.date.Date).Include(x=>x.reactionSet).Include(p=>p.Author.user).Include(p=>p.Author).Include(p=>p.Categorie).ToList();

        PageList<Post> pageList = new PageList<Post>();
         
        if(postParams.sortBy==""||postParams.sortBy==null){
            posts=  _dataContext.posts.Include(x=>x.reactionSet).Include(p=>p.Author.user).Include(p=>p.Author).Include(p=>p.Categorie).ToList();
        }
        else if(postParams.sortBy=="Top"){
        Post[] ppp=posts.ToArray();
        for(int i = 0;i<ppp.Length;i++){
            for(int j=0;j<ppp.Length;j++){
                if(ppp[i].reactionSet!=null){
                     if(ppp[i].reactionSet.ToArray().Length>ppp[j].reactionSet.ToArray().Length&&(ppp[i].date>=ppp[j].date)){
                        var temp = ppp[i];
                            ppp[i]=ppp[j];
                            ppp[j]=temp;}

                }
               
            }
        }
        posts=ppp.ToList();
        }
         else if(postParams.sortBy=="New"){
             posts=  _dataContext.posts.OrderByDescending(x=>x.date.Date).Include(p=>p.Author.user).Include(p=>p.Author).Include(p=>p.Categorie).ToList();
            
        }
        
        pageList.addCreationAsync(posts.AsQueryable(),postParams.pageNumber,postParams.pageSize,postParams.sortBy);
        return  pageList;

        }
        public PageList<Post> getPostsByTopUserCategorie(PostParams postParams){
        List<Post> posts=  _dataContext.posts.OrderByDescending(x=>x.date.Date).Include(x=>x.reactionSet).Include(p=>p.Author.user).Include(p=>p.Author).Include(p=>p.Categorie).ToList();

        List<Post> postsByTopUserCategorie = new List<Post>();
        PageList<Post> pageList = new PageList<Post>();
         List<CategorieJoin> userCategories = new List<CategorieJoin>();
            userCategories=this._dataContext.categorieJoins.Where(x=>x.userId==postParams.userId).ToList();
             foreach (var post in posts)
             {
                 if(userCategories.Where(x=>x.categorieId==post.categorieId).Any()){
                 postsByTopUserCategorie.Add(post);
                      }
                 
             }
        posts=postsByTopUserCategorie;
        if(postParams.sortBy==""||postParams.sortBy==null){
            posts=  _dataContext.posts.Include(x=>x.reactionSet).Include(p=>p.Author.user).Include(p=>p.Author).Include(p=>p.Categorie).ToList();
        }
        else if(postParams.sortBy=="Top"){
        Post[] ppp=posts.ToArray();
        for(int i = 0;i<ppp.Length;i++){
            for(int j=0;j<ppp.Length;j++){
                if(ppp[i].reactionSet!=null){
                     if(ppp[i].reactionSet.ToArray().Length>ppp[j].reactionSet.ToArray().Length&&(ppp[i].date>=ppp[j].date)){
                        var temp = ppp[i];
                            ppp[i]=ppp[j];
                            ppp[j]=temp;}

                }
               
            }
        }
        posts=ppp.ToList();
        }
         else if(postParams.sortBy=="New"){
             posts=  _dataContext.posts.OrderByDescending(x=>x.date.Date).Include(p=>p.Author.user).Include(p=>p.Author).Include(p=>p.Categorie).ToList();
            
        }
        
        pageList.addCreationAsync(posts.AsQueryable(),postParams.pageNumber,postParams.pageSize,postParams.sortBy);
        return  pageList;

        }
        public async  Task<bool> saveAll()
        {
            return await _dataContext.SaveChangesAsync()>0;
        }

        public PageList<Post> getCategoriePosts(PostParams postParams)
        {
            List<Post> posts= new List<Post>();
            throw new NotImplementedException();
 
        }

        public PageList<Post>  getPostsPaging(PostParams postParams)
        {       
        PageList<Post> pageList = new PageList<Post>();

            List<Post> posts = new List<Post>();

            if(postParams.pagingAplicant=="Categorie"){
                posts=  _dataContext.posts.OrderByDescending(x=>x.date.Date).Include(x=>x.reactionSet).Include(p=>p.Author.user).Include(p=>p.Author).Include(p=>p.Categorie).ToList();

                if(postParams.categorieId!=0){
                    posts= this._dataContext.posts.OrderByDescending(x=>x.date).Where(x=>x.categorieId==postParams.categorieId).Include(x=>x.Categorie).Include(x=>x.Author).Include(x=>x.reactionSet).Include(x=>x.Author.user).ToList();
                    if(postParams.sortBy=="New"){
                        posts=posts.OrderByDescending(x=>x.date).ToList();
                    }
                    if(postParams.sortBy=="Top"){
                         posts=sortAsTop(posts);
                }
                if(postParams.sortBy==""||postParams.sortBy==null){
                   posts=sortAsTop(posts);

                }
            }}
            if(postParams.pagingAplicant=="User"){
                posts=  _dataContext.posts.Where(x=>x.Author.userId==postParams.userToViewId).OrderByDescending(x=>x.date.Date).Include(x=>x.reactionSet).Include(p=>p.Author.user).Include(p=>p.Author).Include(p=>p.Categorie).ToList();

                if(postParams.userToViewId!=0){
                    posts= this._dataContext.posts.Where(x=>x.Author.userId==postParams.userToViewId).OrderByDescending(x=>x.date).Where(x=>x.Author.userId==postParams.userToViewId).Include(x=>x.Categorie).Include(x=>x.Author).Include(x=>x.reactionSet).Include(x=>x.Author.user).ToList();
                    if(postParams.sortBy=="New"){
                        posts=posts.OrderByDescending(x=>x.date).ToList();
                    }
                    if(postParams.sortBy=="Top"){
                        posts=sortAsTop(posts);
                }
                if(postParams.sortBy==""||postParams.sortBy==null){
                   posts=sortAsTop(posts);

                }
            }}

            if(postParams.pagingAplicant=="Home"){
                posts=  _dataContext.posts.OrderByDescending(x=>x.date.Date).Include(x=>x.reactionSet).Include(p=>p.Author.user).Include(p=>p.Author).Include(p=>p.Categorie).ToList();

                if(postParams.userId!=0){
                    posts=  _dataContext.posts.OrderByDescending(x=>x.date.Date).Include(x=>x.reactionSet).Include(p=>p.Author.user).Include(p=>p.Author).Include(p=>p.Categorie).ToList();

                    if(postParams.sortBy=="New"){
                        posts=getCategoriesPostsInRefrenseToUser(posts,postParams.userId);
                        posts=posts.OrderByDescending(x=>x.date).ToList();
                    }
                    if(postParams.sortBy=="Top"){

                    posts=getCategoriesPostsInRefrenseToUser(posts,postParams.userId);
                    posts=sortAsTop(posts);

                    }
                    if(postParams.sortBy==""||postParams.sortBy==null){
                    posts=getCategoriesPostsInRefrenseToUser(posts,postParams.userId);
                    posts=sortAsTop(posts);
                    }
            }
            else{
                posts=  _dataContext.posts.OrderByDescending(x=>x.date.Date).Include(x=>x.reactionSet).Include(p=>p.Author.user).Include(p=>p.Author).Include(p=>p.Categorie).ToList();
                if(postParams.sortBy=="New"){
                    posts=posts.OrderByDescending(x=>x.date).ToList();
                }
                if(postParams.sortBy=="Top"){
                   posts=sortAsTop(posts);

                }
                if(postParams.sortBy==""||postParams.sortBy==null){
                   posts=sortAsTop(posts);

                }
            
            }
           }
        for(int i=0;i<posts.ToArray().Length;i++){
            posts[i].Author.user.password=null;
            posts[i].Author.user.email=null;
        }
        pageList.addCreationAsync(posts.AsQueryable(),postParams.pageNumber,postParams.pageSize,postParams.sortBy);
        return  pageList;        }

       
       private List<Post> getCategoriesPostsInRefrenseToUser(List<Post> posts,int userId){

        List<Post> postsByTopUserCategorie = new List<Post>();
        PageList<Post> pageList = new PageList<Post>();
         List<CategorieJoin> userCategories = new List<CategorieJoin>();
            userCategories=this._dataContext.categorieJoins.Where(x=>x.userId==userId).ToList();
             foreach (var post in posts)
             {
                 if(userCategories.Where(x=>x.categorieId==post.categorieId).Any()){
                 postsByTopUserCategorie.Add(post);
                      }
                 
             }
        posts=postsByTopUserCategorie;        
        return posts;   
       }
       private List<Post> sortAsTop(List<Post> posts){
           
            Post[] ppp=posts.ToArray();
            for(int i = 0;i<ppp.Length;i++){
                for(int j=0;j<ppp.Length;j++){
                 if(ppp[i].reactionSet!=null){

                    if(ppp[i].reactionSet.ToArray().Length>ppp[j].reactionSet.ToArray().Length&&(ppp[i].date.Date.AddYears(0)>=ppp[j].date.Date.AddYears(0))){
                     var temp = ppp[i];
                         ppp[i]=ppp[j];
                         ppp[j]=temp;}
                        }
                   }}
                   posts=ppp.ToList();
                   return posts;
       }
    }
}