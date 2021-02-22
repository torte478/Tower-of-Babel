using System.Collections.Generic;
using ToB.DB;

namespace ToB.Interfaces
{
    public interface IRandomRegistries : IRegistries
    {
        List<Registry> ToRandom(int root);
    }
}