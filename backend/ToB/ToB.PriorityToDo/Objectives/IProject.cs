using System.Collections.Generic;

namespace ToB.PriorityToDo.Objectives
{
    public interface IProject
    {
        IEnumerable<(int id, string text)> ToPriorityList();
        (bool added, int next) TryAdd(int target, string text, bool greater);
        bool Remove(int id);
        bool Update(int id, string text);
    }
}