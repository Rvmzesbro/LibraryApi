using LibraryApi.Interfaces;
using LibraryApi.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookservice;
        public BookController(IBookService bookService)
        {
            _bookservice = bookService;
        }

        [HttpGet]
        [Route("/api/books/GetBooks")]
        public async Task<ActionResult> GetAllBooks()
        {
            return await _bookservice.GetAllBooks();
        }

        [HttpGet]
        [Route("/api/books/GetBooksById/{id}")]
        public async Task<ActionResult> GetBooksById(int id)
        {
            return await _bookservice.GetBookById(id);
        }

        [HttpPost]
        [Route("/api/books/NewBook")]
        public async Task<ActionResult> CreateNewBook(CreateNewBook NewBook)
        {
            return await _bookservice.CreateNewBook(NewBook);
        }

        [HttpPut]
        [Route("/api/books/UpdateBook/{id}")]
        public async Task<ActionResult> UpdateBook(int id, CreateNewBook UpdateBook)
        {
            return await _bookservice.UpdateBook(id, UpdateBook);
        }

        [HttpDelete]
        [Route("/api/books/DeleteBook/{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            return await _bookservice.DeleteBook(id);
        }

        [HttpGet]
        [Route("/api/books/GetBooksByGenre/{GenreName}")]
        public async Task<ActionResult> GetBooksByGenre(string GenreName)
        {
            return await _bookservice.GetBooksByGenre(GenreName);
        }

        [HttpGet]
        [Route("/api/books/SearchBook/{TextUser}")]
        public async Task<ActionResult> SearchBook(string TextUser)
        {
            return await _bookservice.SearchBook(TextUser);
        }

        [HttpGet]
        [Route("/api/books/GetAvailableBooks/{id}")]
        public async Task<ActionResult> GetAvailableBooks(int id)
        {
            return await _bookservice.GetAvailableBooks(id);
        }
    }
}
