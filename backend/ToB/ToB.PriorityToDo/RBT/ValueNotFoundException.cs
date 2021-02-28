using System;
using System.Runtime.Serialization;

namespace ToB.PriorityToDo.RBT
{
    [Serializable]
    public class ValueNotFoundException : Exception, ISerializable
    {
        public ValueNotFoundException() : base() { }
        public ValueNotFoundException(string message) : base(message) { }
        public ValueNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
