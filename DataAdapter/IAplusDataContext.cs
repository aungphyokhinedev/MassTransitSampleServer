public interface IAplusDataContext {
    Task<AplusListResponse>  GetListAsync(AplusListRequest request);

    Task<AplusCreateResponse> AddAsync(AplusCreateRequest<T> request);
}