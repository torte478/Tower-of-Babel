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
        
        public IEnumerable<T> ToOrdered()
        {
            return EnumerateFrom(root);
        }

        public RedBlackTree<T> Add(T value)
        {
            var node = new Node(value, Nil);
            if (root == Nil)
            {
                root = node;
            }
            else
            {
                InsertRecurse(root, node);
                InsertRepairTree(node);   
            }

            return this;
        }

        private void InsertRepairTree(Node n)
        {
            if (n.Parent == Nil)
                InsertCase1(n);
            else if (n.Parent.Black)
                InsertCase2(n);
            else if (n.Uncle != Nil && n.Uncle.Red)
                InsertCase3(n);
            else
                InsertCase4(n);
        }

        private void InsertCase1(Node node)
        {
            node.Black = true;
        }

        private void InsertCase2(Node node)
        {
            
        }

        private void InsertCase3(Node node)
        {
            node.Parent.Black = true;
            node.Uncle.Black = true;
            node.Grandparent.Red = true;
            InsertRepairTree(node.Grandparent);
        }

        private void InsertCase4(Node n)
        {
            var p = n.Parent;
            var g = n.Grandparent;

            if (n.IsRightChild && p.IsLeftChild)
            {
                LeftRotate(p);
                n = n.Left;
            }
            else if (n.IsLeftChild && p.IsRightChild)
            {
                RightRotate(p);
                n = n.Right;
            }

            p = n.Parent;
            g = n.Grandparent;

            if (n.IsLeftChild)
                RightRotate(g);
            else
                LeftRotate(g);
            
            p.Black = true;
            g.Red = true;
        }

        private void InsertRecurse(Node r, Node n)
        {
            if (r != Nil)
            {
                if (n.Value.CompareTo(r.Value) < 0)
                {
                    if (r.Left != Nil)
                    {
                        InsertRecurse(r.Left, n);
                        return;
                    }

                    r.Left = n;
                }
                else
                {
                    if (r.Right != Nil)
                    {
                        InsertRecurse(r.Right, n);
                        return;
                    }

                    r.Right = n;
                }
            }

            n.Parent = r;
            n.Red = true;
        }

        private void RightRotate(Node n)
        {
            var nnew = n.Left;
            var p = n.Parent;

            n.Left = nnew.Right;
            nnew.Right = n;
            n.Parent = nnew;

            if (n.Left != Nil)
                n.Left.Parent = n;

            if (p != Nil)
            {
                if (n == p.Right)
                    p.Right = nnew;
                else if (n == p.Left)
                    p.Left = nnew;
            }

            nnew.Parent = p;
            
            if (root.Parent != Nil)
                root = root.Parent;
        }

        private void LeftRotate(Node n)
        {
            var nnew = n.Right;
            var p = n.Parent;

            n.Right = nnew.Left;
            nnew.Left = n;
            n.Parent = nnew;

            if (n.Right != Nil)
                n.Right.Parent = n;

            if (p != Nil)
            {
                if (n == p.Left)
                    p.Left = nnew;
                else if (n == p.Right)
                    p.Right = nnew;
            }

            nnew.Parent = p;
            
            if (root.Parent != Nil)
                root = root.Parent;
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