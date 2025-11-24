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
using Gateway.Infrastructure.Integrations.Message.Request.Auth;
using Gateway.Infrastructure.Integrations.Message.Response.Auth;
using Mapster;
using Microsoft.Extensions.Options;
using System.Net;

namespace Gateway.Infrastructure.Integrations.Service;

public class AuthIntegrationService : IAuthIntegrationService
{
    private readonly IMicroApiClient _microApiClient;
    private readonly IntegrationSettings _integrationSettings;

    public AuthIntegrationService(IMicroApiClient microApiClient, IOptions<IntegrationSettings> integrationSettings)
    {
        _microApiClient = microApiClient;
        _integrationSettings = integrationSettings.Value;
    }

    public async Task<LoginResponseDTO> LoginAsync(LoginDTO loginDTO)
    {
        LoginRequest loginRequest = loginDTO.Adapt<LoginRequest>();

        ApiPostRequest apiPostRequest = new()
        {
            Uri = new Uri(_integrationSettings.AuthUri!),
            Content = loginRequest.SerializeObject()
        };

        ApiResponse apiResponse = await _microApiClient.PostHttpClientRequest(apiPostRequest);

        if (apiResponse.StatusCode != HttpStatusCode.OK)
        {
            ProblemResponse problemResponse = apiResponse.Content.DeserializeObject<ProblemResponse>();

            throw new HttpRequestFailGatewayException((int)apiResponse.StatusCode, problemResponse.Message!);
        }

        LoginResponse loginResponse = apiResponse.Content.DeserializeObject<LoginResponse>();

        return loginResponse.Adapt<LoginResponseDTO>();
    }
}
