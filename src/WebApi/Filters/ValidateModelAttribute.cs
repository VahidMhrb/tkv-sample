using Application.Common.Models.Response.Base;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Application.Constants;

namespace WebApi.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ObjectResult(new ResponseBase<Response>()
                {
                    ResponseCode = Response.InvalidParameters,
                    ErrorMessages = context.ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage),
                })
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
            else
            {
                await base.OnActionExecutionAsync(context, next);
            }
        }
    }
}
