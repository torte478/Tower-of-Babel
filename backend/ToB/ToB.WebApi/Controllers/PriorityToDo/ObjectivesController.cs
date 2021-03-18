using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using ToB.PriorityToDo.Contracts;

namespace ToB.WebApi.Controllers.PriorityToDo
{
    [Route("api/priorityToDo/[controller]")]
    [ApiController]
    public sealed class ObjectivesController : ControllerBase
    {
        private readonly IObjectiveService service;

        public ObjectivesController(IObjectiveService service)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult<List<ObjectiveDto>> List(int projectId)
        {
            return new(GetAllProjectItems(projectId));
        }
        
        [HttpGet]
        [Route("getRoot")]
        public ActionResult<MonadDto> FindRoot(int projectId)
        {
            var id = service.FindRoot(projectId);
            return new MonadDto
            {
                Success = id.HasValue,
                Id = id ?? -1
            };
        }

        [HttpPost]
        [Route("add")]
        public ActionResult<MonadDto> TryAdd(int projectId, int target, bool greater, string text)
        {
            var (added, next) =  service.TryAdd(projectId, target, greater, text);
            return new MonadDto
            {
                Success = added,
                Id = next
            };
        }

        [HttpDelete]
        public ActionResult<bool> Delete(int projectId, int id)
        {
            return service.Remove(projectId, id);
        }

        [HttpPost]
        [Route("update")]
        public ActionResult<bool> Update(int projectId, int id, string text)
        {
            return service.Update(projectId, id, text);
        }

        [HttpPost]
        [Route("actualize")]
        public ActionResult<List<int>> Actualize(int projectId, float percentage)
        {
            throw new NotImplementedException(); //TODO
        }
        
        private List<ObjectiveDto> GetAllProjectItems(int projectId)
        {
            return service
                .ToPriorityList(projectId)
                .Select(_ => new ObjectiveDto
                {
                    Id = _.id,
                    Text = _.text
                })
                .ToList();
        }

        public sealed class ObjectiveDto
        {
            public int Id { get; set; }
            public string Text { get; set; }
        }

        public sealed class MonadDto
        {
            public bool Success { get; set; }
            public int Id { get; set; }
        }
    }
}