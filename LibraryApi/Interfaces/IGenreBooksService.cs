using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Interfaces
{
    public interface IGenreBooksService
    {
        Task<ActionResult> GetAllGenreBooks();
        Task<ActionResult> CreateNewGenre(string Name);
        Task<ActionResult> UpdateGenre(int id, string Name);
        Task<ActionResult> DeleteGenre(int id);
    }
}
