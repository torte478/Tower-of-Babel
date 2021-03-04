using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using ToB.Common.Extensions;
using ToB.PriorityToDo;

namespace ToB.WebApi.Controllers.PriorityToDo
{
    [Route("api/priorityToDo/[controller]")]
    [ApiController]
    public sealed class ProjectsController : ControllerBase
    {
        private readonly IProjectService service;

        public ProjectsController(IProjectService service)
        {
            this.service = service;
        }
        
        [HttpPost]
        [Route("create")]
        public ActionResult<int> Create(int parentId, string name)
        {
            return service.AddProject(parentId, name);
        }

        [HttpGet]
        public ActionResult<ProjectDto> Read()
        {
            return service
                .ToProjects()
                ._(ToProjectDtoProjection);
        }

        [HttpPost]
        [Route("update")]
        public ActionResult<bool> Update(int id, string name)
        {
            return service.UpdateProject(id, name);
        }

        [HttpDelete]
        public ActionResult<bool> Delete(int id)
        {
            return service.DeleteProject(id);
        }
        
        private static ProjectDto ToProjectDtoProjection(ITree<(int id, string name)> tree)
        {
            return new()
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
    }
}