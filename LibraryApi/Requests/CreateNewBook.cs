using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Requests
{
    public class CreateNewBook
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string GenreName { get; set; }
        [Required]
        public short YearPublication { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Count { get; set; }
    }
}
