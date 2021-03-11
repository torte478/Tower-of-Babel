using System;

using ToB.PriorityToDo.Objectives.RBT;

namespace ToB.PriorityToDo.Objectives
{
    //TODO : remove to hell
    public sealed class Node<T> : INode<T> where T : IComparable<T>
    {
        private readonly RedBlackNode<T> origin;

        public INode<T> Left => new Node<T>(origin.Left);
        public INode<T> Right => new Node<T>(origin.Right);
        public T Value => origin.Value;
        
        public Node(RedBlackNode<T> origin)
        {
            this.origin = origin;
        }
        
        public (bool, INode<T>) ToPrevious()
        {
            var previous = origin.InOrderPredecessor;
            return (previous != null, new Node<T>(previous));
        }

        public (bool, INode<T>) ToNext()
        {
            var next = origin.InOrderSuccessor;
            return (next != null, new Node<T>(next));
        }
    }
}