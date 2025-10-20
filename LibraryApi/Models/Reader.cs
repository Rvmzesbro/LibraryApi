using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryApi.Models
{
    public class Reader
    {
        [Key]
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public DateOnly BirthDay { get; set; }
        public string Phone {  get; set; }
        public string? Password { get; set; }
        [JsonIgnore]
        public virtual ICollection<HistoryRentalBook> HistoryRentalBooks { get; set; } = new List<HistoryRentalBook>();
    }
}
