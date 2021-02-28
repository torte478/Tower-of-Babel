using System;
using System.Collections.Generic;

using ToB.Common.Extensions;

namespace ToB.PriorityToDo
{
    public sealed partial class RedBlackTree<T> where T : IComparable<T>
    {
        private static readonly Node Nil;

        private Node root;

        static RedBlackTree()
        {
            Nil = new Node(true);
            Nil.Parent = Nil;
            Nil.Left = Nil;
            Nil.Right = Nil;
        }

        public RedBlackTree()
        {
            root = Nil;
        }

        public RedBlackTree<T> Add(T item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> ToOrdered()
        {
            return EnumerateFrom(root);
        }

        private static IEnumerable<T> EnumerateFrom(Node node)
        {
            if (node == Nil)
                yield break;

            foreach (var child in node.Left._(EnumerateFrom))
                yield return child;
            
            yield return node.Value;
            
            foreach (var child in node.Right._(EnumerateFrom))
                yield return child;
        }
    }
}