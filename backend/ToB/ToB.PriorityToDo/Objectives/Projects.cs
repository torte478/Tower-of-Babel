using System;
using System.Collections.Generic;
using System.Linq;

using ToB.PriorityToDo.DB;

namespace ToB.PriorityToDo.Objectives
{
    public sealed class Projects : IProjects
    {
        private readonly Context context;
        private readonly Func<int, IProject> createProject;
        
        private readonly Dictionary<int, IProject> projects = new();

        public Projects(Context context, Func<int, IProject> createProject)
        {
            this.context = context;
            this.createProject = createProject;
        }

        public IProject this[int id] => Contains(id)
            ? projects[id]
            : throw new ArgumentException($"Project {id} isn't exists", nameof(id));
        
        public bool Contains(int id)
        {
            if (projects.ContainsKey(id))
                return true;

            if (!context.Projects.Any(_ => _.Id == id)) 
                return false;
            
            projects.Add(id, createProject(id));
            return true;

        }
    }
}