using System;
using System.Collections.Generic;

namespace TriviaGameRestEFAPI.Data.Entities
{
    public partial class Genre
    {
        public Genre()
        {
            Game = new HashSet<Game>();
            Question = new HashSet<Question>();
        }

        public Guid GenreId { get; set; }
        public string GenreName { get; set; }

        public virtual ICollection<Game> Game { get; set; }
        public virtual ICollection<Question> Question { get; set; }
    }
}
