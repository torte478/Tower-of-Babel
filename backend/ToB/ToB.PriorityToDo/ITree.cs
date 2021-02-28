using System.Collections.Generic;

namespace ToB.PriorityToDo
{
    public interface ITree<out T>
    {
        public T Item { get; }
        public IEnumerable<ITree<T>> Children { get; }
    }
}