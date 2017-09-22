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
    public class MovieController : ApiController
    {
        private MovieRepository _movieRepository = new MovieRepository();

        // GET: api/Movie
        public IEnumerable<Movie> Get()
        {
            return _movieRepository.Query().ToList();
        }

        // GET: api/Movie/5
        public Movie Get(Guid id)
        {
            return _movieRepository.Get(id);
        }

        // POST: api/Movie
        public void Post([FromBody]Movie movie)
        {
            _movieRepository.Save(movie);
        }

        // PUT: api/Movie/5
        public void Put(Guid id, [FromBody]Movie movie)
        {
            _movieRepository.Update(id, movie);
        }

        // DELETE: api/Movie/5
        public void Delete(Guid id)
        {
            _movieRepository.Delete(id);
        }
    }
}
