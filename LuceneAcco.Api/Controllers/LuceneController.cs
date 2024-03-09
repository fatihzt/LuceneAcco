using LuceneAcco.Business.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LuceneAcco.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LuceneController : ControllerBase
    {
        private readonly ILuceneSearchEngine searchEngine;
        public LuceneController(ILuceneSearchEngine searchEngine)
        {
            this.searchEngine = searchEngine;
        }
        [HttpGet("FindAccommodations/{accoCode}")]
        public async Task<IActionResult> FindAccommotaions(string accoCode)
        {
            var result = searchEngine.FindAccommodation(accoCode);
            return Ok(result);
        }
    }
}
