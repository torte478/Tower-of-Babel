using NUnit.Framework;
using ToB.PriorityToDo.Objectives.RBT;

namespace ToB.PriorityToDo.Objectives.Tests
{
    [TestFixture]
    internal sealed class Node_Should
    {
        [Test]
        public void ReturnNullAsChild_WhenOriginChildIsNull()
        {
            var origin = new RedBlackNode<int>(42);
            var node = new Node<int>(origin);

            Assert.That(node.Right, Is.Null);
        }
    }
}