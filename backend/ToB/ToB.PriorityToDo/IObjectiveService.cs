using System.Collections.Generic;

namespace ToB.PriorityToDo
{
    public interface IObjectiveService
    {
        IEnumerable<(int id, string text)> ToPriorityList(int project);
        (bool added, int id) StartAdd(int project, string text);
        string NextForAdd(int project, int id);
        (bool added, string next) ContinueAdd(int id, bool greater);
        bool Remove(int id);
        bool Update(int id, string text);
    }
}