using System.Collections.Generic;

using ToB.Common.DB;

#nullable disable

namespace ToB.PriorityToDo.DB
{
    public partial class Project : IHaveId<int>, ICopyable<Project>
    {
        public Project()
        {
            ObjectiveProjects = new HashSet<ObjectiveProject>();
        }

        public int Id { get; set; }
        public int Parent { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ObjectiveProject> ObjectiveProjects { get; set; }
        
        public void Copy(Project other)
        {
            if (other == null)
                return;

            if (other.Parent != default)
                Parent = other.Parent;

            if (other.Name != default)
                Name = other.Name;
        }
    }
}
