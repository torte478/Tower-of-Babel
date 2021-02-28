using NUnit.Framework;

namespace ToB.PriorityToDo.Tests
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
        public void RotateTree_OnTwoRedNodes()
        {
            tree.Add(1).Add(2).Add(3);
            
            Assert.That(tree.Root.Left, Is.Not.EqualTo(RedBlackTree<int>.Nil));
        }

        [Test]
        public void ProvideOrder_AfterAdding()
        {
            foreach (var x in new[] {5, 4, 3, 2, 1})
                tree.Add(x);

            Assert.That(tree.ToOrdered(), Is.EquivalentTo(new[] {1, 2, 3, 4, 5}));
        }

        [Test]
        public void ProvideOrder_AfterRemoving()
        {
            foreach (var x in new[] {1, 2, 3, 4, 5})
                tree.Add(x);

            foreach (var x in new[] {4, 2, 3})
                tree.Remove(x);

            Assert.That(tree.ToOrdered(), Is.EquivalentTo(new[] {1, 5}));
        }
    }
}