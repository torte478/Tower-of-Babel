using System.Collections.Generic;

using ToB.Common.DB;

#nullable disable

namespace ToB.PriorityToDo.DB
{
    public partial class Project : IHaveId<int>, ICopyable<Project>
    {
        public Project()
        {
            Objectives = new HashSet<Objective>();
        }

        public int Id { get; set; }
        public int Parent { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Objective> Objectives { get; set; }
        
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
