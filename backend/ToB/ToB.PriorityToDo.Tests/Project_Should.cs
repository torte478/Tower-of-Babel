using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using ToB.Common.DB;
using ToB.PriorityToDo.Contracts;
using ToB.PriorityToDo.DB;

namespace ToB.PriorityToDo.Objectives.Tests
{
    [TestFixture]
    internal sealed class Project_Should
    {
        [Test]
        public void SortByDescending_WhenBuildPriorityList()
        {
            var storage = A.Fake<ICrud<int, Objective>>();
            A.CallTo(() => storage.List()).Returns(new[]
            {
                new Objective
                {
                    Text = "min",
                    Value = 0
                },
                new Objective
                {
                    Text = "max",
                    Value = 99
                }
            });
            var tree = A.Fake<IBalancedTree<int>>();
            var project = Project.Create(0, storage, () => tree);

            var actual = project.ToPriorityList().First().text;

            Assert.That(actual, Is.EqualTo("max"));
        }
    }
}