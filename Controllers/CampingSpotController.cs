using Camp.Data;
using Camp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Camp.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CampingSpotController : ControllerBase
    {
        private readonly AnonUserDB _context;

        public CampingSpotController(AnonUserDB context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Spot>>> GetCampingSpots(
           [FromQuery] string location = null,
           [FromQuery] double? minPricePerNight = null,
           [FromQuery] double? maxPricePerNight = null,
           [FromQuery] int? ownerId = null)
        {
            var query = _context.campingSpots.AsQueryable();

            if (!string.IsNullOrEmpty(location)) 
            {
                query = query.Where(s => s.Location.Contains(location));
            }

            if (minPricePerNight.HasValue)
            {
                query = query.Where(s => s.PricePerNight >= minPricePerNight.Value);
            }

            if (maxPricePerNight.HasValue)
            {
                query = query.Where(s => s.PricePerNight <= maxPricePerNight.Value);
            }

            if (ownerId.HasValue)
            {
                query = query.Where(s => s.OwnerId == ownerId.Value);
            }

            return await query.ToListAsync();
        }
    }
}
