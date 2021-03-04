namespace ToB.Common.DB
{
    public interface ICopyable<in T>
    {
        void Copy(T other);
    }
}