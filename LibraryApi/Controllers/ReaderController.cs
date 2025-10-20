using LibraryApi.Interfaces;
using LibraryApi.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    public class ReaderController : Controller
    {
        private readonly IReaderService _readerservice;
        public ReaderController(IReaderService readerService)
        {
            _readerservice = readerService;
        }


        [HttpPost]
        [Route("/api/readers/NewReader")]
        public async Task<ActionResult> CreateNewReader(CreateNewReader NewReader)
        {
            return await _readerservice.CreateNewReader(NewReader);
        }

        [HttpGet]
        [Route("/api/readers/GetAllReaders")]
        public async Task<ActionResult> GetAllReaders()
        {
            return await _readerservice.GetAllReaders();
        }

        [HttpGet]
        [Route("/api/readers/GetReaderById/{id}")]
        public async Task<ActionResult> GetReaderById(int id)
        {
            return await _readerservice.GetReaderById(id);
        }

        [HttpPut]
        [Route("/api/readers/UpdateReader/{id}")]
        public async Task<ActionResult> UpdateReader(int id, CreateNewReader UpdateReader)
        {
            return await _readerservice.UpdateReader(id, UpdateReader);
        }

        [HttpDelete]
        [Route("/api/readers/DeleteReader/{id}")]
        public async Task<ActionResult> DeleteReader(int id)
        {
            return await _readerservice.DeleteReader(id);
        }

        [HttpGet]
        [Route("/api/readers/GetBooksReader/{id}")]
        public async Task<ActionResult> GetBooksReader(int id)
        {
            return await _readerservice.GetBooksReader(id);
        }
    }
}
