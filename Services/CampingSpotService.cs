using Camp.Data;
using Camp.Models;
using Microsoft.EntityFrameworkCore;

namespace Camp.Services
{
    public class CampingSpotService : ICampingSpotService
    {
        private readonly AnonUserDB _anonUserDB;

        public CampingSpotService(AnonUserDB anonUserDB)
        {
            _anonUserDB = anonUserDB;
        }

        public async Task<Spot> CreateSpotAsync(Spot spot)
        {
            try
            {
                // Add the new spot to the database
                _anonUserDB.campingSpots.Add(spot);
                await _anonUserDB.SaveChangesAsync();
                return spot; // Return the created spot
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw new Exception("Error creating spot", ex);
            }
        }

        public async Task<bool> CancelSpotAsync(int spotId)
        {
            try
            {
                var spot = await _anonUserDB.campingSpots.FindAsync(spotId);
                if (spot != null)
                {
                    _anonUserDB.campingSpots.Remove(spot);
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

        public async Task<IEnumerable<Spot>> GetOwnerCampingSpotsAsync(int ownerId)
        {
            try
            {
                // Retrieve the camping spots owned by the specified owner ID
                var ownerCampingSpots = await _anonUserDB.campingSpots
                    .Where(s => s.OwnerId == ownerId)
                    .ToListAsync();

                return ownerCampingSpots;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                throw new Exception("An error occurred while retrieving owner camping spots.", ex);
            }
        }
    }
}
