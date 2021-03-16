using System;
using System.Collections.Generic;
using System.Linq;

namespace ToB.PriorityToDo.Objectives
{
    public sealed class Service : IService
    {
        private readonly IProjects projects;
        private readonly Func<int> getNextId;
        
        private readonly Dictionary<int, (int project, int node, string text)> toAdd = new();

        public Service(IProjects projects, Func<int> getNextId)
        {
            this.projects = projects;
            this.getNextId = getNextId;
        }

        public IEnumerable<(int id, string text)> ToPriorityList(int project)
        {
            return projects.Contains(project)
                ? projects[project].ToPriorityList()
                : Enumerable.Empty<(int, string)>();
        }

        public (bool added, int id) StartAdd(int project, string text)
        {
            var (added, node) = projects[project].StartAdd(text);

            if (added) return (true, default);
            
            var id = getNextId();
            toAdd.Add(id, (project, node, text));
            return (false, id);
        }

        public string NextForAdd(int id)
        {
            var added = toAdd[id];
            return projects[added.project].NextForAdd(added.node);
        }

        public (bool added, string next) ContinueAdd(int id, bool greater)
        {
            var added = toAdd[id];
            var result = projects[added.project].ContinueAdd(added.node, greater, added.text);

            if (result.added)
            {
                toAdd.Remove(id);
                return (true, string.Empty);
            }

            toAdd[id] = (added.project, result.node, added.text);
            return (false, NextForAdd(id));
        }

        public bool Remove(int project, int id)
        {
            return projects.Contains(project) && projects[project].Remove(id);
        }

        public bool Update(int project, int id, string text)
        {
            return projects.Contains(project) && projects[project].Update(id, text);
        }
    }
}