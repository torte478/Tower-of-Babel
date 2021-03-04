using System;
using System.Collections.Generic;
using System.Linq;

using ToB.Common.Extensions;
using ToB.PriorityToDo.DB;

namespace ToB.PriorityToDo
{
    public sealed class ProjectService : IProjectService
    {
        private readonly Context context;
        private readonly int root;
        private readonly Func<List<Project>, int, ProjectTreeDto> buildTree;

        public ProjectService(Context context, int root, Func<List<Project>, int, ProjectTreeDto> buildTree)
        {
            this.context = context;
            this.root = root;
            this.buildTree = buildTree;
        }

        public int Create(int parent, string name)
        {
            var added = context.Projects.Add(new Project
            {
                Name = name,
                Parent = parent
            });
            context.SaveChanges();

            return added.Entity.Id;
        }

        public ProjectTreeDto ToProjects()
        {
            return context.Projects
                .ToList()
                ._(_ => buildTree(_, root));
        }

        public bool Update(int id, string name)
        {
            var project = Find(id);

            var exists = project != null;
            if (exists)
            {
                project.Name = name;
                context.Update(project);
                context.SaveChanges();
            }
            return exists;
        }

        public bool Delete(int id)
        {
            var project = Find(id);

            var exists = project != null;
            if (exists)
            {
                context.Projects.Remove(project);
                context.SaveChanges();
            }
            return exists;
        }
        
        private Project Find(int id)
        {
            return context.Projects.FirstOrDefault(_ => _.Id == id);
        }
    }
}