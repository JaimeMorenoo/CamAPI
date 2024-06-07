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
