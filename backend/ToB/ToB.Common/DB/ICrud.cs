using System.Collections.Generic;

namespace ToB.Common.DB
{
    public interface ICrud<TKey, TValue> where TValue : IHaveId<TKey>
    {
        TKey Create(TValue item);
        IEnumerable<TValue> Read();
        TValue Read(TKey id);
        bool Update(TValue item);
        bool Delete(TKey id);
    }
}