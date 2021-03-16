using System;
using ToB.Common.Extensions;
using ToB.PriorityToDo.Objectives.RBT;

namespace ToB.PriorityToDo.Objectives
{
    //TODO : remove to hell
    public sealed class Node<T> : INode<T> where T : IComparable<T>
    {
        private readonly RedBlackNode<T> origin;

        public INode<T> Left => origin.Left?._(_ => new Node<T>(_));
        public INode<T> Right => origin.Right?._(_ => new Node<T>(_));

        public T Value => origin.Value;
        
        public Node(RedBlackNode<T> origin)
        {
            this.origin = origin;
        }

        public override string ToString()
        {
            return $"Node ({Value})";
        }
    }
}