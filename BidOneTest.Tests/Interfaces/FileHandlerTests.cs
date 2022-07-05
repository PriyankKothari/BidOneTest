using System;
using System.IO;
using System.Threading.Tasks;
using BidOneTest.WebApi.Implementations;
using BidOneTest.WebApi.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BidOneTest.Tests.Interfaces
{
    [TestClass]
    public class FileHandlerTests
    {
        [TestMethod]
        public async Task FileHandler_Verify_WriteToFileAsync_Called()
        {
            // Arrange
            var mockFileHandler = new Mock<IFileHandler>();
            var fileHandler = mockFileHandler.Object;

            // Act
            await fileHandler.WriteFileAsync(It.IsAny<string>(), It.IsAny<string>());

            // Assert
            mockFileHandler.Verify(v => v.WriteFileAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public async Task FileHandler_Verify_WriteToFileAsync_CreatesFile()
        {
            // Arrange
            var directoryPath = Path.GetTempPath();
            var fileName = $"{DateTime.Now:yyyyMMddhhMMss}.txt";
            var fileContent = "some file content";

            var fileHandler = new FileHandler(directoryPath);
            
            // Act
            await fileHandler.WriteFileAsync(fileName, fileContent);

            // Assert
            Assert.IsTrue(File.Exists(Path.Combine(directoryPath, fileName)));
            Assert.AreEqual(fileContent, File.ReadAllText(Path.Combine(directoryPath, fileName)));
        }
    }
}