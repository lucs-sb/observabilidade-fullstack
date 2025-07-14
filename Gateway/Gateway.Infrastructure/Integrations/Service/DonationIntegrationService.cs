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
using Gateway.Infrastructure.Integrations.Message.Request.Donation;
using Gateway.Infrastructure.Integrations.Message.Response.Donation;
using Mapster;
using Microsoft.Extensions.Options;
using System.Net;

namespace Gateway.Infrastructure.Integrations.Service;

public class DonationIntegrationService : IDonationIntegrationService
{
    private readonly IMicroApiClient _microApiClient;
    private readonly IntegrationSettings _integrationSettings;

    public DonationIntegrationService(IMicroApiClient microApiClient, IOptions<IntegrationSettings> integrationSettings)
    {
        _microApiClient = microApiClient;
        _integrationSettings = integrationSettings.Value;
    }

    public async Task CreateDonationAsync(DonationDTO donationDTO)
    {
        DonationRequest donationRequest = donationDTO.Adapt<DonationRequest>();

        ApiPostRequest apiPostRequest = new()
        {
            Uri = new Uri(_integrationSettings.DonationUri!),
            Content = donationRequest.SerializeObject()
        };

        ApiResponse apiResponse = await _microApiClient.PostHttpClientRequest(apiPostRequest);

        if (apiResponse.StatusCode != HttpStatusCode.NoContent)
        {
            ProblemResponse problemResponse = apiResponse.Content.DeserializeObject<ProblemResponse>();

            throw new HttpRequestFailGatewayException((int)apiResponse.StatusCode, problemResponse.Message!);
        }
    }

    public async Task DeleteDonationAsync(Guid id)
    {
        ApiDeleteRequest apiDeleteRequest = new()
        {
            Uri = new Uri(_integrationSettings.DonationUri! + $"/{id}")
        };

        ApiResponse apiResponse = await _microApiClient.DeleteHttpClientRequest(apiDeleteRequest);

        if (apiResponse.StatusCode != HttpStatusCode.NoContent)
        {
            ProblemResponse problemResponse = apiResponse.Content.DeserializeObject<ProblemResponse>();

            throw new HttpRequestFailGatewayException((int)apiResponse.StatusCode, problemResponse.Message!);
        }
    }

    public async Task<List<DonationResponseDTO>> GetAllDonationsAsync(Guid donorId)
    {
        ApiGetRequest apiGetRequest = new()
        {
            Uri = new Uri(_integrationSettings.DonationUri! + $"/donor/{donorId}")
        };

        ApiResponse apiResponse = await _microApiClient.GetHttpClientRequest(apiGetRequest);

        if (apiResponse.StatusCode != HttpStatusCode.OK)
        {
            ProblemResponse problemResponse = apiResponse.Content.DeserializeObject<ProblemResponse>();

            throw new HttpRequestFailGatewayException((int)apiResponse.StatusCode, problemResponse.Message!);
        }

        List<DonationResponse> donationsResponse = apiResponse.Content.DeserializeObject<List<DonationResponse>>();

        return donationsResponse.Adapt<List<DonationResponseDTO>>();
    }

    public async Task<DonationResponseDTO> GetDonationByIdAsync(Guid id)
    {
        ApiGetRequest apiGetRequest = new()
        {
            Uri = new Uri(_integrationSettings.DonationUri! + $"/{id}")
        };

        ApiResponse apiResponse = await _microApiClient.GetHttpClientRequest(apiGetRequest);

        if (apiResponse.StatusCode != HttpStatusCode.OK)
        {
            ProblemResponse problemResponse = apiResponse.Content.DeserializeObject<ProblemResponse>();

            throw new HttpRequestFailGatewayException((int)apiResponse.StatusCode, problemResponse.Message!);
        }

        DonationResponse donationResponse = apiResponse.Content.DeserializeObject<DonationResponse>();

        return donationResponse.Adapt<DonationResponseDTO>();
    }

    public async Task UpdateDonationAsync(Guid id, DonationDTO donationDTO)
    {
        DonationRequest donationRequest = donationDTO.Adapt<DonationRequest>();

        ApiPutRequest apiPutRequest = new()
        {
            Uri = new Uri(_integrationSettings.DonationUri! + $"/{id}"),
            Content = donationRequest.SerializeObject()
        };

        ApiResponse apiResponse = await _microApiClient.PutHttpClientRequest(apiPutRequest);

        if (apiResponse.StatusCode != HttpStatusCode.Accepted)
        {
            ProblemResponse problemResponse = apiResponse.Content.DeserializeObject<ProblemResponse>();

            throw new HttpRequestFailGatewayException((int)apiResponse.StatusCode, problemResponse.Message!);
        }
    }
}
