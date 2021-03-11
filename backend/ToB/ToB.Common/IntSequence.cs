namespace ToB.Common
{
    public sealed class IntSequence
    {
        private int value = 0;

        public int Next() => ++value;
    }
}