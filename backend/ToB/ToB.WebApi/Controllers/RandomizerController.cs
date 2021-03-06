﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using ToB.WebApi.DTO;
using ToB.WebApi.Interfaces;

namespace ToB.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class RandomizerController : ControllerBase
    {
        private readonly IRandomRegistries registries;

        public RandomizerController(IRandomRegistries registries)
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
