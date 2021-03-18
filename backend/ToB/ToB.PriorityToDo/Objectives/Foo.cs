using System;
using System.Collections.Generic;
using System.Linq;
using ToB.Common.Extensions;
using ToB.PriorityToDo.Objectives.RBT;

namespace ToB.PriorityToDo.Objectives
{
    public sealed class Foo<T> : IFoo<T>
    {
        private readonly Dictionary<T, int> values = new();
        private readonly RedBlackTree<int> tree = new();
        
        private readonly IMeasure measure;

        public event Action<T, int> NodeAdded;
        public event Action<T> NodeRemoved;
        public event Action<Dictionary<T, int>> Rebuilded;

        public Foo(IMeasure measure)
        {
            this.measure = measure;
        }

        public (bool added, T next) Add(T item)
        {
            if (values.ContainsKey(item))
                throw new FooException($"Item already exists: {item}");
            
            if (values.Count == 0)
            {
                var value = measure.Next();
                return Save(item, value);
            }
            else
            {
                return (false, values.First(_ => _.Value == tree.Root.Value).Key);
            }
        }

        public (bool added, T next) Add(T item, T target, bool greater)
        {
            while (true)
            {
                if (values.ContainsKey(item)) 
                    throw new FooException($"Item already exists: {item}");
                if (!values.ContainsKey(target)) 
                    throw new FooException($"Target item isn't exists: {target}");

                var value = values[target];
                var node = ToNode(value);
                var child = greater ? node.Right : node.Left;

                if (child != null) return (false, values.First(_ => _.Value == child.Value).Key);

                var added = greater
                    ? measure.Next(value, FindNext(node))
                    : measure.Next(FindPrevious(node), value);

                if (!tree.Contains(added)) 
                    return Save(item, added);

                Rebuild();
            }
        }

        public bool Remove(T value)
        {
            var contains = values.ContainsKey(value);
            if (!contains)
                return false;

            tree.Remove(values[value]);
            values.Remove(value);
            NodeRemoved.Raise(value);
            return true;
        }

        public IEnumerable<T> ToPriorityList()
        {
            return tree
                .InOrderIterator
                .Reverse()
                .Select(_ => values.First(x => x.Value == _).Key); //TODO : refactor !!!
        }
        
        private (bool, T) Save(T item, int added)
        {
            if (tree.Contains(added))
                Rebuild();
            
            values.Add(item, added);
            tree.Insert(added);
            NodeAdded.Raise(item, added);
            
            return (true, default);
        }

        private void Rebuild()
        {
            var updated = values
                .OrderBy(_ => _.Value)
                .Select(_ => _.Key)
                .Zip(measure.Fill(values.Count))
                .ToDictionary(x => x.First, x => x.Second);

            values.Clear();
            tree.Clear();
            foreach (var update in updated)
            {
                values.Add(update.Key, update.Value);
                tree.Add(update.Value);
            }

            Rebuilded.Raise(updated);
        }

        private RedBlackNode<int> ToNode(int value)
        {
            var current = tree.Root;
            while (true)
            {
                var compare = value.CompareTo(current.Value);
                if (compare == 0)
                    return current;

                current = compare < 0
                    ? current.Left
                    : current.Right;
            }
        }

        private int FindNext(RedBlackNode<int> node)
        {
            var next = false;
            foreach (var current in tree.InOrderIterator)
            {
                if (next)
                    return current;

                next = current.CompareTo(node.Value) == 0;
            }

            return measure.NextMax(tree.Max());
        }

        private int FindPrevious(RedBlackNode<int> node)
        {
            var previous = measure.NextMin(tree.Min());
            foreach (var current in tree.InOrderIterator)
            {
                if (current.CompareTo(node.Value) == 0)
                    break;

                previous = current;
            }

            return previous;
        }
    }
}