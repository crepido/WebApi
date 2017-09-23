using WebApi.Models;
using WebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;

namespace WebApi.Controllers
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
        public async Task<IHttpActionResult> Get(Guid id)
        {
            return Ok(await _actorRepository.Get(id));
        }

        // POST: api/Actor
        public async Task<IHttpActionResult> Post([FromBody]Actor actor)
        {
            await _actorRepository.Save(actor);
            return Ok();
        }

        // PUT: api/Actor/5
        public async Task<IHttpActionResult> Put(Guid id, [FromBody]Actor actor)
        {
            var original = await _actorRepository.Get(id);
            if (original == null)
                return NotFound();

            original.Name = actor.Name;
            original.Birthday = actor.Birthday;
            original.Gender = actor.Gender;

            return Ok();
        }

        // DELETE: api/Actor/5
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            await _actorRepository.Delete(id);
            return Ok();
        }
    }
}
