using Business;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api.Filters;
using Api.Utils;
using Microsoft.Practices.Unity;

namespace Api.Controllers
{
    [Auth]
    [RoutePrefix("api/request")]
    public class MediaRequestController : ApiController
    {
        [Route("")]
        public HttpResponseMessage Get()
        {
            User currentUser = _route.GetData("user") as User;
            if (currentUser == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Constants.UserNotFoundError);
            return Request.CreateResponse(_mediaRequestContext.GetMultipleByUserName(currentUser.Name));
        }

        [Route("{id}")]
        public HttpResponseMessage Put(int id, [FromBody]MediaRequest request)
        {
            _mediaRequestContext.Save(request);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Dependency]
        public IMediaRequestContext _mediaRequestContext { get; set; }
        [Dependency]
        public IRouteManager _route { get; set; }
    }
}
