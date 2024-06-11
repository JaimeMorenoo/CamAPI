using Camp.Data;
using Camp.Models;
using Microsoft.EntityFrameworkCore;

namespace Camp.Services
{
    public class BookingService : IBookingService
    {
        private readonly AnonUserDB _anonUserDB;

        public BookingService(AnonUserDB AnonUserDB)
        {
            _anonUserDB = AnonUserDB;
        }

        public async Task<bool> UpdateSpot(int spotId, Spot updatedSpot)
        {
            try
            {
                var spot = await _anonUserDB.campingSpots.FindAsync(spotId);

                if (spot == null)
                {
                    return false; // Spot not found
                }

                // Update the properties of the spot
                spot.Name = updatedSpot.Name;
                spot.Description = updatedSpot.Description;
                spot.Location = updatedSpot.Location;
                spot.PricePerNight = updatedSpot.PricePerNight;

                // Save changes to the database
                await _anonUserDB.SaveChangesAsync();

                return true; // Spot updated successfully
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
                Console.WriteLine($"Error updating spot: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> BookCampingSpotAsync(int userId, int campingSpotId, DateTime checkInDate, DateTime checkOutDate)
        {
            try
            {
                var booking = new Booking
                {
                    UserId = userId,
                    CampingSpotId = campingSpotId,
                    BookingDate = DateTime.Now,
                    CheckInDate = checkInDate,
                    CheckOutDate = checkOutDate
                };

                _anonUserDB.bookings.Add(booking);
                await _anonUserDB.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                
                return false;
            }
        }

        public async Task<bool> CancelBookingAsync(int bookingId)
        {
            try
            {
                var booking = await _anonUserDB.bookings.FindAsync(bookingId);
                if (booking != null)
                {
                    _anonUserDB.bookings.Remove(booking);
                    await _anonUserDB.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                
                return false;
            }
        }

        public async Task<List<Booking>> GetUserBookingsAsync(int userId)
        {
            try
            {
                var bookings = await _anonUserDB.bookings
                    .Where(b => b.UserId == userId)
                    .ToListAsync();

                return bookings;
            }
            catch (Exception ex)
            {
                // Manejar errores aquí
                return null;
            }
        }



    }
}
