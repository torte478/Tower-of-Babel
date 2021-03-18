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
        private readonly IFoo<int> tree;
        private readonly ICrud<int, Objective> storage;
        
        private readonly Dictionary<int, int> objectiveToNode = new();

        // private Project(int project, IMeasure measure, ICrud<int, Objective> storage, IBalancedTree<Objective> tree)
        // {
        //     this.project = project;
        //     this.measure = measure;
        //     this.tree = tree;
        //     this.storage = storage;
        // }
        //
        // public static IProject Create(int project, IMeasure measure, ICrud<int, Objective> storage, Func<IBalancedTree<Objective>> createTree)
        // {
        //     var tree = createTree();
        //     FillTree(project, storage, tree);
        //
        //     return new Project(project, measure, storage, tree);
        // }

        public IEnumerable<(int id, string text)> ToPriorityList()
        {
            return storage.List() // TODO : optimize list
                .Where(_ => _.Project == project)
                .Select(_ => (_.Id, _.Text));
        }

        public (bool added, int next) TryAdd(int target, string text, bool greater)
        {
            var result = tree.Add(target, greater);
        }
        
        public (bool added, int node) ContinueAdd(int value, bool greater, string text)
        {
            var node = tree.ToNode(new Objective {Value = value});
            if (greater)
            {
                if (node.Right != null)
                    return (false, node.Right.Value.Value);

                var next = tree.FindNext(node);
                var added = next != null
                    ? measure.Next(node.Value.Value, next.Value)
                    : measure.NextMax(node.Value.Value);
                
                Add(text, added);
                return (true, added);
            }
            else
            {
                if (node.Left != null)
                    return (false, node.Left.Value.Value);

                var previous = tree.FindPrevious(node);
                var added = previous != null
                    ? measure.Next(previous.Value, node.Value.Value)
                    : measure.NextMin(node.Value.Value);
                
                Add(text, added);
                return (true, added);
            }
        }

        public bool Remove(int id)
        {
            var deleted = storage.Delete(id);
            
            if (deleted)
                Reload();
            
            return deleted;
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
                var previous = tree.FindPrevious(node);
                if (previous != null && previous.Value + 1 == node.Value.Value)
                    Rebuild();
            }

            {
                var next = tree.FindNext(node);
                if (next != null && next.Value - 1 == node.Value.Value)
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

            Reload();
        }

        private void Reload()
        {
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