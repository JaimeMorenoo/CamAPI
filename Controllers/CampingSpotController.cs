using Camp.Data;
using Camp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Camp.Services;

namespace Camp.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CampingSpotController : ControllerBase
    {
        private readonly AnonUserDB _context;
        private readonly ICampingSpotService _campingSpotService;

        public CampingSpotController(AnonUserDB context, ICampingSpotService campingSpotService )
        {
            _context = context;
            _campingSpotService = campingSpotService;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSpot(int id, [FromBody] Spot updatedSpot)
        {
            if (id != updatedSpot.Id)
            {
                return BadRequest("Spot ID in the request body does not match the route parameter");
            }

            bool spotUpdated = await _campingSpotService.UpdateSpot(id, updatedSpot);

            if (!spotUpdated)
            {
                return NotFound("Spot not found");
            }

            return Ok("Spot updated successfully");
        }

        [HttpPost]
        public async Task<ActionResult<Spot>> CreateSpot(Spot spot)
        {
            try
            {
                var createdSpot = await _campingSpotService.CreateSpotAsync(spot);
                return Ok(createdSpot);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpDelete("CampingSpot/{spotId}")]
        public async Task<IActionResult> CancelBooking(int spotId)
        {
            try
            {

                bool cancellationResult = await _campingSpotService.CancelSpotAsync(spotId);

                if (cancellationResult)
                {

                    return Ok("Spot Deleted successfully.");
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

        [HttpGet("owner/{ownerId}")]
        public async Task<IActionResult> GetOwnerCampingSpots(int ownerId)
        {
            try
            {
                // Call the service method to fetch owner camping spots
                var ownerCampingSpots = await _campingSpotService.GetOwnerCampingSpotsAsync(ownerId);

                // Return the list of owner camping spots as the response
                return Ok(ownerCampingSpots);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
            }
        }
