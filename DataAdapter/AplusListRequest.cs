public class AplusListRequest {

   public AplusListRequest( string tables, int page = 1, int pageSize = 10, string fields = "*" ){
       this.page = page;
       this.pageSize = pageSize.IsPositiveNumber() ? pageSize : 1;
       this.fields = fields;
       this.tables = tables;
   }    

   public int page {get;}
   public  int pageSize{get;}
   public  string? fields{get;set;}
   public  string? where{get;set;}
   public  string? tables {get;}

   public  string? orderBy {get;set;}
   public  string? groupBy {get;set;}

}