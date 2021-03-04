namespace ToB.Common.DB
{
    public interface IHaveId<out T>
    {
        public T Id { get; }
    }
}