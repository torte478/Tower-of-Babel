namespace ToB.PriorityToDo.Objectives
{
    public interface IProjects
    {
        bool Contains(int id);
        IProject this[int id] { get; }
    }
}