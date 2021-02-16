using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using ToB.Extensions;
using ToB.Interfaces;
using ToB.DB;
using ToB.DTO;

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
        public ActionResult<RegistriesDto> Get(int page, int start, int limit)
        {
            return context
                   .Registries
                   .Select(_ => new RegistryDto()
                   {
                       Id = _.Id,
                       Parent = _.Parent,
                       Label = _.Label
                   })
                   .ToArray()
                   ._(_ => new RegistriesDto()
                   {
                       Success = true,
                       Data = _
                   });
        }
    }
}
