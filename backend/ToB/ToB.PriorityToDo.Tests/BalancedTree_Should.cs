using NUnit.Framework;
using ToB.PriorityToDo.Objectives.RBT;

namespace ToB.PriorityToDo.Objectives.Tests
{
    [TestFixture]
    internal sealed class BalancedTree_Should
    {
        [Test]
        public void GoToRightChild_WhenCompareResultIsPositive()
        {
            var tree = new BalancedTree<int>(node => new Node<int>(node));
            tree.Add(10);
            tree.Add(20);
            
            Assert.That(tree.ToNode(20).Value, Is.EqualTo(20));
        }

        [Test]
        public void ReturnRootAsPreviousForMax_WhenTreeHasTwoElements()
        {
            var tree = new BalancedTree<int>(_ => new Node<int>(_));
            tree.Add(10);
            tree.Add(20);

            var node = new Node<int>(new RedBlackNode<int>(20));
            var actual = tree.FindPrevious(node);

            Assert.That(actual, Is.EqualTo(10));
        }
    }
}