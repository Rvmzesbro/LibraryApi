using LibraryApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    public class GenreBooksController : Controller
    {
        private readonly IGenreBooksService _genrebooksservice;
        public GenreBooksController(IGenreBooksService genreBooksService)
        {
            _genrebooksservice = genreBooksService;
        }

        [HttpGet]
        [Route("/api/genres/GetAllGenreBooks")]
        public async Task<ActionResult> GetAllGenreBooks()
        {
            return await _genrebooksservice.GetAllGenreBooks();
        }

        [HttpPost]
        [Route("/api/genres/CreateNewGenre/{Name}")]
        public async Task<ActionResult> CreateNewGenre(string Name)
        {
            return await _genrebooksservice.CreateNewGenre(Name);
        }

        [HttpPut]
        [Route("/api/genres/UpdateGenre/{id}/{Name}")]
        public async Task<ActionResult> UpdateGenre(int id, string Name)
        {
            return await _genrebooksservice.UpdateGenre(id, Name);
        }

        [HttpDelete]
        [Route("/api/genres/DeleteGenre/{id}")]
        public async Task<ActionResult> DeleteGenre(int id)
        {
            return await _genrebooksservice.DeleteGenre(id);
        }
    }
}
