using NUnit.Framework;
using TravelGuide.Data;

namespace TravelGuide.Tests.Data.TravelGuideContextTests
{
    [TestFixture]
    public class Create_Should
    {
        [Test]
        public void ReturnCorrectInstance_WhenMethodIsCalled()
        {
            // Arrange & Act
            var context = TravelGuideContext.Create();

            // Assert
            Assert.IsInstanceOf<TravelGuideContext>(context);
        }
    }
}
