using System.Collections.Generic;

namespace ToB.PriorityToDo.Projects
{
    public sealed class TreeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TreeDto> Children { get; set; }
    }
}