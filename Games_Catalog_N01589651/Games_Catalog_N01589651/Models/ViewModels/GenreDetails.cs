using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Games_Catalog_N01589651.Models.ViewModels
{
    public class GenreDetails
    {
        public GenreDto SelectedGenre { get; set; }

        public IEnumerable<GameDto> GameCollection { get; set; }
    }
}