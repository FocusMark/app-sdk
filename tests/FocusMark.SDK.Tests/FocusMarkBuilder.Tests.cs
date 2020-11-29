using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FocusMark.SDK
{
    [TestClass]
    public class FocusMarkBuilderTests
    {
        [TestMethod]
        public void Constructor_SetsCollection()
        {
            // Arrange
            IServiceCollection services = new ServiceCollection();
            
            // Act
            var builder = new FocusMarkBuilder(services);

            // Assert
            Assert.AreEqual(services, builder.ServiceRegistery);
        }
    }
}
