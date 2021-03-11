using System;
using System.Collections.Generic;
using System.Linq;

using ToB.Common.DB;
using ToB.PriorityToDo.DB;

namespace ToB.PriorityToDo.Objectives
{
    public sealed class Project : IProject
    {
        private readonly int project;
        private readonly IMeasure measure;
        private readonly IBalancedTree<Objective> tree;
        private readonly ICrud<int, Objective> storage;

        private Project(int project, IMeasure measure, ICrud<int, Objective> storage, IBalancedTree<Objective> tree)
        {
            this.project = project;
            this.measure = measure;
            this.tree = tree;
            this.storage = storage;
        }

        public static IProject Create(int project, IMeasure measure, ICrud<int, Objective> storage, Func<IBalancedTree<Objective>> createTree)
        {
            var tree = createTree();
            FillTree(project, storage, tree);

            return new Project(project, measure, storage, tree);
        }

        public IEnumerable<(int id, string text)> ToPriorityList()
        {
            return tree
                .Enumerate()
                .Select(_ => (_.Id, _.Text));
        }

        public (bool added, int node) StartAdd(string text)
        {
            if (tree.Count > 0)
                return (false, tree.Root.Value);

            var value = measure.Next();
            Add(text, value);
            
            return (true, value);
        }

        public string NextForAdd(int value)
        {
            var node = tree.ToNode(new Objective { Value = value });
            return node.Value.Text;
        }

        public (bool added, int node) ContinueAdd(int value, bool greater, string text)
        {
            var node = tree.ToNode(new Objective {Value = value});
            if (greater)
            {
                if (node.Right != null)
                    return (false, node.Right.Value.Value);

                var (exists, next) = node.ToNext();
                var added = exists
                    ? measure.Next(node.Value.Value, next.Value.Value)
                    : measure.NextMax(node.Value.Value);
                
                Add(text, added);
                return (true, added);
            }
            else
            {
                if (node.Left != null)
                    return (false, node.Left.Value.Value);

                var (exists, previous) = node.ToPrevious();
                var added = exists
                    ? measure.Next(previous.Value.Value, node.Value.Value)
                    : measure.NextMin(node.Value.Value);
                
                Add(text, added);
                return (true, added);
            }
        }

        public bool Remove(int id)
        {
            return storage.Delete(id);
        }

        public bool Update(int id, string text)
        {
            return storage.Update(new Objective
            {
                Id = id,
                Text = text
            });
        }
        
        private void Add(string text, int value)
        {
            var added = new Objective
            {
                Project = project,
                Text = text,
                Value = value
            };
            
            added.Id = storage.Create(added);
            tree.Add(added);

            CheckRebuild(added);
        }

        private void CheckRebuild(Objective value)
        {
            var node = tree.ToNode(value);
            
            {
                var (exists, previous) = node.ToPrevious();
                if (exists && previous.Value.Value + 1 == node.Value.Value)
                    Rebuild();
            }
            
            {
                var (exists, next) = node.ToNext();
                if (exists && next.Value.Value - 1 == node.Value.Value)
                    Rebuild();
            }
        }

        private void Rebuild()
        {
            var nodes = tree
                .Enumerate()
                .ToList();

            var zip = nodes.Zip(measure.Fill(nodes.Count));

            foreach (var (node, value) in zip)
            {
                node.Value = value;
                storage.Update(node);
            }

            tree.Clear();
            FillTree(project, storage, tree);
        }
        
        private static void FillTree(int project, ICrud<int, Objective> storage, IBalancedTree<Objective> tree)
        {
            var nodes = storage
                .List()
                .Where(_ => _.Project == project);

            foreach (var node in nodes)
                tree.Add(node);
        }
    }
}