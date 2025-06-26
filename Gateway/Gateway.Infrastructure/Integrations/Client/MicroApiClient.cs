﻿using Gateway.Domain.DTOs.MicroApiClient.Request;
using Gateway.Domain.DTOs.MicroApiClient.Response;
using Gateway.Domain.Extensions;
using Gateway.Domain.Interfaces.Http;
using Gateway.Infrastructure.Integrations.Message.Error;
using Gateway.Infrastructure.Integrations.Resource;
using System.Net;
using System.Text;

namespace Gateway.Infrastructure.Integrations.Client;

public class MicroApiClient : IMicroApiClient
{
    private readonly HttpClient _httpClient;

    public MicroApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;

        ConfigureClient();
    }

    private void ConfigureClient()
    {
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }

    public Task<ApiResponse> DeleteHttpClientRequest(ApiDeleteRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse> GetHttpClientRequest(ApiGetRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse> PostHttpClientRequest(ApiPostRequest postRequest)
    {
        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, postRequest.Uri);
            request.Content = new StringContent(postRequest.Content!, Encoding.UTF8, "application/json");

            foreach (KeyValuePair<string, string> header in postRequest.Headers!)
                request.Headers.Add(header.Key, header.Value);

            using var cts = new CancellationTokenSource();
            var response = await _httpClient.SendAsync(request, cts.Token);
            string contentResponse = await response.Content.ReadAsStringAsync();

            return new(response.StatusCode, contentResponse);
        }
        catch
        {
            return new ApiResponse(HttpStatusCode.InternalServerError, new ProblemResponse() { Message = IntegrationMessage.Gateway_Client_Request_Fail }.SerializeObject());
        }
    }

    public Task<ApiResponse> PutHttpClientRequest(ApiPutRequest request)
    {
        throw new NotImplementedException();
    }
}
