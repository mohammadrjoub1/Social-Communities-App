using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Website001.API.Data;
using Website001.API.Models;

namespace Website001.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        { 
           
           
           var host=  CreateHostBuilder(args).Build();
           using(var scope = host.Services.CreateScope()){
               
               var services = scope.ServiceProvider;
               var context = services.GetRequiredService<DataContext>();
                               context.Database.Migrate();

               if(!context.reactions.Any()){
               Reaction reaction = new Reaction();
               reaction.id=1;
               reaction.name="Like";
                Reaction reaction1 = new Reaction();
               reaction1.id=2;
               reaction1.name="Hate";
                Reaction reaction2 = new Reaction();
               reaction2.id=3;
               reaction2.name="Funny";
               context.reactions.Add(reaction);
               context.reactions.Add(reaction1);
               context.reactions.Add(reaction2);

               PostType postType = new PostType();
               postType.id=1;
               postType.title="post";
               context.PostTypes.Add(postType);


               PostType postType1 = new PostType();
               postType1.id=2;
               postType1.title="image";
               context.PostTypes.Add(postType1);

                context.SaveChanges();

 

                }

           }
           
           host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    
     
    }

}
