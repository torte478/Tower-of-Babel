using System;
using System.Collections.Generic;
using System.Linq;
using ToB.Common.DB;
using ToB.Common.Extensions;
using ToB.PriorityToDo.DB;

namespace ToB.PriorityToDo.Projects
{
    public sealed class Service : IService
    {
        private readonly ICrud<int, Project> crud;
        private readonly int root;
        private readonly Func<List<Project>, int, TreeDto> buildTree;

        public Service(ICrud<int, Project> crud, int root, Func<List<Project>, int, TreeDto> buildTree)
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

        public TreeDto ToProjects()
        {
            return crud.List()
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