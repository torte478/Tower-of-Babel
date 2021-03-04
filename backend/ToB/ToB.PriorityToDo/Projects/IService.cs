namespace ToB.PriorityToDo.Projects
{
    public interface IService
    {
        int Create(int parent, string name);
        TreeDto ToProjects();
        bool Update(int id, string name);
        bool Delete(int id);
    }
}