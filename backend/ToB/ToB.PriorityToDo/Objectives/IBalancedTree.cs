using System.Collections.Generic;

namespace ToB.PriorityToDo.Objectives
{
    public interface IBalancedTree<T>
    {
        int Count { get; }
        T Root { get; }

        INode<T> ToNode(T value);
        void Add(T value);
        IEnumerable<T> Enumerate();
        void Clear();
    }
}