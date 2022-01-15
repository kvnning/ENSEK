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
    public class MeterReadingUploadServiceTests
    {
        [TestMethod]
        public void Read_WithValidData_ReturnsPopulatedList()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ENSEKContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString())
                  .Options;
            var dbContext = new ENSEKContext(options);
            dbContext.Accounts.Add(AccountFactory.ValidAccount());
            var uploadService = new DatabaseUploadService(dbContext);
            var service = new MeterReadingUploadService(new CSVParserService(), uploadService);
            var content = @"AccountId,MeterReadingDateTime,MeterReadValue
2344,22 / 04 / 2019 09:24,01002
2233,22 / 04 / 2019 12:25,00323
8766,22 / 04 / 2019 12:25,03440";
            var bytes = Encoding.UTF8.GetBytes(content);
            var data = new FormFile(new MemoryStream(bytes), 0, content.Length, "Data", "test.csv");

            //Act
            var result = service.ProcessUpload(data);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result);
        }
    }
}