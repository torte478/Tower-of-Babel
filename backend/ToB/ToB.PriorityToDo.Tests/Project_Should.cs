using FakeItEasy;
using NUnit.Framework;
using ToB.Common.DB;
using ToB.PriorityToDo.DB;

namespace ToB.PriorityToDo.Objectives.Tests
{
    [TestFixture]
    internal sealed class Project_Should
    {
        [Test]
        public void RebuildTree_AfterElementRemove()
        {
            var tree = A.Fake<IBalancedTree<Objective>>();
            var storage = A.Fake<ICrud<int, Objective>>();
            A.CallTo(() => storage.Delete(A<int>._)).Returns(true);
            var project = Project.Create(
                1, 
                A.Fake<IMeasure>(), 
                storage,
                () => tree);

            project.Remove(42);

            A.CallTo(() => tree.Clear()).MustHaveHappened();
        }
    }
}