using LibraryApi.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Interfaces
{
    public interface IHistoryRentalBooksService
    {
        Task<ActionResult> CreateNewRental(CreateNewRental NewRental, DateTime DateOfDelivery);
        Task<ActionResult> ReturnBook(CreateNewRental NewRental);
        Task<ActionResult> GetHistoryRentalReader(int id);
        Task<ActionResult> GetCurrentRental();
        Task<ActionResult> GetRentalByBook(int id);
    }
}
