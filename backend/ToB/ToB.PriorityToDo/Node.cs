#nullable enable
using System;

namespace ToB.PriorityToDo
{
    public class Node : IComparable<Node>
    {
        public int Id { get; }
        public int Project { get; }
        public int Value { get; }
        
        public Node(int id, int project, int value)
        {
            Id = id;
            Project = project;
            Value = value;
        }
        
        public int CompareTo(Node? other)
        {
            return Value.CompareTo(other?.Value ?? int.MinValue);
        }
    }
}