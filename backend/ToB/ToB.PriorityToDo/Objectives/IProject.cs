using System.Collections.Generic;

namespace ToB.PriorityToDo.Objectives
{
    public interface IProject
    {
        IEnumerable<(int id, string text)> ToPriorityList();
        (bool added, int node) StartAdd(string text);
        string NextForAdd(int value);
        (bool added, int node) ContinueAdd(int value, bool greater, string text);
        bool Remove(int id);
        bool Update(int id, string text);
    }
}