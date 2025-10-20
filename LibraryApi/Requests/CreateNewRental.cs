using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApi.Requests
{
    public class CreateNewRental
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public int CountBook { get; set; }
    }
}
