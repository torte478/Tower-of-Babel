using System.Collections.Generic;
using ToB.Common;
using ToB.Common.DB;

#nullable disable

namespace ToB.PriorityToDo.DB
{
    public partial class Project : IHaveId<int>
    {
        public Project()
        {
            ObjectiveProjects = new HashSet<ObjectiveProject>();
        }

        public int Id { get; set; }
        public int Parent { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ObjectiveProject> ObjectiveProjects { get; set; }
    }
}
