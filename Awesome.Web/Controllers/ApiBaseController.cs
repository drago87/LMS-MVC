using System.Web.Http;
using Awesome.Data.Contracts;

namespace Awesome.Web.Controllers
{
    public class ApiBaseController : ApiController
    {
        protected IAwesomeUow Uow { get; set; }
    }
}