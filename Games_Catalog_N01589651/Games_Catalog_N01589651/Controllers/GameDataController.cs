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
        

        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all games in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all games in the database, including their associated genre and developers.
        /// </returns>
        /// <example>
        /// GET: api/GameData/ListGames
        /// </example>

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
                GenreId = g.Genres.GenreId,
                GenreName = g.Genres.GenreName,
                DeveloperId = g.Developers.DeveloperId,
                DeveloperName = g.Developers.DeveloperStudioName
            }
            ));
            return GameDtos;
        }

        /// <summary>
        /// Returns all games in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: A game in the system matching up to the gamme ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the game</param>
        /// <example>
        /// GET: api/GameData/FindGame/2
        /// </example>
     
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
                GenreId = Game.Genres.GenreId,
                GenreName = Game.Genres.GenreName,
                DeveloperId = Game.Developers.DeveloperId,
                DeveloperName = Game.Developers.DeveloperStudioName

            };
            if (Game == null)
            {
                return NotFound();
            }

            return Ok(GameDto);
        }

        /// <summary>
        /// Gathers information about all games related to a particular Developer ID
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all games in the database, including their associated developers matched with a particular developer ID
        /// </returns>
        /// <param name="id">Developer ID.</param>
        /// <example>
        /// GET: api/GameData/ListGamesForDevelopers/3
        /// </example>
        [HttpGet]
        [ResponseType(typeof(GameDto))]
        [Route("api/GameData/ListGamesForDevelopers/{id}")]
        public IHttpActionResult ListGamesForDevelopers(int id)
        {
            List<Game> Games = db.Games.Where(a => a.DeveloperId == id).ToList();
            List<GameDto> GameDtos = new List<GameDto>();

            Games.ForEach(g => GameDtos.Add(new GameDto()
            {
                GameId = g.GameId,
                GameName = g.GameName,
                ReleaseDate = g.ReleaseDate,
                Price = g.Price,
                Description = g.Description,
                DeveloperId = g.Developers.DeveloperId,
                DeveloperName = g.Developers.DeveloperStudioName
            }
            ));

            return Ok(GameDtos);
        }

        /// <summary>
        /// Gathers information about all games related to a particular Genre ID
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all games in the database, including their associated Genre matched with a particular Genre ID
        /// </returns>
        /// <param name="id">Genre ID.</param>
        /// <example>
        /// GET: api/GameData/ListGamesForGenre/3
        /// </example>
        [HttpGet]
        [ResponseType(typeof(GameDto))]
        [Route("api/GameData/ListGamesForGenre/{id}")]
        public IHttpActionResult ListGamesForGenre(int id)
        {
            List<Game> Games = db.Games.Where(a => a.GenreId == id).ToList();
            List<GameDto> GameDtos = new List<GameDto>();

            Games.ForEach(g => GameDtos.Add(new GameDto()
            {
                GameId = g.GameId,
                GameName = g.GameName,
                ReleaseDate = g.ReleaseDate,
                Price = g.Price,
                Description = g.Description,
                GenreId = g.Genres.GenreId,
                GenreName = g.Genres.GenreName            }
            ));

            return Ok(GameDtos);
        }

        /// <summary>
        /// Updates a particular game in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the Game ID primary key</param>
        /// <param name="Game">JSON FORM DATA of an game</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/GameData/UpdateGame/5
        /// FORM DATA: Game JSON Object
        /// </example>
         
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/gamedata/updategame/{id}")]
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

        /// <summary>
        /// Adds an game to the system
        /// </summary>
        /// <param name="Game">JSON FORM DATA of an game</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Game ID, Game Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: POST: api/GameData/AddGame
        /// FORM DATA: Game JSON Object
        /// </example>
         
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

        /// <summary>
        /// Deletes a game from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the game</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/GameData/DeleteGame/2
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(Game))]
        [HttpPost]
        [Route("api/GameData/DeleteGame/{id}")]
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
