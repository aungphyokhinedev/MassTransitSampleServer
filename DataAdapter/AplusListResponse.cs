public class AplusListResponse {

    public int page {get;set;}
    public int pageSize{get;set;}

    public int total{get;set;}
    
    public IEnumerable<dynamic>? rows {get;set;}

}