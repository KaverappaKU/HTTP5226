using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Games_Catalog_N01589651.Models
{
    public class Developer
    {
        public int DeveloperId { get; set; }
        public string DeveloperStudioName { get; set; }
 

        //A Developer can have many games
        public ICollection<Game> Games { get; set; }

    }
     public class DeveloperDto
    {
        public int DeveloperId { get; set; }
        public string DeveloperStudioName { get; set; }
    }
}