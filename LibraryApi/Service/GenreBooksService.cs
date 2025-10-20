using LibraryApi.Connection;
using LibraryApi.Interfaces;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Service
{
    public class GenreBooksService : IGenreBooksService
    {
        private readonly ContextDB _contextdb;
        public GenreBooksService(ContextDB contextDB)
        {
            _contextdb = contextDB;
        }

        public async Task<ActionResult> GetAllGenreBooks()
        {
            var GenreBooks = await _contextdb.GenreBooks.ToListAsync();
            if(GenreBooks == null || GenreBooks.Count == 0)
            {
                return new OkObjectResult(new
                {
                    status = false,
                    message = "Жанров нет"
                });
            }

            return new OkObjectResult(new
            {
                status = true,
                list = GenreBooks
            });
        }

        public async Task<ActionResult> CreateNewGenre(string Name)
        {
            var genre = await _contextdb.GenreBooks.FirstOrDefaultAsync(p => p.Name.ToLower() == Name.ToLower());
            if(genre == null)
            {
                var Genre = new GenreBook()
                {
                    Name = Name
                };
                await _contextdb.AddAsync(Genre);
                await _contextdb.SaveChangesAsync();
            }
            else
            {
                return new OkObjectResult(new
                {
                    status = false
                });
            }
            

            return new OkObjectResult(new
            {
                status = true
            });
        }

        public async Task<ActionResult> UpdateGenre(int id, string Name)
        {
            var SelectedGenre = await _contextdb.GenreBooks.FirstOrDefaultAsync(p => p.Id == id);
            if(SelectedGenre == null)
            {
                return new OkObjectResult(new
                {
                    status = false
                });
            }

            SelectedGenre.Name = Name;
            _contextdb.GenreBooks.Update(SelectedGenre);
            await _contextdb.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true
            });
        }

        public async Task<ActionResult> DeleteGenre(int id)
        {
            var DeleteGenre = await _contextdb.GenreBooks.FirstOrDefaultAsync(p => p.Id == id);
            if(DeleteGenre == null)
            {
                return new OkObjectResult(new
                {
                    status = false
                });
            }

            _contextdb.GenreBooks.Remove(DeleteGenre);
            await _contextdb.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true
            });
        }
    }
}
