using Camp.Models;

namespace Camp.Services
{
    public interface IBookingService
    {
        Task<bool> BookCampingSpotAsync(int userId, int campingSpotId, DateTime checkInDate, DateTime checkOutDate);
        Task<bool> CancelBookingAsync(int bookingId);
        Task<List<Booking>> GetUserBookingsAsync(int userId);
    }
}
