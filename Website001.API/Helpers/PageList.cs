using System;
using System.Collections.Generic;
using System.Linq;
using Website001.API.Data;
using Website001.API.Models;

namespace Website001.API.Helpers{
    public class PageList<T> :List<T>{
        public List<T> Items { get;set; }
        public int PageSize { get;set; }
        public int PageNumber { get;set; }
        public int totalPages{set;get;} 
        public int count{set;get;}
        string sortBy{set;get;}
        int userId{set;get;}
        private DataContext d;
        public PageList(DataContext _d){
            d=_d;
        }

        public PageList()
        {
        }

        public  void addCreationAsync(IQueryable<T> items,int pageNumber,int pageSize,string sortBy){
             List<T> entityToReturn = new List<T>();
            this.count=items.Count();
            this.PageNumber=pageNumber;
            this.PageSize=pageSize;

            this.totalPages=(int) Math.Ceiling(count/((double)pageSize));
           

           entityToReturn =(List<T>)items.Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
           this.Items= entityToReturn.ToList();
           }
        
    }
   
}