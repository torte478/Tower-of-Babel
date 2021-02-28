using System;
using System.Collections.Generic;

using ToB.Common.Extensions;

namespace ToB.PriorityToDo
{
    public sealed partial class RedBlackTree<T> where T : IComparable<T>
    {
        public static Node Nil { get; private set; }

        public Node Root { get; private set; }

        static RedBlackTree()
        {
            Nil = new Node(true);
            Nil.Parent = Nil;
            Nil.Left = Nil;
            Nil.Right = Nil;
        }

        public RedBlackTree()
        {
            Root = Nil;
        }
        
        public IEnumerable<T> ToOrdered()
        {
            return EnumerateFrom(Root);
        }

        public RedBlackTree<T> Add(T value)
        {
            var node = new Node(value, Nil);
            if (Root == Nil)
            {
                Root = node;
                Root.Black = true;
            }
            else
            {
                InsertToLeaf(Root, node);
                RepairTree(node);   
            }

            return this;
        }

        public RedBlackTree<T> Remove(T value)
        {
            DeleteOneChild(Root, value);

            return this;
        }

        private void DeleteOneChild(Node n, T value)
        {
            var diff = value.CompareTo(n.Value); 
            if (diff < 0)
            {
                DeleteOneChild(n.Left, value);
                return;
            }
            else if (diff > 0)
            {
                DeleteOneChild(n.Right, value);
                return;
            }
            
            if (n.Left != Nil && n.Right != Nil)
            {
                var successor = FindMin(n.Right);
                n.Value = successor.Value;
                DeleteOneChild(successor, successor.Value);
            }
            else if (n.Left != Nil)
                ReplaceNodeInParent(n, n.Left);
            else if (n.Right != Nil)
                ReplaceNodeInParent(n, n.Right);
            else
                ReplaceNodeInParent(n, Nil);
            
            DeleteCase1(n);
        }

        private Node FindMin(Node node)
        {
            var current = node;
            while (node.Left != Nil)
                current = node.Left;
            return current;
        }

        private void ReplaceNodeInParent(Node current, Node replaced)
        {
            if (current.Parent != Nil)
            {
                if (current.IsLeftChild)
                    current.Parent.Left = replaced;
                else
                    current.Parent.Right = replaced;
            }

            if (replaced != Nil)
                replaced.Parent = current.Parent;
        }

        private void DeleteCase1(Node n)
        {
            if (n.Parent != Nil)
                DeleteCase2(n);
        }

        private void DeleteCase2(Node n)
        {
            var s = n.Sibling;

            if (s.Red)
            {
                n.Parent.Red = true;
                s.Black = true;
                if (n.IsLeftChild)
                    LeftRotate(n.Parent);
                else
                    RightRotate(n.Parent);
            }

            DeleteCase3(n);
        }

        private void DeleteCase3(Node n)
        {
            var s = n.Sibling;

            if (n.Parent.Black && s.Black && s.Left.Black && s.Right.Black)
            {
                s.Red = true;
                DeleteCase1(n.Parent);
            }
            else
            {
                DeleteCase4(n);
            }
        }

        private void DeleteCase4(Node n)
        {
            var s = n.Sibling;

            if (n.Parent.Red && s.Black && s.Left.Black && s.Right.Black)
            {
                s.Red = true;
                n.Parent.Black = true;
            }
            else
            {
                DeleteCase5(n);
            } 
        }

        private void DeleteCase5(Node n)
        {
            var s = n.Sibling;

            if (s.Black)
            {
                if (n.IsLeftChild && s.Right.Black && s.Left.Red)
                {
                    s.Red = true;
                    s.Left.Black = true;
                    RightRotate(s);
                }
                else if (n.IsRightChild && s.Left.Black && s.Right.Red)
                {
                    s.Red = true;
                    s.Right.Black = true;
                    LeftRotate(s);
                }
            }

            DeleteCase6(n);
        }

        private void DeleteCase6(Node n)
        {
            var s = n.Sibling;

            s.Black = n.Parent.Black;
            n.Parent.Black = true;

            if (n.IsLeftChild)
            {
                s.Right.Black = true;
                LeftRotate(n.Parent);
            }
            else
            {
                s.Left.Black = true;
                RightRotate(n.Parent);
            }
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
            
            if (Root.Parent != Nil)
                Root = Root.Parent;
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
            
            if (Root.Parent != Nil)
                Root = Root.Parent;
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