using Gateway.Domain.DTOs.MicroApiClient.Request;
using Gateway.Domain.DTOs.MicroApiClient.Response;

namespace Gateway.Domain.Interfaces.Http;

public interface IMicroApiClient
{
    Task<ApiResponse> PostHttpClientRequest(ApiPostRequest request);

    Task<ApiResponse> PutHttpClientRequest(ApiPutRequest request);

    Task<ApiResponse> GetHttpClientRequest(ApiGetRequest request);

    Task<ApiResponse> DeleteHttpClientRequest(ApiDeleteRequest request);
}
