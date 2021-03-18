using System.Collections.Generic;
using System.Linq;

namespace ToB.PriorityToDo.Objectives
{
    public sealed class Service : IService
    {
        private readonly IProjects projects;

        public Service(IProjects projects)
        {
            this.projects = projects;
        }

        public IEnumerable<(int id, string text)> ToPriorityList(int project)
        {
            return projects.Contains(project)
                ? projects[project].ToPriorityList()
                : Enumerable.Empty<(int, string)>();
        }

        public int? FindRoot(int project)
        {
            return projects[project].FindRoot();
        }

        public (bool added, int next) TryAdd(int project, int target, bool greater, string text)
        {
            return projects[project].TryAdd(target, greater, text);
        }
        
        public bool Remove(int project, int id)
        {
            return projects[project].Remove(id);
        }

        public bool Update(int project, int id, string text)
        {
            return projects[project].Update(id, text);
        }
    }
}