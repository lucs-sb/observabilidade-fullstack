using Gateway.Domain.DTOs;
using Gateway.Domain.DTOs.MicroApiClient.Request;
using Gateway.Domain.DTOs.MicroApiClient.Response;
using Gateway.Domain.DTOs.Response;
using Gateway.Domain.Exceptions;
using Gateway.Domain.Extensions;
using Gateway.Domain.Interfaces.Http;
using Gateway.Domain.Interfaces.Integration;
using Gateway.Domain.Settings;
using Gateway.Infrastructure.Integrations.Message.Error;
using Gateway.Infrastructure.Integrations.Message.Request.Donor;
using Gateway.Infrastructure.Integrations.Message.Response.Donor;
using Mapster;
using Microsoft.Extensions.Options;
using System.Net;

namespace Gateway.Infrastructure.Integrations.Service;

public class DonorIntegrationService : IDonorIntegrationService
{
    private readonly IMicroApiClient _microApiClient;
    private readonly IntegrationSettings _integrationSettings;

    public DonorIntegrationService(IMicroApiClient microApiClient, IOptions<IntegrationSettings> integrationSettings)
    {
        _microApiClient = microApiClient;
        _integrationSettings = integrationSettings.Value;
    }

    public async Task CreateDonorAsync(DonorDTO donorDTO)
    {
        DonorRequest donorRequest = donorDTO.Adapt<DonorRequest>();

        ApiPostRequest apiPostRequest = new()
        {
            Uri = new Uri(_integrationSettings.DonorUri!),
            Content = donorRequest.SerializeObject()
        };

        ApiResponse apiResponse = await _microApiClient.PostHttpClientRequest(apiPostRequest);

        if (apiResponse.StatusCode != HttpStatusCode.NoContent)
        {
            ProblemResponse problemResponse = apiResponse.Content.DeserializeObject<ProblemResponse>();

            throw new HttpRequestFailGatewayException((int)apiResponse.StatusCode, problemResponse.Message!);
        }
    }

    public async Task DeleteDonorAsync(Guid id)
    {
        ApiDeleteRequest apiDeleteRequest = new()
        {
            Uri = new Uri(_integrationSettings.DonorUri! + $"/{id}")
        };

        ApiResponse apiResponse = await _microApiClient.DeleteHttpClientRequest(apiDeleteRequest);

        if (apiResponse.StatusCode != HttpStatusCode.NoContent)
        {
            ProblemResponse problemResponse = apiResponse.Content.DeserializeObject<ProblemResponse>();

            throw new HttpRequestFailGatewayException((int)apiResponse.StatusCode, problemResponse.Message!);
        }
    }

    public async Task<List<DonorResponseDTO>> GetAllDonorsAsync()
    {
        ApiGetRequest apiGetRequest = new()
        {
            Uri = new Uri(_integrationSettings.DonorUri!)
        };

        ApiResponse apiResponse = await _microApiClient.GetHttpClientRequest(apiGetRequest);

        if (apiResponse.StatusCode != HttpStatusCode.OK)
        {
            ProblemResponse problemResponse = apiResponse.Content.DeserializeObject<ProblemResponse>();

            throw new HttpRequestFailGatewayException((int)apiResponse.StatusCode, problemResponse.Message!);
        }

        List<DonorResponse> donorsResponse = apiResponse.Content.DeserializeObject<List<DonorResponse>>();

        return donorsResponse.Adapt<List<DonorResponseDTO>>();
    }

    public async Task<DonorResponseDTO> GetDonorByIdAsync(Guid id)
    {
        ApiGetRequest apiGetRequest = new()
        {
            Uri = new Uri(_integrationSettings.DonorUri! + $"/{id}")
        };

        ApiResponse apiResponse = await _microApiClient.GetHttpClientRequest(apiGetRequest);

        if (apiResponse.StatusCode != HttpStatusCode.OK)
        {
            ProblemResponse problemResponse = apiResponse.Content.DeserializeObject<ProblemResponse>();

            throw new HttpRequestFailGatewayException((int)apiResponse.StatusCode, problemResponse.Message!);
        }

        DonorResponse donorResponse = apiResponse.Content.DeserializeObject<DonorResponse>();

        return donorResponse.Adapt<DonorResponseDTO>();
    }

    public async Task UpdateDonorAsync(Guid id, DonorDTO donorDTO)
    {
        DonorRequest donorRequest = donorDTO.Adapt<DonorRequest>();

        ApiPutRequest apiPutRequest = new()
        {
            Uri = new Uri(_integrationSettings.DonorUri! + $"/{id}"),
            Content = donorRequest.SerializeObject()
        };

        ApiResponse apiResponse = await _microApiClient.PutHttpClientRequest(apiPutRequest);

        if (apiResponse.StatusCode != HttpStatusCode.Accepted)
        {
            ProblemResponse problemResponse = apiResponse.Content.DeserializeObject<ProblemResponse>();

            throw new HttpRequestFailGatewayException((int)apiResponse.StatusCode, problemResponse.Message!);
        }
    }
}
