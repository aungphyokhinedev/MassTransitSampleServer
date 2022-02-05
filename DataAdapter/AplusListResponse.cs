public class AplusListResponse : IAplusResponse{

    public int page {get;set;}
    public int pageSize{get;set;}

    public long total{get;set;}
    
    public IEnumerable<dynamic>? rows {get;set;}

}