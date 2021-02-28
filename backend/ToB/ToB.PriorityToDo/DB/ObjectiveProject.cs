#nullable disable

namespace ToB.PriorityToDo.DB
{
    public partial class ObjectiveProject
    {
        public int Id { get; set; }
        public int Objective { get; set; }
        public int Project { get; set; }

        public virtual Objective ObjectiveNavigation { get; set; }
        public virtual Project ProjectNavigation { get; set; }
    }
}
