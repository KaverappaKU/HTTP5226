using Games_Catalog_N01589651.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Games_Catalog_N01589651.Controllers
{
    public class GenreDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all Genre in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Genre in the database..
        /// </returns>
        /// <example>
        /// GET: api/GenreData/ListGenre
        /// </example>

        [HttpGet]
        [Route("api/GenreData/ListGenre")]

        public List<GenreDto> ListGenre()
        {
            List<Genre> Genres = db.Genres.ToList();
            List<GenreDto> GenreDtos = new List<GenreDto>();

            Genres.ForEach(g => GenreDtos.Add(new GenreDto()
            {
                GenreId = g.GenreId,
                GenreName = g.GenreName,
            }
            ));
            return GenreDtos;
        }

        /// <summary>
        /// Returns all Genre in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: An Genre in the system matching up to the Genre ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the Genre</param>
        /// <example>
        /// GET: GET: api/GenreData/FindGenre/2
        /// </example>
         
        [ResponseType(typeof(Genre))]
        [HttpGet]
        [Route("api/GenreData/FindGenre/{id}")]
        public IHttpActionResult FindGenre(int id)
        {
            Genre Genre = db.Genres.Find(id);
            GenreDto GenreDto = new GenreDto()
            {
                GenreId = Genre.GenreId,
                GenreName = Genre.GenreName,
            };
            if (Genre == null)
            {
                return NotFound();
            }

            return Ok(GenreDto);
        }

        /// <summary>
        /// Updates a particular Genre in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the Genre ID primary key</param>
        /// <param name="Genre">JSON FORM DATA of an Genre</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/GenreData/UpdateGenre/5
        /// FORM DATA: Genre JSON Object
        /// </example>
         
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/GenreData/UpdateGenre/{id}")]
        public IHttpActionResult UpdateGenre(int id, Genre Genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Genre.GenreId)
            {

                return BadRequest();
            }

            db.Entry(Genre).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Adds an Genre to the system
        /// </summary>
        /// <param name="Genre">JSON FORM DATA of an Genre</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Genre ID, Genre Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/GenreData/AddGenre
        /// FORM DATA: Genre JSON Object
        /// </example>
         
        [ResponseType(typeof(Genre))]
        [HttpPost]
        [Route("api/GenreData/AddGenre")]
        public IHttpActionResult AddGenre(Genre Genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Genres.Add(Genre);
            db.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Deletes a Genre from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the Genre</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/GenreData/DeleteGenre/2
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(Genre))]
        [HttpPost]
        [Route("api/GenreData/DeleteGenre/{id}")]
        public IHttpActionResult DeleteGenre(int id)
        {
            Genre Genre = db.Genres.Find(id);
            if (Genre == null)
            {
                return NotFound();
            }

            db.Genres.Remove(Genre);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GenreExists(int id)
        {
            return db.Genres.Count(e => e.GenreId == id) > 0;
        }
    }
}
