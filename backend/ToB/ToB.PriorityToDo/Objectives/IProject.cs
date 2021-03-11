using System.Collections.Generic;

namespace ToB.PriorityToDo.Objectives
{
    public interface IProject
    {
        IEnumerable<(int id, string text)> ToPriorityList();
        (bool added, int node) StartAdd(string text);
        string NextForAdd(int addedNode);
        (bool added, int node) ContinueAdd(int node, bool greater);
        bool Remove(int id);
        bool Update(int id, string text);
    }
}