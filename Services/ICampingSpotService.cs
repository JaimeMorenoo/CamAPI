using Camp.Models;

namespace Camp.Services
{
    public interface ICampingSpotService
    {
        Task<IEnumerable<Spot>> GetOwnerCampingSpotsAsync(int ownerId);
        public Task<bool> UpdateSpot(int spotId, Spot updatedSpot);

        public Task<bool> CancelSpotAsync(int bookingId);

        public Task<Spot> CreateSpotAsync(Spot spot);
    }
}
