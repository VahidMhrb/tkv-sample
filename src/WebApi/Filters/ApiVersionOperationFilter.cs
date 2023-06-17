using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApi.Filters
{
    public class ApiVersionOperationFilter : IOperationFilter
    {
        public static ApiVersionModel? GetApiVersion(ActionDescriptor actionDescriptor)
        {
            return actionDescriptor.Properties
              .Where((kvp) => ((Type)kvp.Key).Equals(typeof(ApiVersionModel)))
              .Select(kvp => kvp.Value as ApiVersionModel).FirstOrDefault();
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var actionApiVersionModel = GetApiVersion(context.ApiDescription.ActionDescriptor);
            if (actionApiVersionModel == null)
            {
                return;
            }

            operation.Parameters ??= new List<OpenApiParameter>();

            if (actionApiVersionModel.DeclaredApiVersions.Any())
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "api-version",
                    In = ParameterLocation.Header,
                    Schema = new OpenApiSchema
                    {
                        Type = "string",
                        Enum = actionApiVersionModel.DeclaredApiVersions.Select(i => new OpenApiString(i.ToString())).ToArray(),
                    },
                    Required = true,
                });
            }
            else
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "api-version",
                    In = ParameterLocation.Header,
                    Schema = new OpenApiSchema
                    {
                        Type = "string",
                        Enum = actionApiVersionModel.ImplementedApiVersions.Select(i => new OpenApiString(i.ToString())).ToArray(),
                    },
                    Required = true,
                });
            }
        }
    }
}
