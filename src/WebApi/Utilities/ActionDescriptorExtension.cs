using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace WebApi.Utilities
{
    public static class ActionDescriptorExtension
    {
        public static ApiVersionModel? GetApiVersion(this ActionDescriptor actionDescriptor)
        {
            return actionDescriptor.Properties
              .Where((kvp) => ((Type)kvp.Key).Equals(typeof(ApiVersionModel)))
              .Select(kvp => kvp.Value as ApiVersionModel).FirstOrDefault();
        }
    }
}
