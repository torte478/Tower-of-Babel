using System.Collections.Generic;

using ToB.DB;

namespace ToB.Interfaces
{
    public interface IRegistries
    {
        List<Registry> ToAll(int root);
        List<Registry> ToRandom(int root);
        IRegistries Delete(int id);
        IRegistries Add(int? parent, string label);
    }
}