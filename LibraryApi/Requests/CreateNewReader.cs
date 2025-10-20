using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Requests
{
    public class CreateNewReader
    {
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateOnly BirthDay { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
