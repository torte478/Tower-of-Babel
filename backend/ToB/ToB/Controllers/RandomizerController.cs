using Microsoft.AspNetCore.Mvc;

using ToB.Interfaces;

namespace ToB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class RandomizerController : ControllerBase
    {
        private readonly IRandomizer random;

        public RandomizerController(IRandomizer random)
        {
            this.random = random;
        }

        [HttpGet]
        public ActionResult<int> Get(int min, int max)
        {
            //TODO : validation

            return random.ToRandom(min, max);
        }
    }
}
