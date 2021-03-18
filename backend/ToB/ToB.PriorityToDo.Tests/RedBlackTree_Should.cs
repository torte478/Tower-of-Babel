using System.Linq;

using NUnit.Framework;

namespace ToB.PriorityToDo.Objectives.RBT.Tests
{
    [TestFixture]
    internal sealed class RedBlackTree_Should
    {
        private RedBlackTree<int> tree;

        [SetUp]
        public void SetUp()
        {
            tree = new RedBlackTree<int>();
        }

        [Test]
        public void ProvideOrder_AfterAdding()
        {
            foreach (var x in new[] {5, 4, 3, 2, 1})
                tree.Add(x);

            Assert.That(tree.ToList(), Is.EquivalentTo(new[] {1, 2, 3, 4, 5}));
        }

        [Test]
        public void ProvideOrder_AfterRemoving()
        {
            foreach (var x in new[] {1, 2, 3, 4, 5})
                tree.Add(x);

            foreach (var x in new[] {4, 2, 3})
                tree.Remove(x);

            Assert.That(tree.ToList(), Is.EquivalentTo(new[] {1, 5}));
        }
    }
}