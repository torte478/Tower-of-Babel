using System.Collections.Generic;

namespace ToB.PriorityToDo.Objectives
{
    public interface IService
    {
        IEnumerable<(int id, string text)> ToPriorityList(int project);
        int? FindRoot(int project);
        (bool added, int next) TryAdd(int project, int target, bool greater, string text);
        bool Remove(int project, int id);
        bool Update(int project, int id, string text);
    }
}