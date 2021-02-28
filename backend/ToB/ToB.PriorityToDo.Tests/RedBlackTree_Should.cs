using NUnit.Framework;

namespace ToB.PriorityToDo.Tests
{
    [TestFixture]
    internal sealed class RedBlackTree_Should
    {
        [Test]
        public void ProvideOrder_AfterAdding()
        {
            var tree = new RedBlackTree<int>();
            
            foreach (var x in new[] {5, 4, 3, 2, 1})
                tree.Add(x);
            
            Assert.That(tree.ToOrdered(), Is.EquivalentTo(new[] { 1, 2, 3, 4, 5}));
        }
    }
}