using System;
using System.Collections.Generic;

namespace TriviaGameRestEFAPI.Data.Entities
{
    public partial class Game
    {
        public Game()
        {
            Answer = new HashSet<Answer>();
        }

        public Guid GameId { get; set; }
        public Guid GenreId { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual ICollection<Answer> Answer { get; set; }
    }
}
