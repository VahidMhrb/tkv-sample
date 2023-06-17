using Application.Common.Models.Response.Base;
using Infrastructure.Common.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;

namespace WebApi.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected readonly IHttpContextAccessor _accessor;
        protected readonly ApplicationSetting Settings;
        public ApiControllerBase(IOptionsMonitor<ApplicationSetting> options, IHttpContextAccessor accessor)
        {
            _accessor = accessor;
            Settings = options.CurrentValue;
        }

        protected IActionResult HandleResponse(ResponseBase response)
        {
            return StatusCode(response.HttpStatusCode ?? HttpStatusCode.Conflict, response);
        }
        protected IActionResult HandleResponse<ResponseClass>(ResponseBase<ResponseClass> response)
        {
            return StatusCode(response.HttpStatusCode ?? HttpStatusCode.Conflict, response);
        }
        protected IActionResult HandleResponse<ResponseClass>(ListResponseBase<ResponseClass> response)
        {
            return StatusCode(response.HttpStatusCode ?? HttpStatusCode.Conflict, response);
        }


        protected IActionResult StatusCode(HttpStatusCode statusCode, object value) =>
            StatusCode((int)statusCode, value);
        protected IActionResult StatusCode(HttpStatusCode statusCode) =>
            StatusCode((int)statusCode);
    }
}
