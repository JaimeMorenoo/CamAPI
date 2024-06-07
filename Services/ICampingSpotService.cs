using Camp.Models;

namespace Camp.Services
{
    public interface ICampingSpotService
    {
        Task<IEnumerable<Spot>> GetOwnerCampingSpotsAsync(int ownerId);
    }
}
