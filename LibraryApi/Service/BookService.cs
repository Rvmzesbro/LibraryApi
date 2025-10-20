using LibraryApi.Connection;
using LibraryApi.Interfaces;
using LibraryApi.Models;
using LibraryApi.Requests;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Service
{
    public class BookService : IBookService
    {
        private readonly ContextDB _contextdb;
        public BookService(ContextDB contextdb)
        {
            _contextdb = contextdb;
        }

        public async Task<ActionResult> GetAllBooks()
        {
            var ListBooks = await _contextdb.Books.ToListAsync();

            return new OkObjectResult(new
            {
                status = true,
                list = ListBooks
            });
        }

        public async Task<ActionResult> GetBookById(int id)
        {
            var book = await _contextdb.Books.FirstOrDefaultAsync(p => p.Id == id);

            if(book == null)
            {
                return new OkObjectResult(new
                {
                    status = false,
                    message = "Такой книги нету"
                });
            }

            return new OkObjectResult(new
            {
                status = true,
                book = book
            });
        }
        public async Task<ActionResult> CreateNewBook(CreateNewBook NewBook)
        {
            var genre = await _contextdb.GenreBooks.FirstOrDefaultAsync(p => p.Name.ToLower() == NewBook.GenreName.ToLower());
            if (genre == null)
            {
                return new OkObjectResult(new
                {
                    status = false,
                    message = "Такого жанра нет"
                });
            }

            var book = new Book()
            {
                Name = NewBook.Name,
                Author = NewBook.Author,
                GenreId = genre.Id,
                YearPublication = NewBook.YearPublication,
                Description = NewBook.Description,
                Count = NewBook.Count,
            };

            await _contextdb.AddAsync(book);
            await _contextdb.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true
            });
        }

        public async Task<ActionResult> UpdateBook(int id, CreateNewBook UpdateBook)
        {
            var updatebook = await _contextdb.Books.FirstOrDefaultAsync(p => p.Id == id);
            if(updatebook == null)
            {
                return new OkObjectResult(new
                {
                    status = false,
                    message = "Такой книги нету"
                });
            }

            var genre = await _contextdb.GenreBooks.FirstOrDefaultAsync(p => p.Name.ToLower() == UpdateBook.GenreName.ToLower());
            if (genre == null)
            {
                return new OkObjectResult(new
                {
                    status = false,
                    message = "Такого жанра нет"
                });
            }

            updatebook.Name = UpdateBook.Name;
            updatebook.Author = UpdateBook.Author;
            updatebook.GenreId = genre.Id;
            updatebook.YearPublication = UpdateBook.YearPublication;
            updatebook.Description = UpdateBook.Description;
            updatebook.Count = UpdateBook.Count;

            _contextdb.Update(updatebook);
            await _contextdb.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true
            });
        }

        public async Task<ActionResult> DeleteBook(int id)
        {
            var deletebook = await _contextdb.Books.FirstOrDefaultAsync(p => p.Id == id);
            if (deletebook == null)
            {
                return new OkObjectResult(new
                {
                    status = false,
                    message = "Такой книги нету"
                });
            }
            _contextdb.Remove(deletebook);
            await _contextdb.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true
            });
        }

        public async Task<ActionResult> GetBooksByGenre(string GenreName)
        {
            var SelectedGenre = await _contextdb.GenreBooks.FirstOrDefaultAsync(p => p.Name.ToLower() == GenreName.ToLower());
            if (SelectedGenre == null)
            {
                return new OkObjectResult(new
                {
                    status = false
                });
            }

            var ListBooksByGenre = await _contextdb.Books.Where(p => p.GenreId == SelectedGenre.Id).ToListAsync();
            if(ListBooksByGenre == null)
            {
                return new OkObjectResult(new
                {
                    status = false
                });
            }

            return new OkObjectResult(new
            {
                status = true,
                list = ListBooksByGenre
            });
        }

        public async Task<ActionResult> SearchBook(string TextUser)
        {
            var ListBooks = await _contextdb.Books.Where(p => p.Author.ToLower() == TextUser.ToLower() || p.Name.ToLower() == TextUser.ToLower()).ToListAsync();
            if(ListBooks == null || ListBooks.Count == 0)
            {
                return new OkObjectResult(new
                {
                    status = false
                });
            }

            return new OkObjectResult(new
            {
                status = true,
                list = ListBooks
            });
        }

        public async Task<ActionResult> GetAvailableBooks(int id)
        {
            var book = await _contextdb.Books.FirstOrDefaultAsync(p => p.Id == id);
            if(book == null)
            {
                return new OkObjectResult(new
                {
                    status = false
                });
            }



            return new OkObjectResult(new
            {
                status = true,
                NameBook = book.Name,
                CountBook = book.Count
            });
        }
    }
}
