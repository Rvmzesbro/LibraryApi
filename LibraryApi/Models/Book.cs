using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryApi.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        [ForeignKey("GenreBook")]
        public int GenreId { get; set; }
        public short YearPublication { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        [JsonIgnore]
        public virtual GenreBook GenreBook { get; set; }
        [JsonIgnore]
        public virtual ICollection<HistoryRentalBook> HistoryRentalBooks { get; set; } = new List<HistoryRentalBook>();
    }
}
