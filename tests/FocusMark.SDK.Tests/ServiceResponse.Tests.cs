using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FocusMark.SDK
{
    [TestClass]
    public class ServiceResponseTests
    {
        [TestMethod]
        public void EmptyConstructor_SetsDefaults()
        {
            // Arrange
            var serviceResponse = new ServiceResponse();

            // Assert
            Assert.IsTrue(serviceResponse.IsSuccessful);
            Assert.IsNotNull(serviceResponse.Errors);
        }

        [TestMethod]
        public void ErrorsConstructor_AssignsErrors()
        {
            // Arrange
            var error1 = new ResponseError(1, "error1");
            var error2 = new ResponseError(2, "error2");

            // Act
            var serviceResponse = new ServiceResponse(error1, error2);

            // Assert
            Assert.IsFalse(serviceResponse.IsSuccessful, $"Expected {serviceResponse.IsSuccessful} to be false.");
            Assert.AreEqual(error1, serviceResponse.Errors[0], "Expected first index to be the first error.");
            Assert.AreEqual(error2, serviceResponse.Errors[1], "Expected second index to be the second error.");
        }
    }
}
