using System;
using System.Collections.Generic;
using System.Linq;
using ToB.PriorityToDo.Objectives.RBT;

namespace ToB.PriorityToDo.Objectives
{
    public sealed class BalancedTree<T> : IBalancedTree<T> where T : IComparable<T>
    {
        private readonly RedBlackTree<T> origin = new();
        private readonly Func<RedBlackNode<T>, INode<T>> toNode;

        public int Count => origin.Count;
        public T Root => origin.Root.Value;
        
        public BalancedTree(Func<RedBlackNode<T>, INode<T>> toNode)
        {
            this.toNode = toNode;
        }

        public INode<T> ToNode(T value)
        {
            var current = origin.Root;
            while (true)
            {
                var compare = value.CompareTo(current.Value);
                if (compare == 0)
                    return toNode(current);

                current = compare < 0
                    ? current.Left
                    : current.Right;
            }
        }

        public T FindNext(INode<T> node)
        {
            var next = false;
            foreach (var current in origin.InOrderIterator)
            {
                if (next)
                    return current;

                next = current.CompareTo(node.Value) == 0;
            }

            return default;
        }

        public T FindPrevious(INode<T> node)
        {
            T previous = default;
            foreach (var current in origin.InOrderIterator)
            {
                if (current.CompareTo(node.Value) == 0)
                    break;

                previous = current;
            }

            return previous;
        }

        public void Add(T value)
        {
            origin.Add(value);
        }

        public IEnumerable<T> Enumerate()
        {
            return origin
                .InOrderIterator
                .Reverse();
        }

        public void Clear()
        {
            origin.Clear();
        }
    }
}