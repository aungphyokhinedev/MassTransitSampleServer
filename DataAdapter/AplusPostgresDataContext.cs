using System.Reflection;
using Dapper;
using Npgsql;

public class AplusPostgresDataContext : IAplusDataContext
{
    public async Task<AplusListResponse> GetListAsync(AplusListRequest request)
    {
        using (var connection = new NpgsqlConnection("Host=54.169.8.73;Username=postgres;Password=newpassword;Database=aplus")) 
        { 
                connection.Open(); 
              
                string query = $"Select {request.fields}, count(*) OVER() AS total_rows from {request.tables}";
                
                if(request.where.IsNotNullOrEmpty()){
                    query = $"{query} where {request.where}";
                }

                
                 if(request.orderBy.IsNotNullOrEmpty()){
                    query = $"{query} group by {request.groupBy} ";
                }

                if(request.orderBy.IsNotNullOrEmpty()){
                    query += $"{query}  order by {request.orderBy} ";
                }
               

                query = $"{query} offset {((request.page - 1) * request.pageSize)} limit {request.pageSize};";
                
                
               
                var value = await connection.QueryAsync(query); 

                int total = 0;
                if(value.Count().IsPositiveNumber()){
                   
                    total = int.Parse(value.First().GetPropValue(value.First(),"total_rows"));
                }
                else{
                    query = $"select count(*) from  {request.tables}";
                    if(request.where.IsNotNullOrEmpty()){
                        query = $"{query} where {request.where};";
                    }
                    await connection.QueryAsync(query); 
                    total = int.Parse(value.First()[0]);
                }
               
           
            return  new AplusListResponse{
                total = total,
                page = 1,
                pageSize = 20,
                rows = value,
            };
        } 
        
    }
}