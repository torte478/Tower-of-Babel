using System;
using System.Collections.Generic;
using System.Linq;

using ToB.Common.DB;
using ToB.Common.Extensions;
using ToB.PriorityToDo.DB;

namespace ToB.PriorityToDo
{
    public sealed class ProjectService : IProjectService
    {
        private readonly ICrud<int, Project> crud;
        private readonly int root;
        private readonly Func<List<Project>, int, ProjectTreeDto> buildTree;

        public ProjectService(ICrud<int, Project> crud, int root, Func<List<Project>, int, ProjectTreeDto> buildTree)
        {
            this.crud = crud;
            this.root = root;
            this.buildTree = buildTree;
        }

        public int Create(int parent, string name)
        {
            var added = crud.Create(new Project
            {
                Name = name,
                Parent = parent
            });

            return added;
        }

        public ProjectTreeDto ToProjects()
        {
            return crud.Read()
                .ToList()
                ._(_ => buildTree(_, root));
        }

        public bool Update(int id, string name)
        {
            return crud.Update(new Project
            {
                Id = id,
                Name = name,
            });
        }

        public bool Delete(int id)
        {
            return crud.Delete(id);
        }
    }
}