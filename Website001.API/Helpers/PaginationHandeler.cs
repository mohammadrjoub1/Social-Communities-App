namespace Website001.API.Helpers{
    public class PaginationHandeler{
        public int pageSize{set;get;}
        public int pageNumber{set;get;}
        public int totalPages{set;get;}
        public int count{set;get;}

        public PaginationHandeler(int pageSize,int pageNumber,int totalPages,int count){
            this.count=count;
            this.pageSize=pageSize;
            this.pageNumber=pageNumber;
            this.totalPages=totalPages;
        }
    }
}