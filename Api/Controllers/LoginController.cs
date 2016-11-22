using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api.Utils;
using Business;
using Microsoft.Practices.Unity;

namespace Api.Controllers
{
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        [Route("")]
        public HttpResponseMessage Post(User user)
        {
            if (_userContext.IsValid(user))
            {
                string token = _cache.SetItem(user);

                return Request.CreateResponse(HttpStatusCode.OK, token);
            }
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }
        
        [Dependency]
        public IUserContext _userContext { get; set; }
        [Dependency]
        public ICache _cache { get; set; }
    }
}