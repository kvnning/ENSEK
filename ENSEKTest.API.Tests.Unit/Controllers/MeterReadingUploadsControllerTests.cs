using ENSEKTest.API.Controllers;
using ENSEKTest.CommonTestResources;
using ENSEKTest.Models.EFModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace ENSEKTest.API.Tests.Unit
{
    [TestClass]
    public class MeterReadingUploadsControllerTests
    {
        [TestMethod]
        public void Post_Always_ReturnsOkObjectResult()
        {
            //Arrange
            var controller = new MeterReadingUploadsController(new MockMeterReadingUploadService(1,2));

            //Act
            var result = controller.Post(new FormFile(null, 0, 0, null, null));

            //Assert
            Assert.AreEqual(typeof(OkObjectResult), result.GetType());
            Assert.AreEqual("2 rows successfully processed, 1 rows failed.", ((OkObjectResult)result).Value);
        }
    }
}