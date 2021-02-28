using System.Collections.Generic;
using ToB.WebApi.DB;

namespace ToB.WebApi.Interfaces
{
    public interface IRegistries
    {
        List<Registry> ToAll(int root);
        IRegistries Delete(int id);
        IRegistries Add(int? parent, string label);
    }
}