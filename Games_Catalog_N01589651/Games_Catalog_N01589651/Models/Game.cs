using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Games_Catalog_N01589651.Models
{
    public class Game
    {
        [Key]
        public int GameId { get; set; }
        public string GameName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }

        [ForeignKey("Genres")]
        public int GenreId { get; set; }
        public virtual Genre Genres { get; set; }

        [ForeignKey("Developers")]
        public int DeveloperId { get; set; }
        public virtual Developer Developers { get; set; }

    }

    public class GameDto
    {
        public int GameId { get; set; } 
        public string GameName { get; set; }
        public DateTime ReleaseDate { get; set; }
       
        public string Price { get; set; } 
        public string Description { get; set; }

        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public int DeveloperId { get; set; }
        public string DeveloperName { get;set; }

    }
}