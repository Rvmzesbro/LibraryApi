using LibraryApi.Connection;
using LibraryApi.Interfaces;
using LibraryApi.Models;
using LibraryApi.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Service
{
    public class HistoryRentalBooksService : IHistoryRentalBooksService
    {
        private readonly ContextDB _contextdb;
        public HistoryRentalBooksService(ContextDB contextDB)
        {
            _contextdb = contextDB;
        }

        public async Task<ActionResult> CreateNewRental(CreateNewRental NewRental, DateTime DateOfDelivery)
        {
            // Хрень чтобы перевести время в допустимую для добавления в бд. Крч делаем дату к концу дня и приводим к UTC.
            var dateDeliveryUtc = new DateTime(
                DateOfDelivery.Year,
                DateOfDelivery.Month,
                DateOfDelivery.Day,
                23, 59, 59, DateTimeKind.Local
            ).ToUniversalTime();

            var _NewRental = new HistoryRentalBook()
            {
                UserId = NewRental.UserId,
                BookId = NewRental.BookId,
                CountBook = NewRental.CountBook,
                DateOfIssue = DateTime.UtcNow,
                DateDelivery = dateDeliveryUtc
            };

            if(DateOfDelivery.Date < DateTime.UtcNow.Date || DateOfDelivery.Date > DateTime.UtcNow.Date.AddDays(30))
            {
                return new OkObjectResult(new
                {
                    status = false,
                    message = "Введите дату правильно"
                });
            }

            var RentalBook = await _contextdb.Books.FirstOrDefaultAsync(p => p.Id == NewRental.BookId);
            var RentalReader = await _contextdb.Readers.FirstOrDefaultAsync(p => p.Id == NewRental.UserId);
            if (RentalBook == null)
            {
                return new OkObjectResult(new
                {
                    status = false,
                    message = "Книга не найдена"
                });
            }
            if (RentalReader == null)
            {
                return new OkObjectResult(new
                {
                    status = false,
                    message = "Читатель не найден"
                });
            }

            if (RentalBook.Count < NewRental.CountBook)
            {
                return new OkObjectResult(new
                {
                    status = false,
                    message = $"Недостаточно книг. Доступно: {RentalBook.Count}"
                });
            }

            RentalBook.Count -= NewRental.CountBook;
            _contextdb.Books.Update(RentalBook);

            await _contextdb.HistoryRentalBooks.AddAsync(_NewRental);
            await _contextdb.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true
            });
        }

        public async Task<ActionResult> ReturnBook(CreateNewRental NewRental)
        {
            var ExistingRental = await _contextdb.HistoryRentalBooks.FirstOrDefaultAsync(p => p.UserId == NewRental.UserId && p.BookId == NewRental.BookId);
            if(ExistingRental == null)
            {
                return new OkObjectResult(new
                {
                    status = false,
                    message = "У вас нет аренд"
                });
            }

            var RentalBook = await _contextdb.Books.FirstOrDefaultAsync(p => p.Id == NewRental.BookId);

            if (RentalBook == null)
            {
                return new OkObjectResult(new
                {
                    status = false,
                    message = "Книга не найдена"
                });
            }

            if(NewRental.CountBook > ExistingRental.CountBook)
            {
                return new OkObjectResult(new
                {
                    status = false,
                    message = $"У вас нет столько книг. Вы арендовали: {ExistingRental.CountBook}"
                });
            }


            RentalBook.Count += NewRental.CountBook;
            _contextdb.Update(RentalBook);

            ExistingRental.CountBook -= NewRental.CountBook;
            ExistingRental.DateDelivery = DateTime.UtcNow;

            if (ExistingRental.CountBook == 0)
            {
                _contextdb.HistoryRentalBooks.Remove(ExistingRental);
            }
            else
            {
                _contextdb.Update(ExistingRental);
            }

            await _contextdb.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true
            });
        }

        public async Task<ActionResult> GetHistoryRentalReader(int id)
        {
            var ListRental = await _contextdb.HistoryRentalBooks.Where(p => p.UserId == id).ToListAsync();
            if(ListRental.Count == 0 || ListRental == null)
            {
                return new OkObjectResult(new
                {
                    status = false,
                    message = "У вас нет аренд"
                });
            }

            return new OkObjectResult(new
            {
                status = true,
                list = ListRental
            });
        }

        public async Task<ActionResult> GetCurrentRental()
        {
            var ListRental = await _contextdb.HistoryRentalBooks.Include(p => p.Reader).Select(p => new
            {
                p.UserId,
                p.Reader.Name,
                DeadLine = (p.DateDelivery - p.DateOfIssue).Value.Days == 0 ? "Нужно вернуть сегодня"
                                                                            : $"Осталось дней: {(p.DateDelivery - p.DateOfIssue).Value.Days}"
        }).ToListAsync();

            if(ListRental.Count == 0 || ListRental == null)
            {
                return new OkObjectResult(new
                {
                    status = false,
                    message = "Аренд нет"
                });
            }

            return new OkObjectResult(new
            {
                status = true,
                list = ListRental
            });
        }

        public async Task<ActionResult> GetRentalByBook(int id)
        {
            var ListRental = await _contextdb.HistoryRentalBooks.Where(p => p.BookId == id).ToListAsync();
            if(ListRental == null || ListRental.Count == 0)
            {
                return new OkObjectResult(new
                {
                    status = false,
                    message = "Эта книга не в аренде"
                });
            }
            return new OkObjectResult(new
            {
                status = true,
                list = ListRental
            });
        }
    }
}
