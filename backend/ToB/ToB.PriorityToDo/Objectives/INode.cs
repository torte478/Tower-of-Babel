namespace ToB.PriorityToDo.Objectives
{
    public interface INode<out T>
    {
        INode<T> Left { get; }
        INode<T> Right { get; }
        T Value { get; }
    }
}