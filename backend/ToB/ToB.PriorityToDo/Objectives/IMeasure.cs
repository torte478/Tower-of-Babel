using System.Collections.Generic;

namespace ToB.PriorityToDo.Objectives
{
    public interface IMeasure
    {
        int Min { get; }
        int Max { get; }
        
        int Next(int min, int max);
        IEnumerable<int> Fill(int count);
    }
}