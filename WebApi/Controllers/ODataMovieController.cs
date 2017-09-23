using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Models;
using WebApi.Repository;
using System.Web.OData;
using System.Web.OData.Query;
using Microsoft.OData;

namespace SwaggerTest.Controllers
{
    public class ODataMovieController : ODataController
    {
        private MovieRepository _movieRepository = new MovieRepository();
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        // GET: odata/ODataMovie
        [EnableQuery]
        public async Task<IHttpActionResult> Get(ODataQueryOptions<Movie> queryOptions)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(_movieRepository.Query());
        }

        // GET: odata/ODataMovie(5)
        public async Task<IHttpActionResult> GetMovie([FromODataUri] System.Guid? key, ODataQueryOptions<Movie> queryOptions)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            var movie = _movieRepository.Query().Where(x => x.Id == key);
            return Ok(movie);
        }

        // POST: odata/ODataMovie
        public async Task<IHttpActionResult> Post(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _movieRepository.Save(movie);

            return Created(movie);
        }

        // PUT: odata/ODataMovie(5)
        public async Task<IHttpActionResult> Put([FromODataUri] System.Guid? key, Delta<Movie> delta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movie = await _movieRepository.Get(key.Value);
            if (movie == null)
                return NotFound();

            delta.Patch(movie);

            return Updated(movie);
        }

        // PATCH: odata/ODataMovie(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] System.Guid? key, Delta<Movie> delta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movie = await _movieRepository.Get(key.Value);
            if (movie == null)
                return NotFound();

            delta.Patch(movie);

            return Updated(movie);
        }

        // DELETE: odata/ODataMovie(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] System.Guid? key)
        {
            await _movieRepository.Delete(key.Value);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
