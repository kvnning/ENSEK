using ENSEKTest.CommonTestResources;
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
            dbContext.SaveChanges();
            var service = new DatabaseUploadService(dbContext);
            var data = MeterReadingFactory.ValidMeterReading();

            //Act
            var result = service.CanUpload(data);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanUpload_WithoutExistingAccount_ReturnsFalse()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ENSEKContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString())
                  .Options;
            var dbContext = new ENSEKContext(options);
            var service = new DatabaseUploadService(dbContext);
            var data = MeterReadingFactory.InvalidMeterReading();

            //Act
            var result = service.CanUpload(data);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CanUpload_WithExistingRecord_ReturnsFalse()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ENSEKContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString())
                  .Options;
            var dbContext = new ENSEKContext(options);
            dbContext.Accounts.Add(AccountFactory.ValidAccount());
            dbContext.MeterReadings.Add(MeterReadingFactory.ValidMeterReading());
            dbContext.SaveChanges();
            var service = new DatabaseUploadService(dbContext);
            var data = MeterReadingFactory.ValidMeterReading();

            //Act
            var result = service.CanUpload(data);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CanUpload_WithExistingNewerRecord_ReturnsFalse()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ENSEKContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString())
                  .Options;
            var dbContext = new ENSEKContext(options);
            dbContext.Accounts.Add(AccountFactory.ValidAccount());
            var reading = MeterReadingFactory.ValidMeterReading();
            reading.MeterReadingDateTime = reading.MeterReadingDateTime.AddDays(1);
            dbContext.MeterReadings.Add(reading);
            dbContext.SaveChanges();
            var service = new DatabaseUploadService(dbContext);
            var data = MeterReadingFactory.ValidMeterReading();

            //Act
            var result = service.CanUpload(data);

            //Assert
            Assert.IsFalse(result);
        }
    }
}