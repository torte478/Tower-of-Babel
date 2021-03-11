using System.Collections.Generic;

namespace ToB.PriorityToDo.Objectives
{
    public interface IService
    {
        IEnumerable<(int id, string text)> ToPriorityList(int project);
        (bool added, int id) StartAdd(int project, string text);
        string NextForAdd(int id);
        (bool added, string next) ContinueAdd(int id, bool greater);
        bool Remove(int project, int id);
        bool Update(int project, int id, string text);
    }
}