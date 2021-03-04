using System.Collections.Generic;

namespace ToB.PriorityToDo
{
    public sealed class ProjectTreeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProjectTreeDto> Children { get; set; }
    }
}