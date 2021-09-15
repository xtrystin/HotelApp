using HAWebUI.Helpers;
using HAWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HAWebUI.Tests
{
    public class ErrorCreatorTests
    {
        [Theory]
        [InlineData("Forbidden", "You do not have permission to access this page")]
        [InlineData("Unauthorized", "You are not authorized. Please try sign out, sign in and try again.")]
        [InlineData("aaa123", "Fatal Exception")]
        public void CreateApiError_ShouldWorkIfNotNull(string message, string expectedMessage)
        {
            // Arrange
            Exception ex = new Exception(message);

            // Act
            var actual = ErrorCreator.CreateApiError(ex);

            // Assert
            Assert.NotNull(actual);
            Assert.True(actual.Title == ex.Message);
            Assert.True(actual.Message == expectedMessage);
            Assert.True(actual.ShowError == true);
        }

        [Fact]
        public void CreateApiError_ShouldWorkIfNull()
        {
            // Arrange
            Exception ex = null;

            // Act
            var actual = ErrorCreator.CreateApiError(ex);

            // Assert
            Assert.NotNull(actual);
            Assert.True(actual.Title == "Exception is null");
            Assert.True(actual.Message == "Fatal Exception");
            Assert.True(actual.ShowError == true);
        }
    }
}
