using System.Collections.Generic;
using System.Linq;
using ToB.Common.Extensions;
using ToB.PriorityToDo.DB;

namespace ToB.PriorityToDo.Projects
{
    public sealed class Trees
    {
        private readonly List<Project> projects;

        public Trees(List<Project> projects)
        {
            this.projects = projects;
        }

        public TreeDto Build(int root)
        {
            return projects
                .Single(_ => _.Id == root)
                ._(Build);
        }

        private TreeDto Build(Project node)
        {
            return projects
                .Where(_ => _.Parent == node.Id)
                .Select(Build)
                .ToList()
                ._(_ => new TreeDto
                {
                    Id = node.Id,
                    Name = node.Name,
                    Children = _
                });
        }
    }
}