using System.Collections.Generic;
using System.Web.Http;

namespace MvcCookiesVsWebBearer.Controllers.Api
{
    [Authorize]
    public class ValueController : ApiController
    {
        // GET: Value
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Value/5
        public string Get(int id)
        {
            return id.ToString();
        }
    }
}
