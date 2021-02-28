using System.Collections.Generic;

#nullable disable

namespace ToB.PriorityToDo.DB
{
    public partial class Objective
    {
        public Objective()
        {
            ObjectiveProjects = new HashSet<ObjectiveProject>();
        }

        public int Id { get; set; }
        public int Value { get; set; }
        public string Text { get; set; }

        public virtual ICollection<ObjectiveProject> ObjectiveProjects { get; set; }
    }
}
