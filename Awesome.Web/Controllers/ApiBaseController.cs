using System.Web.Http;
using Awesome.Data.Contracts;
using System.Web.Mvc;

namespace Awesome.Web.Controllers
{
    public class ApiBaseController : ApiController
    {
        protected IAwesomeUow Uow { get; set; }
    }

    public class MvcBaseController : Controller
    {
        protected IAwesomeUow Uow { get; set; }
    }
}