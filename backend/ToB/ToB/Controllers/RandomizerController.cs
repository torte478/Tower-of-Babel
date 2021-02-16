using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using ToB.Interfaces;
using ToB.DB;

namespace ToB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class RandomizerController : ControllerBase
    {
        private readonly IRandomizer random;
        private readonly RandomizerContext context;

        public RandomizerController(IRandomizer random, RandomizerContext context)
        {
            this.random = random;
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Registry>> Get()
        {
            return context.Registries.ToArray();
        }
    }
}
