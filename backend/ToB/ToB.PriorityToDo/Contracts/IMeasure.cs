using System.Collections.Generic;

namespace ToB.PriorityToDo.Contracts
{
    public interface IMeasure
    {
        int Next();
        int Next(int min, int max);
        int NextMax(int max);
        int NextMin(int min);
        IEnumerable<int> Fill(int count);
    }
}