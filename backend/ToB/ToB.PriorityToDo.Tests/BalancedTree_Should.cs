using System.Linq;

using NUnit.Framework;

using ToB.PriorityToDo.Contracts;

namespace ToB.PriorityToDo.Objectives.Tests
{
    [TestFixture]
    internal sealed class BalancedTree_Should
    {
        private IBalancedTree<int> tree;

        [SetUp]
        public void SetUp()
        {
            tree = new BalancedTree<int>(new Measure(4));
        }

        [Test]
        public void InsertNode_WhenTreeIsEmpty()
        {
            tree.Add(42, true, 1);
            
            Assert.That(tree.ToPriorityList().Count(), Is.EqualTo(1));
        }

        [Test]
        public void RemoveNode_WhenTreeContainsIt()
        {
            tree.Add(42, 10);

            tree.Remove(42);

            Assert.That(tree.ToPriorityList().Count(), Is.EqualTo(0));
        }

        [Test]
        public void ReturnTrue_WhenCheckAddingToNodeWithNullChild()
        {
            tree.Add(42, 10);

            var (can, _) = tree.CanAdd(42, true);

            Assert.That(can, Is.True);
        }

        [Test]
        public void RaiseActualizeEvent_WhenValueDuplicates()
        {
            var raised = false;
            tree.Rebuilded += _ => { raised = true; }; //TODO : to test utility class
            
            tree.Add(666, true, 1);
            tree.Add(1, true, 2);
            tree.Add(2, false, 3);
            tree.Add(3, true, 4);

            Assert.That(raised, Is.True);
        }
    }
}