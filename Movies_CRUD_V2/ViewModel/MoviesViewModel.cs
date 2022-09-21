using Movies_CRUD_V2.Models;
using System.ComponentModel.DataAnnotations;

namespace Movies_CRUD_V2.ViewModel
{
    public class MoviesViewModel
    {
        public int Id { get; set; }
        public int Year { get; set; }
        [Required, StringLength(50)]
        public string Title { get; set; }
        [Required, StringLength(2500)]
        public string StoryLine { get; set; }
        [Range(0,10)]
        public float Rate { get; set; }
        public byte[] Poster { get; set; }
        [Display(Name = "Genre")]
        public byte GenreId { get; set; }
        public IEnumerable<Genres> Geners { get; set; }
    }
}
