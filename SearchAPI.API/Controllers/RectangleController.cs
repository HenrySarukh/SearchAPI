using SearchAPI.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;

namespace SearchAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RectangleController : ControllerBase
    {
        private readonly IRectangleService rectangleService;

        public RectangleController(IRectangleService rectangleService)
        {
            this.rectangleService = rectangleService;
        }

        [HttpGet]
        public async Task<ActionResult> SearchMatch([FromQuery] List<Coordinate> coordinates)
        {
            var result = await this.rectangleService.SearchMatch(coordinates);

            return Ok(result);
        }
    }
}
