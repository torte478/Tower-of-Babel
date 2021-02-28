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
                InsertToLeaf(root, node);
                RepairTree(node);   
            }

            return this;
        }

        private void RepairTree(Node node)
        {
            if (node.Parent == Nil)
                node.Black = true;

            if (node.Parent == Nil || node.Parent.Black) 
                return;
            
            if (node.Uncle != Nil && node.Uncle.Red)
                RecolorUncle(node);
            else
                MakeRotate(node);
        }

        private void RecolorUncle(Node node)
        {
            node.Parent.Black = true;
            node.Uncle.Black = true;
            node.Grandparent.Red = true;
            RepairTree(node.Grandparent);
        }

        private void MakeRotate(Node node)
        {
            var parent = node.Parent;

            if (node.IsRightChild && parent.IsLeftChild)
            {
                LeftRotate(parent);
                node = node.Left;
            }
            else if (node.IsLeftChild && parent.IsRightChild)
            {
                RightRotate(parent);
                node = node.Right;
            }

            parent = node.Parent;
            var grandparent = node.Grandparent;

            if (node.IsLeftChild)
                RightRotate(grandparent);
            else
                LeftRotate(grandparent);
            
            parent.Black = true;
            grandparent.Red = true;
        }

        private static void InsertToLeaf(Node current, Node added)
        {
            if (current != Nil)
            {
                if (added.Value.CompareTo(current.Value) < 0)
                {
                    if (current.Left != Nil)
                    {
                        InsertToLeaf(current.Left, added);
                        return;
                    }

                    current.Left = added;
                }
                else
                {
                    if (current.Right != Nil)
                    {
                        InsertToLeaf(current.Right, added);
                        return;
                    }

                    current.Right = added;
                }
            }

            added.Parent = current;
            added.Red = true;
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