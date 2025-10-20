using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryApi.Models
{
    public class GenreBook
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
