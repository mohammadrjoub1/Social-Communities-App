namespace Website001.API.Helpers{

    public class PostParams{

        public int maxPageSize=10;
        public int PageSize=6;
        public int pageNumber{set;get;}=1;
        public int totalPages{set;get;}
        public string sortBy{set;get;}
        public int userId{set;get;}
        public int categorieId{set;get;}
        public int userToViewId{set;get;}
        public string pagingAplicant{set;get;}

        public int pageSize{
            get{return this.PageSize;}
            set{if(value>this.maxPageSize){ this.PageSize=3;}else{this.PageSize=value;}
        }   }

        
    }}
