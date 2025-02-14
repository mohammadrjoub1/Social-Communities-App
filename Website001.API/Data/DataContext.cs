using Microsoft.EntityFrameworkCore;
using Website001.API.Models;

namespace Website001.API.Data{
    public class DataContext:DbContext{
    
        public DataContext(DbContextOptions<DataContext> options):base(options){}

        public DbSet<User> users{set;get;}
        public DbSet<Post> posts{set;get;}
        public DbSet<Author> authors{set;get;}
        public DbSet<Categorie> categories{set;get;}
        public DbSet<Admin> admins{set;get;}
        public DbSet<Reaction> reactions{set;get;}
        public DbSet<ReactionSet> reactionSets{set;get;}
        public DbSet<CategorieJoin> categorieJoins{set;get;}
        public DbSet<PostType> PostTypes{set;get;}
        public DbSet<Comment> comments{set;get;}

    }
}