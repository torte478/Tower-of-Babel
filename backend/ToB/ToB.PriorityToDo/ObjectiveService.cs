﻿using System.Collections.Generic;
using System.Linq;

using ToB.Common.DB;
using ToB.Common.Extensions;
using ToB.PriorityToDo.DB;
using ToB.PriorityToDo.RBT;

namespace ToB.PriorityToDo
{
    public sealed class ObjectiveService : IObjectiveService
    {
        private readonly Context context;
        private readonly IBinarySearchTree<Node> tree;

        private ICrud<int, Objective> Objectives => context.AsCrud(_ => _.Objectives);

        
        public IEnumerable<(int id, string text)> ToPriorityList(int project)
        {
            var ids = tree.InOrderIterator
                .Where(_ => _.Project == project)
                .Select(_ => _.Id)
                .ToHashSet();

            return context.Objectives
                .Where(_ => ids.Contains(_.Id))
                .Select(_ => new { _.Id, _.Text })
                .ToList()
                .Select(_ => (_.Id, _.Text));
        }

        public (bool added, int id) StartAdd(int project, string text)
        {
            throw new System.NotImplementedException();
        }

        public string NextForAdd(int project, int id)
        {
            throw new System.NotImplementedException();
        }

        public (bool added, string next) ContinueAdd(int id, bool greater)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(int id)
        {
            return Objectives.Delete(id);
        }

        public bool Update(int id, string text)
        {
            var entity = Objectives.Read(id);
            
            var none = entity == null;
            if (none) return false;

            entity.Text = text;
            return Objectives.Update(entity);
        }
    }
}