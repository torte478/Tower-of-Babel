#nullable enable

using System;
using ToB.Common.DB;

namespace ToB.PriorityToDo.DB
{
    public partial class Objective : IHaveId<int>, ICopyable<Objective>, IComparable<Objective>
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public string Text { get; set; }
        public int Project { get; set; }

        public virtual Project ProjectNavigation { get; set; }
        
        public void Copy(Objective other)
        {
            if (other == null)
                return;

            if (other.Value != default)
                Value = other.Value;

            if (other.Text != default)
                Text = other.Text;

            if (other.Project != default)
                Project = other.Project;
        }
        
        public int CompareTo(Objective? other)
        {
            if (other == null)
                return -1;

            return Value.CompareTo(other.Value);
        }

        public override string ToString()
        {
            return $"{Id} - {Value} - {Text}";
        }
    }
}
