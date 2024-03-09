using LuceneAcco.Business.Contracts;
using LuceneAcco.RedisCache.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LuceneAcco.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccommodationsController : ControllerBase
    {
        private readonly IAccommodationsService _service;
        private readonly IRedisCache _redisService;

        public AccommodationsController(IAccommodationsService service,IRedisCache redisCache)
        {
            _service = service;
            _redisService = redisCache;
        }
        [HttpGet("GetAllAccommodationFromDB")]
        public async Task<IActionResult> GetAllAccommodations()
        {
            var result = await _service.GetAllAccommodations();
            return Ok(result);
        }
        [HttpPost("SaveAccommodationsOnRedisDBHash")]
        public async Task<IActionResult> SaveAccosRedisHasg()
        {
            var result = _redisService.AddAccommodationsHash();
            return Ok(result);
        }
        [HttpGet("GetAllAccommodationsFromRedisDBHash")]
        public async Task<IActionResult> GetAllAccommodationsRedisDBHash()
        {
            var result = _redisService.GetAllAccommodationsHash();
            return Ok(result);
        }
    }
}
