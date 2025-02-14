using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Website001.API.Data;
using Website001.API.Helpers;

namespace Website001.API
{
    public class Startup
    {    string Me="asdfasdfa34gtqa3wy4gq";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddDbContext<DataContext>(options =>
            options.UseSqlite("Data Source=database.db"));
            services.AddScoped<IUserRepo,UserRepo>();
            services.AddScoped<IManagementRepo,ManagementRepo>();
            services.AddScoped<IPostRepo,PostRepo>(); 
            services.AddScoped<IReactionRepo,ReactionRepo>();
            services.AddScoped<ICommentRepo,CommentRepo>();

               services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(a => {a.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters{
                ValidateIssuerSigningKey=true,
                IssuerSigningKey= new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Me)),
                ValidateIssuer=false,
                ValidateAudience=false
            };});
            services.AddCors();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Website001.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Website001.API v1"));
            }  
            else{
                app.UseExceptionHandler(options => {
                    
                    options.Run(async context=>{
                        context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                      
                        if(error!=null){
                            context.Response.AddApplicationError(error.Error.Message);
                           await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
            }


            //app.UseHttpsRedirection();

                  app.UseRouting();
            app.UseCors(adasd=>adasd.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
             app.UseAuthentication();
            app.UseAuthorization();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
             });
        }
    }
}
