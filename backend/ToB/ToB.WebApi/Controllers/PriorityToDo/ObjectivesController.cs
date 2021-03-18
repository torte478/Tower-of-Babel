using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ToB.PriorityToDo.Contracts;
using ToB.PriorityToDo.Objectives;

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
        public ActionResult<int?> FindRoot(int projectId)
        {
            return service.FindRoot(projectId);
        }

        [HttpPost]
        [Route("add")]
        public ActionResult<AddDto> TryAdd(int projectId, int target, bool greater, string text)
        {
            var (added, next) =  service.TryAdd(projectId, target, greater, text);
            return new AddDto
            {
                Added = added,
                Next = next
            };
        }

        [HttpDelete]
        public ActionResult<List<ObjectiveDto>> Delete(int projectId, int id)
        {
            service.Remove(projectId, id);

            return new ActionResult<List<ObjectiveDto>>(GetAllProjectItems(projectId));
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

        public sealed class AddDto
        {
            public bool Added { get; set; }
            public int Next { get; set; }
        }
    }
}