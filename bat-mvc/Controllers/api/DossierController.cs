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
    public class DossierController : ApiController
    {
        public readonly IRepository<Dossier> _DossierRepo;
        public readonly IUnitOfWork _uow;

        public DossierController (IRepository<Dossier> DossierRepository, IUnitOfWork uow)
        {
            _DossierRepo = DossierRepository;
            _uow = uow;
        }

        // GET api/Dossiers
        [Authorize]
        //public IQueryable Get()
        public IEnumerable<Dossier> Get()
        {
            //var model = Uow.Dossiers.GetSomeDossiers(3);
            var Dossiers = _uow.Dossiers.GetAll();
            return Dossiers;
        }

        // GET api/Dossiers/5
        public Dossier Get(int id)
        {
            var Dossier = _uow.Dossiers.Get(id);
            if (Dossier != null) return Dossier;
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
        }

        //// PUT /api/Dossiers/ - Update an existing Dossier
        //public HttpResponseMessage Put([FromBody] Dossier Dossier)
        //{
        //    _uow.Dossiers.Update(Dossier);
        //    _uow.Complete();
        //    return new HttpResponseMessage(HttpStatusCode.NoContent);
        //}

        //// POST /api/Dossiers - Create a new Dossier
        //public HttpResponseMessage Post(Dossier Dossier)
        //{
        //    _uow.Dossiers.Add(Dossier);
        //    _uow.Complete();

        //    var response = Request.CreateResponse(HttpStatusCode.Created, Dossier);
        //    return response;
        //}

        //// DELETE api/movies/5
        //public HttpResponseMessage Delete(int id)
        //{
        //    _uow.Dossiers.Delete(id);
        //    _uow.Complete();
        //    return new HttpResponseMessage(HttpStatusCode.NoContent);
        //}
    }
}