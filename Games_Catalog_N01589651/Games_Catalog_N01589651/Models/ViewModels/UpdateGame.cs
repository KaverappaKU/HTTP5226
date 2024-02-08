using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Games_Catalog_N01589651.Models.ViewModels
{
    public class UpdateGame
    {
        //This viewmodel is a class which stores information that we need to present to /Game/Update/{}

        //the existing game information

        public GameDto SelectedGame { get; set; }

        // all genre to choose from when updating this game

        public IEnumerable<GenreDto> GenreOptions { get; set; }

        // all developer to choose from when updating this game

        public IEnumerable<DeveloperDto> DeveloperOptions { get; set; }
    }
}