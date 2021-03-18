using System;
using System.Collections.Generic;
using System.Linq;

namespace ToB.PriorityToDo.Objectives
{
    public sealed class Service : IService
    {
        private readonly Dictionary<int, IProject> projects;
        private readonly Func<int> nextId;
        
        private readonly Dictionary<int, (int project, int next)> operations = new();

        public Service(Dictionary<int, IProject> projects, Func<int> nextId)
        {
            this.projects = projects;
            this.nextId = nextId;
        }

        public IEnumerable<(int id, string text)> ToPriorityList(int project)
        {
            return projects.ContainsKey(project)
                ? projects[project].ToPriorityList()
                : Enumerable.Empty<(int, string)>();
        }

        public int StartAdd(int project)
        {
            var id = nextId();
            operations.Add(id, (project, -1)); //TODO : constant
            return id;
        }

        public (bool added, string next) ContinueAdd(int operation, string text, bool greater)
        {
            var (project, target) = operations[operation];
            var (added, otherwise, next) =  projects[project].TryAdd(target, text, greater);
            
            if (added)
                operations.Remove(operation);
            else
                operations[operation] = (project, otherwise);
            
            return (added, next);
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