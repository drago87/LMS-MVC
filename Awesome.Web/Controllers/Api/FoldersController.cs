using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Awesome.Data.Contracts;
using Awesome.Model;

namespace Awesome.Web.Controllers.Api
{
    public class FoldersController : ApiBaseController
    {
        public FoldersController(IAwesomeUow uow)
        {
            Uow = uow;
        }

        // GET api/Folders
        [Authorize]
        //public IQueryable Get()
        public IEnumerable<Folder> Get()
        {
            //var model = Uow.Folders.GetAll();
            var model = Uow.Folders.GetSomeFolders(3);
            return model;
        }

        // GET api/Folders/5
        public Folder Get(int id)
        {
            var Folder = Uow.Folders.GetById(id);
            if (Folder != null) return Folder;
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
        }

        // PUT /api/Folders/ - Update an existing Folder
        public HttpResponseMessage Put([FromBody] Folder Folder)
        {
            Uow.Folders.Update(Folder);
            Uow.Commit();
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        // POST /api/Folders - Create a new Folder
        public HttpResponseMessage Post(Folder Folder)
        {
            Uow.Folders.Add(Folder);
            Uow.Commit();

            var response = Request.CreateResponse(HttpStatusCode.Created, Folder);
            return response;
        }

        // DELETE api/movies/5
        public HttpResponseMessage Delete(int id)
        {
            Uow.Folders.Delete(id);
            Uow.Commit();
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}
