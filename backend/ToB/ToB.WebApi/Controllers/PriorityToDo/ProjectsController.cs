using Microsoft.AspNetCore.Mvc;

using ToB.PriorityToDo;
using ToB.PriorityToDo.Contracts;
using ToB.PriorityToDo.Projects;

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
            return service.Create(parentId, name);
        }

        [HttpGet]
        public ActionResult<TreeDto> Read()
        {
            return service.ToProjects();
        }

        [HttpPost]
        [Route("update")]
        public ActionResult<bool> Update(int id, string name)
        {
            return service.Update(id, name);
        }

        [HttpDelete]
        public ActionResult<bool> Delete(int id)
        {
            return service.Delete(id);
        }
    }
}