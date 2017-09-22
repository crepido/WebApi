using SwaggerTest.Models;
using SwaggerTest.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SwaggerTest.Controllers
{
    public class ActorController : ApiController
    {
        private ActorRepository _actorRepository = new ActorRepository();

        // GET: api/Actor
        public IEnumerable<Actor> Get()
        {
            return _actorRepository.Query().ToList();
        }

        // GET: api/Actor/5
        public Actor Get(Guid id)
        {
            return _actorRepository.Get(id);
        }

        // POST: api/Actor
        public void Post([FromBody]Actor value)
        {
            _actorRepository.Save(value);
        }

        // PUT: api/Actor/5
        public void Put(Guid id, [FromBody]Actor value)
        {
            _actorRepository.Update(id, value);
        }

        // DELETE: api/Actor/5
        public void Delete(Guid id)
        {
            _actorRepository.Delete(id);
        }
    }
}
