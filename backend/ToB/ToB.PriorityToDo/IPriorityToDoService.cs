using System.Collections.Generic;

namespace ToB.PriorityToDo
{
    public interface IPriorityToDoService
    {
        int AddProject(int parent, string name);
        ITree<(int id, string name)> ToProjects();
        bool UpdateProject(int id, string name);
        bool DeleteProject(int id);

        IEnumerable<(int id, string text)> ToPriorityList(int project);
        (bool added, int id) StartAdd(int project, string text);
        string NextForAdd(int project, int id);
        (bool added, string next) ContinueAdd(int projectId, int id, bool greater);
        bool Remove(int project, int id);
        bool Update(int project, int id, string text);
    }
}