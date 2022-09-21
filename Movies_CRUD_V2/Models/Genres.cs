using System.ComponentModel.DataAnnotations.Schema;

namespace Movies_CRUD_V2.Models
{
    public class Genres
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }
        public string Name { get; set; }
    }
}
