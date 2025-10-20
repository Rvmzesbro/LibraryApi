using LibraryApi.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Interfaces
{
    public interface IBookService
    {
        Task<ActionResult> GetAllBooks();
        Task<ActionResult> GetBookById(int id);
        Task<ActionResult> CreateNewBook(CreateNewBook NewBook);
        Task<ActionResult> UpdateBook(int id, CreateNewBook UpdateBook);
        Task<ActionResult> DeleteBook(int id);
        Task<ActionResult> GetBooksByGenre(string GenreName);
        Task<ActionResult> SearchBook(string TextUser);
        Task<ActionResult> GetAvailableBooks(int id);
    }
}
