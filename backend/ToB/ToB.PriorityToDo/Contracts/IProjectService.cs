namespace ToB.PriorityToDo.Contracts
{
    public interface IProjectService
    {
        int Create(int parent, string name);
        TreeDto ToProjects();
        bool Update(int id, string name);
        bool Delete(int id);
    }
}