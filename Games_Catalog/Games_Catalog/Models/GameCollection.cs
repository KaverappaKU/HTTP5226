using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Games_Catalog.Models
{
    public class GameCollection
    {
        [Key]
        public int GameCollectionId { get; set; }

        [ForeignKey("Genres")]
        public int GenreId { get; set; }
        public virtual Genre Genres { get; set; }


        [ForeignKey("Platforms")]
        public int PlatformId { get; set; }
        public virtual Platform Platforms { get; set; }

        [ForeignKey("Games")]
        public int GameId { get; set; }
        public virtual Game Games { get; set; }
    }
}