using System;

namespace ToB.PriorityToDo.Objectives
{
    public sealed class FooException : Exception
    {
        public FooException(string? message) : base(message)
        {
        }
    }
}