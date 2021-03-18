using System;
using System.Linq;
using NUnit.Framework;

namespace ToB.PriorityToDo.Objectives.Tests
{
    [TestFixture]
    internal sealed class Foo_Should
    {
        private IFoo<int> tree;

        [SetUp]
        public void SetUp()
        {
            tree = new Foo<int>(new Measure(4));
        }

        [Test]
        public void InsertNode_WhenTreeIsEmpty()
        {
            tree.Add(42);
            
            Assert.That(tree.ToPriorityList().Count(), Is.EqualTo(1));
        }

        [Test]
        public void RaiseNodeAddedEvent_AfterValueInsert()
        {
            var raised = false;
            tree.NodeAdded += (_, _) => { raised = true; };

            tree.Add(42);

            Assert.That(raised, Is.True);
        }

        [Test]
        public void RemoveNode_WhenTreeContainsIt()
        {
            tree.Add(42);

            tree.Remove(42);

            Assert.That(tree.ToPriorityList().Count(), Is.EqualTo(0));
        }

        [Test]
        public void RaiseNodeRemovedEvent_AfterValueRemoved()
        {
            var raised = false;
            tree.NodeRemoved += _ => { raised = true; };
            tree.Add(42);

            tree.Remove(42);

            Assert.That(raised, Is.True);
        }

        [Test]
        public void AddNode_WhenChildIsNull()
        {
            tree.Add(42);

            var (_, next) = tree.Add(111);
            var (added, _) = tree.Add(111, next, true);

            Assert.That(added, Is.True);
        }

        [Test]
        public void RaiseActualizeEvent_WhenValueDuplicates()
        {
            var raised = false;
            tree.Rebuilded += _ => { raised = true; }; //TODO : to test utility class
            
            tree.Add(1);
            tree.Add(2, 1, true);
            tree.Add(3, 2, false);
            tree.Add(4, 1, true);

            Assert.That(raised, Is.True);
        }

        [Test]
        public void ThrowException_WhenInsertDuplicate()
        {
            tree.Add(1);

            Assert.Throws<FooException>(() => tree.Add(1));
        }

        [Test]
        public void ThrowException_WhenContinueInsertDuplicate()
        {
            tree.Add(1);

            Assert.Throws<FooException>(() => tree.Add(1, 1, true));
        }

        [Test]
        public void ThrowException_WhenNotContainsTarget()
        {
            Assert.Throws<FooException>(() => tree.Add(1, 1, true));
        }
    }
}