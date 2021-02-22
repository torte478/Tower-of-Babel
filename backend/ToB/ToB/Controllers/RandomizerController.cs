using System;
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
        public ActionResult<List<RegistryDto>> Get(int root)
        {
            return context
                .Registries
                .Where(_ => _.Parent == root)
                .Select(_ => new RegistryDto()
                {
                    Id = _.Id,
                    Parent = _.Parent,
                    Label = _.Label
                })
                .ToList();
        }

        [HttpDelete]
        public void Delete(int id)
        {
            var item = context.Registries.FirstOrDefault(_ => _.Id == id);
            
            if (item != null)
            {
                context.Registries.Remove(item);
                context.SaveChanges();
            }
        }

        [HttpPost]
        public void Post([FromBody] RegistryDto registry)
        {
            context.Registries.Add(new Registry()
            {
                Label = registry.Label,
                Parent = registry.Parent
            });
            context.SaveChanges();
        }
    }
}
