public interface IAplusDataContext {
    Task<AplusListResponse>  GetListAsync(AplusListRequest request);

    Task<AplusDataResponse> AddAsync(object data);
}