using Games_Catalog_N01589651.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

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

        //Find Game

        //Add Game

        //Update Game

        //Delete Game

    }
}
