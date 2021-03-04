using System.Collections.Generic;

using ToB.Common.DB;

#nullable disable

namespace ToB.PriorityToDo.DB
{
    public partial class Objective : IHaveId<int>, ICopyable<Objective>
    {
        public Objective()
        {
            ObjectiveProjects = new HashSet<ObjectiveProject>();
        }

        public int Id { get; set; }
        public int Value { get; set; }
        public string Text { get; set; }

        public virtual ICollection<ObjectiveProject> ObjectiveProjects { get; set; }
        
        public void Copy(Objective other)
        {
            if (other == null)
                return;

            if (other.Value != default)
                Value = other.Value;

            if (other.Text != default)
                Text = other.Text;
        }
    }
}
