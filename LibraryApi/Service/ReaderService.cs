using LibraryApi.Connection;
using LibraryApi.Interfaces;
using LibraryApi.Models;
using LibraryApi.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Service
{
    public class ReaderService : IReaderService
    {
        private readonly ContextDB _contextdb;
        public ReaderService(ContextDB contextDB)
        {
            _contextdb = contextDB;
        }

        public async Task<ActionResult> CreateNewReader(CreateNewReader NewReader)
        {
            var newreader = new Reader()
            {
                Name = NewReader.Name,
                Surname = NewReader.Surname,
                BirthDay = NewReader.BirthDay,
                Phone = NewReader.Phone,
                Password = NewReader.Password
            };
            await _contextdb.AddAsync(newreader);
            await _contextdb.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true
            });
        }

        public async Task<ActionResult> GetAllReaders()
        {
            var ListReaders = await _contextdb.Readers.ToListAsync();
            if(ListReaders == null || ListReaders.Count == 0)
            {
                return new OkObjectResult(new
                {
                    status = false
                });
            }
            return new OkObjectResult(new
            {
                status = true,
                list = ListReaders
            });
        }

        public async Task<ActionResult> GetReaderById(int id)
        {
            var reader = await _contextdb.Readers.FirstOrDefaultAsync(p => p.Id == id);
            if(reader == null)
            {
                return new OkObjectResult(new
                {
                    status = false,
                    message = "Такого читателя нет"
                });
            }

            return new OkObjectResult(new
            {
                status = true,
                reader = reader
            });
        }

        public async Task<ActionResult> UpdateReader(int id, CreateNewReader UpdateReader)
        {
            var updatereader = await _contextdb.Readers.FirstOrDefaultAsync(p => p.Id == id);
            if(updatereader == null)
            {
                return new OkObjectResult(new
                {
                    status = false,
                    message = "Такого читателя нет"
                });
            }

            updatereader.Name = UpdateReader.Name;
            updatereader.Surname = UpdateReader.Surname;
            updatereader.BirthDay = UpdateReader.BirthDay;
            updatereader.Phone = UpdateReader.Phone;
            updatereader.Password = UpdateReader.Password;

            _contextdb.Update(updatereader);
            await _contextdb.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true
            });
        }

        public async Task<ActionResult> DeleteReader(int id)
        {
            var DeleteReader = await _contextdb.Readers.FirstOrDefaultAsync(p => p.Id == id);
            if (DeleteReader == null)
            {
                return new OkObjectResult(new
                {
                    status = false,
                    message = "Такого читателя нет"
                });
            }

            _contextdb.Remove(DeleteReader);
            await _contextdb.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true
            });
        }

        public async Task<ActionResult> GetBooksReader(int id)
        {
            var ListBooksReader = await _contextdb.HistoryRentalBooks.Where(p => p.UserId == id).ToListAsync();
            if(ListBooksReader == null || ListBooksReader.Count == 0)
            {
                return new OkObjectResult(new
                {
                    status = false
                });
            }

            return new OkObjectResult(new
            {
                status = true,
                list = ListBooksReader
            });
        }
    }
}
