using System.Collections.Generic;

namespace ToB.PriorityToDo
{
    public sealed class ObjectiveService : IObjectiveService
    {
        public IEnumerable<(int id, string text)> ToPriorityList(int project)
        {
            throw new System.NotImplementedException();
        }

        public (bool added, int id) StartAdd(int project, string text)
        {
            throw new System.NotImplementedException();
        }

        public string NextForAdd(int project, int id)
        {
            throw new System.NotImplementedException();
        }

        public (bool added, string next) ContinueAdd(int projectId, int id, bool greater)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(int project, int id)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(int project, int id, string text)
        {
            throw new System.NotImplementedException();
        }
    }
}