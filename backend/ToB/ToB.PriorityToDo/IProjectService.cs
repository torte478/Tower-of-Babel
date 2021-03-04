namespace ToB.PriorityToDo
{
    public interface IProjectService
    {
        int AddProject(int parent, string name);
        ITree<(int id, string name)> ToProjects();
        bool UpdateProject(int id, string name);
        bool DeleteProject(int id);
    }
}