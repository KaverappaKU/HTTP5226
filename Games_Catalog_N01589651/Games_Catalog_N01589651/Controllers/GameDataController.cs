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
    public class GameDataController : ApiController
    {
        //List Game

        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        [Route("api/GameData/ListGames")]

        public List<GameDto> ListGames()
        {
            List<Game> Games = db.Games.ToList();
            List<GameDto> GameDtos = new List<GameDto>();

            Games.ForEach(g => GameDtos.Add(new GameDto()
            {
                GameId = g.GameId,
                GameName = g.GameName,
                ReleaseDate = g.ReleaseDate,    
                Price = g.Price,
                Description = g.Description,
                GenreName = g.Genres.GenreName,
                DeveloperName = g.Developers.DeveloperStudioName
            }
            ));
            return GameDtos;
        }

        // GET: api/GameData/FindGame/2
        [ResponseType(typeof(Game))]
        [HttpGet]
        [Route("api/gamedata/findgame/{id}")]
        public IHttpActionResult FindGame(int id)
        {
            Game Game = db.Games.Find(id);
            GameDto GameDto = new GameDto()
            {
                GameId = Game.GameId,
                GameName = Game.GameName,
                ReleaseDate = Game.ReleaseDate,
                Price = Game.Price,
                Description = Game.Description,
                GenreName = Game.Genres.GenreName,
                DeveloperName = Game.Developers.DeveloperStudioName

            };
            if (Game == null)
            {
                return NotFound();
            }

            return Ok(GameDto);
        }

        // POST: api/GameData/UpdateGame/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateGame(int id, Game Game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Game.GameId)
            {

                return BadRequest();
            }

            db.Entry(Game).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
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

        // POST: api/GameData/AddGame
        [ResponseType(typeof(Game))]
        [HttpPost]
        [Route("api/gamedata/addgame")]
        public IHttpActionResult AddGame(Game Game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Games.Add(Game);
            db.SaveChanges();

            return Ok();
        }

        // POST: api/GameData/DeleteGame/5
        [ResponseType(typeof(Game))]
        [HttpPost]
        public IHttpActionResult DeleteGame(int id)
        {
            Game Game = db.Games.Find(id);
            if (Game == null)
            {
                return NotFound();
            }

            db.Games.Remove(Game);
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

        private bool GameExists(int id)
        {
            return db.Games.Count(e => e.GameId == id) > 0;
        }

    }
}
