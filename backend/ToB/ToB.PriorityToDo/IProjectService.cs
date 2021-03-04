namespace ToB.PriorityToDo
{
    public interface IProjectService
    {
        int Create(int parent, string name);
        ProjectTreeDto ToProjects();
        bool Update(int id, string name);
        bool Delete(int id);
    }
}