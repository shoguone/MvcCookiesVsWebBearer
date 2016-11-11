using Newtonsoft.Json.Linq;
using System.Web.Mvc;

namespace MvcCookiesVsWebBearer.Controllers
{
    [Authorize]
    public class ValueController : Controller
    {
        // GET: Value
        public ActionResult Index()
        {
            var result = new string[] { "value1", "value2" };
            var jsonArray = JArray.FromObject(result);
            var jsonString = jsonArray.ToString();
            return View((object)jsonString);
        }
    }
}