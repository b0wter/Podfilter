using Microsoft.AspNetCore.Mvc;

namespace Podfilter.Controllers
{
    /// <summary>
    /// Base for all other controllers. Contains helper methods.
    /// </summary>
    public abstract class BaseController : Controller
    {
        
    }

    /// <summary>
    /// Base controller for returning html pages.
    /// </summary>
    public abstract class HtmlBaseController : BaseController
    {
        
    }

    /// <summary>
    /// Base controller for returning json/xml/... results.
    /// </summary>
    public abstract class ApiBaseController : BaseController
    {
        protected abstract ActionResult MakeResult(object obj);
    }

    public abstract class JsonApiBaseController : ApiBaseController
    {
        protected override ActionResult MakeResult(object obj)
        {
            return new JsonResult(obj);
        }
    }
}