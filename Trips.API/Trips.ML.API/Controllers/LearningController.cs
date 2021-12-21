using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trips.ML.API.Interfaces.Services;

namespace Trips.ML.API.Controllers
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
        public IActionResult PrepareData()
        {
            _learningService.PrepareData();

            return Ok();
        }

        [HttpPost("[action]")]
        public IActionResult Learn()
        {
            _learningService.Learn();

            return Ok();
        }
    }
}
