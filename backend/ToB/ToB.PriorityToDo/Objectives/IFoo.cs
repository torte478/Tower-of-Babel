using System;
using System.Collections.Generic;

namespace ToB.PriorityToDo.Objectives
{
    public interface IFoo<T>
    {
        event Action<Dictionary<T, int>> Rebuilded;

        IEnumerable<T> ToPriorityList();
        
        void Add(T item, int value);
        (bool can, T next) CanAdd(T target, bool greater);
        int Add(T target, bool greater, T item);

        (bool exists, T root) FindRoot();

        
        bool Remove(T item);

    }
}