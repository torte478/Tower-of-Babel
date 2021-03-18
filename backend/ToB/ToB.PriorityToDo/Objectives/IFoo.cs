using System;
using System.Collections.Generic;

namespace ToB.PriorityToDo.Objectives
{
    public interface IFoo<T>
    {
        event Action<T, int> NodeAdded;
        event Action<T> NodeRemoved;
        event Action<Dictionary<T, int>> Rebuilded;

        (bool added, T next) Add(T item);
        (bool added, T next) Add(T item, T target, bool greater);
        bool Remove(T value);
        IEnumerable<T> ToPriorityList();
    }
}