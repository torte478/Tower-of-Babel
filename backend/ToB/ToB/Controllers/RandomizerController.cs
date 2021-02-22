using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using ToB.Interfaces;
using ToB.DTO;

namespace ToB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class RandomizerController : ControllerBase
    {
        private readonly IRegistries registries;

        public RandomizerController(IRegistries registries)
        {
            this.registries = registries;
        }

        [HttpGet]
        public ActionResult<List<RegistryDto>> Get(int root)
        {
            return registries
                .ToAll(root)
                .Select(_ => new RegistryDto
                {
                    Id = _.Id,
                    Parent = _.Parent,
                    Label = _.Label
                })
                .ToList();
        }

        [HttpGet]
        [Route("getRandom")]
        public ActionResult<string> GetRandom(int root)
        {
            var path = registries
                .ToRandom(root)
                .Select(_ => _.Label);
            
            return string.Join(Environment.NewLine, path);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            registries.Delete(id);
        }

        [HttpPost]
        public void Post([FromBody] RegistryDto registry)
        {
            registries.Add(registry.Parent, registry.Label);
        }
    }
}
