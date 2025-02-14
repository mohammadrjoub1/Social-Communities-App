using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Website001.API.Helpers{

    public static class Extention{

        public static void AddApplicationError(this HttpResponse response, string message){
            response.Headers.Add("Application-Error",message);
            response.Headers.Add("Access-Control-Expose-Headers","Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin","*");
        }

        public static void AddApplicationPagination(this HttpResponse response, int pageSize,int pageNumber,int totalPages,int count){
            var paginationHandeler = new PaginationHandeler(pageSize,pageNumber,totalPages,count);
            response.Headers.Add("Pagination",JsonConvert.SerializeObject(paginationHandeler));
            response.Headers.Add("Access-Control-Expose-Headers","Pagination");
        }
    }
}