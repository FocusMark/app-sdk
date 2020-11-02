using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FocusMark.SDK
{
    [TestClass]
    public class ResponseErrorTests
    {
        [TestMethod]
        public void Constructor_AssignsValues()
        {
            // Arrange
            int code = 1234;
            string message = "Error message";

            // Act
            var error = new ResponseError(code, message);

            // Assert
            Assert.AreEqual(code, error.Code, "Expected the error code to be assigned.");
            Assert.AreEqual(message, error.Message, "Expected the error message to be assigned.");
        }
    }
}
