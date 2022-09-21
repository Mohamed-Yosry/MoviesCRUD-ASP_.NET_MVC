using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies_CRUD_V2.Models
{
    public class Movies
    {
        public int Id { get; set; }
        public int Year { get; set; }
        [Required, MaxLength(50)]
        public string Title { get; set; }
        [Required, MaxLength(2500)]
        public string StoryLine { get; set; }
        public float Rate { get; set; }
        public byte[] Poster { get; set; }
        public byte GenreId { get; set; }
        public Genres Genre { get; set; }
    }
}
