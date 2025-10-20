using LibraryApi.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Interfaces
{
    public interface IReaderService
    {
        Task<ActionResult> CreateNewReader(CreateNewReader NewReader);
        Task<ActionResult> GetAllReaders();
        Task<ActionResult> GetReaderById(int id);
        Task<ActionResult> UpdateReader(int id, CreateNewReader UpdateReader);
        Task<ActionResult> DeleteReader(int id);
        Task<ActionResult> GetBooksReader(int id);
    }
}
