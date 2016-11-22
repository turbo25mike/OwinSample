using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Business;
using Microsoft.Practices.Unity;

namespace Api.Filters
{
    public class ExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        [Dependency]
        public IErrorManager _errorManager { get; set; }
       
        public override void OnException(HttpActionExecutedContext context)
        {
            _errorManager.Handle(context.Exception);
            if (context.Exception is NotImplementedException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
            }
            else
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}