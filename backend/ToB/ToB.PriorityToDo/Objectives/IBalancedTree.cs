using System;
using System.Collections.Generic;
using ToB.PriorityToDo.Objectives.RBT;

namespace ToB.PriorityToDo.Objectives
{
    public interface IBalancedTree<T>// : IBinarySearchTree<T> where T : IComparable<T>
    {
        int Count { get; }
        T Root { get; }

        INode<T> ToNode(T value);
        void Add(T value);
        IEnumerable<T> Enumerate();
        void Clear();
    }
}