using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryApi.Models
{
    public class HistoryRentalBook
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Reader")]
        public int UserId {  get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public int CountBook { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime? DateDelivery { get; set; }
        [JsonIgnore]
        public virtual Reader Reader { get; set; }
        [JsonIgnore]
        public virtual Book Book { get; set; }
    }
}
