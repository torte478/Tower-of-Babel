namespace ToB.PriorityToDo.Objectives
{
    public interface INode<T>
    {
        INode<T> Left { get; }
        INode<T> Right { get; }
        T Value { get; }
        
        (bool, INode<T>) ToPrevious();
        (bool, INode<T>) ToNext();
    }
}