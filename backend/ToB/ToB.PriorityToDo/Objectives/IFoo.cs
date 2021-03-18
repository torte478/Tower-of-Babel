using System;
using System.Collections.Generic;

namespace ToB.PriorityToDo.Objectives
{
    public interface IFoo<T>
    {
        T? Root { get; }

        event Action<Dictionary<T, int>> Rebuilded;

        IEnumerable<T> ToPriorityList();
        
        void Add(T item, int value);
        (bool can, int next) CanAdd(int target, bool greater);
        int Add(T target, bool greater, T item);

        
        bool Remove(T item);

    }
}