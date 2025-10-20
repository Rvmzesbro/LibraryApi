using LibraryApi.Interfaces;
using LibraryApi.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    public class HistoryRentalBooksController : Controller
    {
        private readonly IHistoryRentalBooksService _HistoryRentalBooksService;
        public HistoryRentalBooksController(IHistoryRentalBooksService historyRentalBooksService)
        {
            _HistoryRentalBooksService = historyRentalBooksService;
        }

        [HttpPost]
        [Route("/api/rentals/CreateNewRental/{DateOfDelivery}")]
        public async Task<ActionResult> CreateNewRental(CreateNewRental NewRental, DateTime DateOfDelivery)
        {
            return await _HistoryRentalBooksService.CreateNewRental(NewRental, DateOfDelivery);
        }

        [HttpPut]
        [Route("/api/rentals/ReturnBook")]
        public async Task<ActionResult> ReturnBook(CreateNewRental NewRental)
        {
            return await _HistoryRentalBooksService.ReturnBook(NewRental);
        }

        [HttpGet]
        [Route("/api/rentals/GetHistoryRentalReader/{id}")]
        public async Task<ActionResult> GetHistoryRentalReader(int id)
        {
            return await _HistoryRentalBooksService.GetHistoryRentalReader(id);
        }

        [HttpGet]
        [Route("/api/rentals/GetCurrentRental")]
        public async Task<ActionResult> GetCurrentRental()
        {
            return await _HistoryRentalBooksService.GetCurrentRental();
        }

        [HttpGet]
        [Route("/api/rentals/GetRentalByBook/{id}")]
        public async Task<ActionResult> GetRentalByBook(int id)
        {
            return await _HistoryRentalBooksService.GetRentalByBook(id);
        }
    }
}
