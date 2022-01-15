using ENSEKTest.Models.EFModels;
using ENSEKTest.Services;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace ENSEKTest.Tests.Unit
{
    [TestClass]
    public class DatabaseUploadServiceTests
    {
        [TestMethod]
        public void CanUpload_WithValidData_ReturnsTrue()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ENSEKContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString())
                  .Options;
            var dbContext = new ENSEKContext(options);
            dbContext.Accounts.Add(AccountFactory.ValidAccount());
            var service = new DatabaseUploadService(dbContext);
            var data = MeterReadingFactory.ValidMeterReading();

            //Act
            var result = service.CanUpload(data);

            //Assert
            Assert.IsTrue(result);
        }

        [DataTestMethod]
        public void CanUpload_WithInvalidData_ReturnsFalse()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ENSEKContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString())
                  .Options;
            var dbContext = new ENSEKContext(options);
            dbContext.Accounts.Add(AccountFactory.ValidAccount());
            var service = new DatabaseUploadService(dbContext);
            var data = MeterReadingFactory.InvalidMeterReading();

            //Act
            var result = service.CanUpload(data);

            //Assert
            Assert.IsFalse(result);
        }
    }
}