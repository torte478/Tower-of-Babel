namespace ToB.PriorityToDo.Contracts
{
    public interface IProjects
    {
        bool Contains(int id);
        IProject this[int id] { get; }
    }
}