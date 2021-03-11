using FakeItEasy;
using NUnit.Framework;

namespace ToB.PriorityToDo.Objectives.Tests
{
    [TestFixture]
    internal sealed class Service_Should
    {
        [Test]
        public void ReturnIterationId_FromGeneratedSequence()
        {
            var project = A.Fake<IProject>();
            A.CallTo(() => project.StartAdd(A<string>._)).Returns((true, 99));
            
            var projects = A.Fake<IProjects>();
            A.CallTo(() => projects[12]).Returns(project);
            var service = new Service(A.Fake<IProjects>(), () => 42);

            var (_, id) = service.StartAdd(12, string.Empty);

            Assert.That(id, Is.EqualTo(42));
        }
    }
}