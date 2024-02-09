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
    public class DeveloperDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all Developer in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Developer in the database..
        /// </returns>
        /// <example>
        /// GET: api/DeveloperData/ListDeveloper
        /// </example>

        [HttpGet]
        [Route("api/DeveloperData/ListDeveloper")]

        public List<DeveloperDto> ListDeveloper()
        {
            List<Developer> Developers = db.Developers.ToList();
            List<DeveloperDto> DeveloperDtos = new List<DeveloperDto>();

            Developers.ForEach(d => DeveloperDtos.Add(new DeveloperDto()
            {
                DeveloperId = d.DeveloperId,
                DeveloperStudioName = d.DeveloperStudioName
            }
            ));
            return DeveloperDtos;
        }

        /// <summary>
        /// Returns all Developer in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: An Developer in the system matching up to the Developer ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the Developer</param>
        /// <example>
        /// GET: api/DeveloperData/FindDeveloper/2
        /// </example>
         
        [ResponseType(typeof(Developer))]
        [HttpGet]
        [Route("api/DeveloperData/FindDeveloper/{id}")]
        public IHttpActionResult FindDeveloper(int id)
        {
            Developer Developer = db.Developers.Find(id);
            DeveloperDto DeveloperDto = new DeveloperDto()
            {
                DeveloperId = Developer.DeveloperId,
                DeveloperStudioName = Developer.DeveloperStudioName
            };
            if (Developer == null)
            {
                return NotFound();
            }

            return Ok(DeveloperDto);
        }

        /// <summary>
        /// Updates a particular Developer in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the Developer ID primary key</param>
        /// <param name="Developer">JSON FORM DATA of an Developer</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/DeveloperData/UpdateDeveloper/5
        /// FORM DATA: Developer JSON Object
        /// </example>
         
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/DeveloperData/UpdateDeveloper/{id}")]
        public IHttpActionResult UpdateDeveloper(int id, Developer Developer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Developer.DeveloperId)
            {

                return BadRequest();
            }

            db.Entry(Developer).State = EntityState.Modified;

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
        /// Adds an Developer to the system
        /// </summary>
        /// <param name="Developer">JSON FORM DATA of an Developer</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Developer ID, Developer Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/DeveloperData/AddDeveloper
        /// FORM DATA: Developer JSON Object
        /// </example>
         
        [ResponseType(typeof(Developer))]
        [HttpPost]
        [Route("api/DeveloperData/AddDeveloper")]
        public IHttpActionResult AddDeveloper(Developer Developer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Developers.Add(Developer);
            db.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Deletes a Developer from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the Developer</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/DeveloperData/DeleteDeveloper/2
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(Developer))]
        [HttpPost]
        [Route("api/DeveloperData/DeleteDeveloper/{id}")]
        public IHttpActionResult DeleteDeveloper(int id)
        {
            Developer Developer = db.Developers.Find(id);
            if (Developer == null)
            {
                return NotFound();
            }

            db.Developers.Remove(Developer);
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
            return db.Developers.Count(e => e.DeveloperId == id) > 0;
        }
    }
}
