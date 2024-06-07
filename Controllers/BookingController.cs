using Camp.Services;
using Microsoft.AspNetCore.Mvc;

namespace Camp.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<IActionResult> BookCampingSpot(int userId, int campingSpotId, DateTime checkInDate, DateTime checkOutDate)
        {
            var success = await _bookingService.BookCampingSpotAsync(userId, campingSpotId, checkInDate, checkOutDate);
            if (success)
            {
                return Ok("Booking successful.");
            }
            else
            {
                return BadRequest("Booking failed.");
            }
        }

        [HttpDelete("bookings/{bookingId}")]
        public async Task<IActionResult> CancelBooking(int bookingId)
        {
            try
            {
              
                bool cancellationResult = await _bookingService.CancelBookingAsync(bookingId);

                if (cancellationResult)
                {
                    
                    return Ok("Booking cancelled successfully.");
                }
                else
                {
                    
                    return BadRequest("Unable to cancel booking. Please try again later.");
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("bookings/user/{userId}")]
        public async Task<IActionResult> GetUserBookings(int userId)
        {
            try
            {
             
                var userBookings = await _bookingService.GetUserBookingsAsync(userId);

                return Ok(userBookings);
            }
            catch (Exception ex)
            {
         
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
