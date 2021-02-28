using System.Collections.Generic;
using ToB.WebApi.DB;

namespace ToB.WebApi.Interfaces
{
    public interface IRandomRegistries : IRegistries
    {
        List<Registry> ToRandom(int root);
    }
}