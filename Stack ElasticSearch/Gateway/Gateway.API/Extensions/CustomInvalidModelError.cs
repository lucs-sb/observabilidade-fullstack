using Gateway.API.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.API.Extensions;

public class CustomInvalidModelError
{
    public BadRequestObjectResult CustomErrorResponse(ActionContext context)
    {
        Dictionary<string, List<string>> jsonResult = [];

        context.ModelState
            .Where(modelError => modelError.Value!.Errors.Count > 0)
            .Select(modelError =>
            {
                var keyName = modelError.Key;
                var errors = modelError.Value?.Errors.Select(e => e.ErrorMessage).ToList();
                var unexpectedErrors = errors!.Where(e => e.Contains("is not valid") || e.Contains("is invalid")).ToList();

                if (modelError.Key.StartsWith("$.") || unexpectedErrors.Count != 0 || errors!.Contains(""))
                    errors = [ApiMessage.Gateway_Validation_Field_Fail];

                jsonResult.Add(keyName, errors!);
                return string.Empty;

            }).ToList();

        if (jsonResult.Count > 1 && jsonResult.ContainsKey("Body"))
            jsonResult.Remove("Body");

        var objectResult = new
        {
            message = "Requisição inválida",
            errors = jsonResult.Select(entry => new
            {
                field = entry.Key.Replace("$.", string.Empty),
                message = entry.Value
            }).ToList()
        };

        return new BadRequestObjectResult(objectResult);
    }
}