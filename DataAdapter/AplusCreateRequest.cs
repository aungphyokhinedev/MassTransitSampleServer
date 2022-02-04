public class AplusCreateRequest<T> {

   public AplusCreateRequest( string table ){
       this.table = table;
   }    

 
   public  string table {get;}
   public T data{get;} 

}