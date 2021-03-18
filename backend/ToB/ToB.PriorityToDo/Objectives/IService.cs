using System.Collections.Generic;

namespace ToB.PriorityToDo.Objectives
{
    public interface IService
    {
        IEnumerable<(int id, string text)> ToPriorityList(int project);
        int StartAdd(int project);
        (bool added, int next) TryAdd(int operation, string text, bool greater);
        bool Remove(int project, int id);
        bool Update(int project, int id, string text);
    }
}