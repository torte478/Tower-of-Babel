using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ToB.PriorityToDo.Objectives.Tests
{
    [TestFixture]
    internal sealed class Foo_Should
    {
        private IFoo<int> tree = null;

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
            tree.NodeAdded += (_, __) => { raised = true; };

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
        

        private interface IFoo<T>
        {
            event Action<T, int> NodeAdded;
            event Action<T> NodeRemoved;
            
            (bool added, T next) Add(T value);
            (bool added, T next) Add(T value, T target, bool greater);
            bool Remove(T value);
            IEnumerable<T> ToPriorityList();
        }
    }
}