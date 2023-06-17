using System.Net;

namespace Application.Common.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class HttpStatusAttribute : Attribute
    {
        public HttpStatusCode HttpStatus { get; }

        public HttpStatusAttribute(HttpStatusCode httpStatus)
        {
            HttpStatus = httpStatus;
        }
    }
}
