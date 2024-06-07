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
                // Call the service method to cancel the booking
                bool cancellationResult = await _bookingService.CancelBookingAsync(bookingId);

                if (cancellationResult)
                {
                    // Return success response if the booking was successfully cancelled
                    return Ok("Booking cancelled successfully.");
                }
                else
                {
                    // Return error response if the booking could not be cancelled
                    return BadRequest("Unable to cancel booking. Please try again later.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("bookings/user/{userId}")]
        public async Task<IActionResult> GetUserBookings(int userId)
        {
            try
            {
                // Call the service method to fetch user bookings
                var userBookings = await _bookingService.GetUserBookingsAsync(userId);

                // Return the list of user bookings as the response
                return Ok(userBookings);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
