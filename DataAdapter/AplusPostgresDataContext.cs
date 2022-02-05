using System.Reflection;
using Dapper;
using Npgsql;

public class AplusPostgresDataContext : IAplusDataContext
{


    public async Task<AplusListResponse> GetListAsync(AplusListRequest request)
    {
        using (var connection = new NpgsqlConnection("Host=54.169.8.73;Username=postgres;Password=newpassword;Database=aplus"))
        {
            try
            {
                connection.Open();

                string query = $"Select {request.fields} from {request.tables}";
                object parameters = new object{};
                //adding where clause 
                //instead of using value directly
                //consider using parameter to prevent SQL injection
                //eg where name = @name and then add @name value in request parameters
                if (request.condition.IsNotNullOrEmpty())
                {
                    query = $"{query} where {request.condition.where}";
                    parameters = request.condition.parameters;
                }

                //adding group by clause
                if (request.groupBy.IsNotNullOrEmpty())
                {
                    query = $"{query} group by {request.groupBy}";
                }

                //adding order by clause
                if (request.orderBy.IsNotNullOrEmpty())
                {
                    query = $"{query}  order by {request.orderBy}";
                }


                query = $"{query} offset {((request.page - 1) * request.pageSize)} limit {request.pageSize};";



                var value = await connection.QueryAsync(query, parameters);

                ///get total for pagination
                long total = 0;
                query = $"select count(*) as total_rows from  {request.tables}";
                if (request.condition.IsNotNullOrEmpty())
                {
                    query = $"{query} where {request.condition.where};";
                }
                var countvalue = await connection.QueryAsync(query, parameters);
                total = countvalue.SingleOrDefault().total_rows;


                return new AplusListResponse
                {
                    code = ResultCode.Success,
                    total = total,
                    page = request.page,
                    pageSize = request.pageSize,
                    rows = value,
                };
            }
            catch (Exception e)
            {
                return new AplusListResponse
                {
                    code = ResultCode.Fail,
                    message = e.Message
                };
            }
        }

    }

    public async Task<AplusDataResponse> AddAsync(object data)
    {
        using (var connection = new NpgsqlConnection("Host=54.169.8.73;Username=postgres;Password=newpassword;Database=aplus"))
        {
            try
            {
                connection.Open();
                var props = data.PropertiesFromInstance();
                string columns = String.Join(",", props.Keys.ToList<string>());
                string parameters = String.Join(",", props.Keys.ToList<object>().Select(i => $"@{i}"));
                string query = $"INSERT INTO {data.GetType()} ({columns}) VALUES ({parameters}) RETURNING Id;";

                int Id = await connection.ExecuteScalarAsync<int>(query, data);
                var created = await connection.QueryAsync($"SELECT * FROM {data.GetType()} WHERE id = {Id};");
                return new AplusDataResponse
                {
                    code = ResultCode.Success,
                    message = "ok",
                    data = created
                };
            }
            catch (Exception e)
            {
                return new AplusDataResponse
                {
                    code = ResultCode.Fail,
                    message = e.Message
                };
            }
        }
        throw new NotImplementedException();
    }


}