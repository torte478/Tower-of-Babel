using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using ToB.PriorityToDo.Objectives;

namespace ToB.WebApi.Controllers.PriorityToDo
{
    [Route("api/priorityToDo/[controller]")]
    [ApiController]
    public sealed class ObjectivesController : ControllerBase
    {
        private readonly IService service;

        public ObjectivesController(IService service)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult<List<ObjectiveDto>> List(int projectId)
        {
            return new(GetAllProjectItems(projectId));
        }

        [HttpPost]
        [Route("startAdd")]
        public ActionResult<int> StartAdd(int projectId)
        {
            return service.StartAdd(projectId);
        }

        [HttpPost]
        [Route("continueAdd")]
        public ActionResult<AddDto> ContinueAdd(int operationId, string text, bool greater = false)
        {
            var (added, next) =  service.ContinueAdd(operationId, text, greater);
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
            public string Next { get; set; }
        }
    }
}