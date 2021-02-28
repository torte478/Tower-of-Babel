using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using ToB.Common.Extensions;
using ToB.PriorityToDo;

namespace ToB.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class PriorityToDoController : ControllerBase
    {
        private readonly IPriorityToDoService service;

        public PriorityToDoController(IPriorityToDoService service)
        {
            this.service = service;
        }

        #region Projects

        [HttpPost]
        [Route("projects/create")]
        public ActionResult<int> Create(int parentId, string name)
        {
            return service.AddProject(parentId, name);
        }

        [HttpGet]
        [Route("projects")]
        public ActionResult<ProjectDto> Read()
        {
            return service
                .ToProjects()
                ._(ToProjectDtoProjection);
        }

        [HttpPost]
        [Route("projects/update")]
        public ActionResult<bool> Update(int id, string name)
        {
            return service.UpdateProject(id, name);
        }

        [HttpDelete]
        [Route("projects")]
        public ActionResult<bool> Delete(int id)
        {
            return service.DeleteProject(id);
        }

        #endregion
        
        #region Items

        [HttpGet]
        [Route("items")]
        public ActionResult<List<ItemDto>> GetAll(int projectId)
        {
            return new ActionResult<List<ItemDto>>(GetAllProjectItems(projectId));
        }

        [HttpPost]
        [Route("items/startAdd")]
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
        [Route("items/continueAdd")]
        public ActionResult<AddDto> ContinueAdd(int projectId, int id, bool greater)
        {
            var result = service
                .ContinueAdd(projectId, id, greater)
                ._(_ => new AddDto
                {
                    Added = _.added,
                    Id = id
                });

            return FinishOrNext(projectId, result);
        }

        [HttpDelete]
        [Route("items")]
        public ActionResult<List<ItemDto>> Complete(int projectId, int id)
        {
            service.Remove(projectId, id);

            return new ActionResult<List<ItemDto>>(GetAllProjectItems(projectId));
        }

        [HttpPost]
        [Route("items/update")]
        public ActionResult<bool> Change(int projectId, int id, string text)
        {
            return service.Update(projectId, id, text);
        }

        [HttpPost]
        [Route("items/actualize")]
        public ActionResult<List<int>> Actualize(int projectId, float percentage)
        {
            throw new NotImplementedException(); //TODO
        }

        #endregion

        #region Inner impl
        
        private ActionResult<AddDto> FinishOrNext(int projectId, AddDto result)
        {
            if (result.Added)
                result.Items = GetAllProjectItems(projectId);
            else
                result.Next = service.NextForAdd(projectId, result.Id);

            return result;
        }
        
        private List<ItemDto> GetAllProjectItems(int projectId)
        {
            return service
                .ToPriorityList(projectId)
                .Select(_ => new ItemDto
                {
                    Id = _.id,
                    Text = _.text
                })
                .ToList();
        }
        
        private static ProjectDto ToProjectDtoProjection(ITree<(int id, string name)> tree)
        {
            return new ProjectDto
            {
                Id = tree.Item.id,
                Name = tree.Item.name,
                Children = tree.Children.Select(ToProjectDtoProjection).ToList()
            };
        }

        public sealed class ProjectDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<ProjectDto> Children { get; set; }
        }
        
        public sealed class ItemDto
        {
            public int Id { get; set; }
            public string Text { get; set; }
        }

        public sealed class AddDto
        {
            public bool Added { get; set; }
            public int Id { get; set; }
            public string Next { get; set; }
            public IEnumerable<ItemDto> Items { get; set; }
        }
        
        #endregion
    }
}