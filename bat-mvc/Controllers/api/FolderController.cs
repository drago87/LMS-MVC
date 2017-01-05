using Queries.Core;
using Queries.Core.Domain;
using Queries.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace bat_mvc.Controllers.api
{
    public class FolderController : ApiController
    {
        public readonly IRepository<Folder> _folderRepo;
        public readonly IUnitOfWork _uow;

        public FolderController (IRepository<Folder> folderRepository, IUnitOfWork uow)
        {
            _folderRepo = folderRepository;
            _uow = uow;
        }

        // GET api/Folders
        [Authorize]
        //public IQueryable Get()
        public IEnumerable<Folder> Get()
        {
            //var model = Uow.Folders.GetSomeFolders(3);
            var folders = _uow.Folders.GetAll();
            return folders;
        }

        // GET api/Folders/5
        public Folder Get(int id)
        {
            var Folder = _uow.Folders.Get(id);
            if (Folder != null) return Folder;
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
        }

        //// PUT /api/Folders/ - Update an existing Folder
        //public HttpResponseMessage Put([FromBody] Folder Folder)
        //{
        //    _uow.Folders.Update(Folder);
        //    _uow.Complete();
        //    return new HttpResponseMessage(HttpStatusCode.NoContent);
        //}

        //// POST /api/Folders - Create a new Folder
        //public HttpResponseMessage Post(Folder Folder)
        //{
        //    _uow.Folders.Add(Folder);
        //    _uow.Complete();

        //    var response = Request.CreateResponse(HttpStatusCode.Created, Folder);
        //    return response;
        //}

        //// DELETE api/movies/5
        //public HttpResponseMessage Delete(int id)
        //{
        //    _uow.Folders.Delete(id);
        //    _uow.Complete();
        //    return new HttpResponseMessage(HttpStatusCode.NoContent);
        //}
    }
}