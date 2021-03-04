using System.Collections.Generic;
using System.Linq;

using ToB.Common.Extensions;
using ToB.PriorityToDo.DB;

namespace ToB.PriorityToDo
{
    public sealed class Trees
    {
        private readonly List<Project> projects;

        public Trees(List<Project> projects)
        {
            this.projects = projects;
        }

        public ProjectTreeDto Build(int root)
        {
            return projects
                .Single(_ => _.Id == root)
                ._(Build);
        }

        private ProjectTreeDto Build(Project node)
        {
            return projects
                .Where(_ => _.Parent == node.Id)
                .Select(Build)
                .ToList()
                ._(_ => new ProjectTreeDto
                {
                    Id = node.Id,
                    Name = node.Name,
                    Children = _
                });
        }
    }
}