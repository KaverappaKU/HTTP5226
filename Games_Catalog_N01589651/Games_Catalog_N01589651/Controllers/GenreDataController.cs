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

        // GET: api/GenreData/FindGenre/2
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

        // POST: api/GenreData/UpdateGenre/5
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

        // POST: api/GenreData/AddGenre
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
