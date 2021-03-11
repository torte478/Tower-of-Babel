using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using ToB.Common.Extensions;
using ToB.PriorityToDo;
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
        public ActionResult<AddDto> StartAdd(int projectId, string text)
        {
            var result = service
                .StartAdd(projectId, text)
                ._(_ => new AddDto
                {
                    Added = _.added,
                    Id = _.id
                });

            return FinishOrNext(projectId, result);
        }

        [HttpPost]
        [Route("continueAdd")]
        public ActionResult<AddDto> ContinueAdd(int projectId, int id, bool greater)
        {
            var result = service
                .ContinueAdd(id, greater)
                ._(_ => new AddDto
                {
                    Added = _.added,
                    Id = id
                });

            return FinishOrNext(projectId, result);
        }

        [HttpDelete]
        public ActionResult<List<ObjectiveDto>> Complete(int projectId, int id)
        {
            service.Remove(projectId, id);

            return new ActionResult<List<ObjectiveDto>>(GetAllProjectItems(projectId));
        }

        [HttpPost]
        [Route("update")]
        public ActionResult<bool> Change(int projectId, int id, string text)
        {
            return service.Update(projectId, id, text);
        }

        [HttpPost]
        [Route("actualize")]
        public ActionResult<List<int>> Actualize(int projectId, float percentage)
        {
            throw new NotImplementedException(); //TODO
        }

        private ActionResult<AddDto> FinishOrNext(int projectId, AddDto result)
        {
            if (result.Added)
                result.Items = GetAllProjectItems(projectId);
            else
                result.Next = service.NextForAdd(result.Id);

            return result;
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
            public int Id { get; set; }
            public string Next { get; set; }
            public IEnumerable<ObjectiveDto> Items { get; set; }
        }
    }
}