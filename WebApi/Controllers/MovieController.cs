using WebApi.Models;
using WebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class MovieController : ApiController
    {
        private MovieRepository _movieRepository = new MovieRepository();

        // GET: api/Movie
        public IEnumerable<Movie> Get()
        {
            return _movieRepository.Query().ToList();
        }

        // GET: api/Movie/5
        public async Task<IHttpActionResult> Get(Guid id)
        {
            return Ok(await _movieRepository.Get(id));
        }

        // POST: api/Movie
        public async Task<IHttpActionResult> Post([FromBody]Movie movie)
        {
            await _movieRepository.Save(movie);
            return Ok();
        }

        // PUT: api/Movie/5
        public async Task<IHttpActionResult> Put(Guid id, [FromBody]Movie movie)
        {
            var original = await _movieRepository.Get(id);
            if (original == null)
                return NotFound();

            original.Rating = movie.Rating;
            original.Title = movie.Title;

            return Ok();
        }

        // DELETE: api/Movie/5
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            await _movieRepository.Delete(id);
            return Ok();
        }
    }
}
