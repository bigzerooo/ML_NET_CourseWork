using Microsoft.AspNetCore.Mvc;
using Trips.ML.Interfaces;

namespace Trips.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearningController : ControllerBase
    {
        private readonly ILearningService _learningService;

        public LearningController(ILearningService learningService)
        {
            _learningService = learningService;
        }

        [HttpPost("[action]")]
        public IActionResult Learn()
        {
            _learningService.Learn();

            return Ok();
        }
    }
}
