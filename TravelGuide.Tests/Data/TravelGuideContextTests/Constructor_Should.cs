using NUnit.Framework;
using TravelGuide.Data;

namespace TravelGuide.Tests.Data.TravelGuideContextTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ReturnCorrectInstance_WhenConstructorIsCalled()
        {
            // Arrange & Act
            var context = new TravelGuideContext();

            // Assert
            Assert.IsInstanceOf<TravelGuideContext>(context);
        }
    }
}
