using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Games_Catalog_N01589651.Models.ViewModels
{
    public class NewGame
    {
        // all genre to choose from when creating this game

        public IEnumerable<GenreDto> GenreOptions { get; set; }

        // all developer to choose from when creating this game

        public IEnumerable<DeveloperDto> DeveloperOptions { get; set; }
    }
}