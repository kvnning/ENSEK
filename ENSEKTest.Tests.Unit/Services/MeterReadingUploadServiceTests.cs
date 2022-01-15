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
            dbContext.SaveChanges();
            var uploadService = new DatabaseUploadService(dbContext);
            var service = new MeterReadingUploadService(new CSVParserService(), uploadService);
            var content = @"AccountId,MeterReadingDateTime,MeterReadValue
2344,22/04/2019 12:25,45522
2344,23/04/2019 12:25,999999
2344,24/04/2019 12:25,00054
2344,25/04/2019 12:25,00123
2344,26/04/2019 12:25,VOID";
            var bytes = Encoding.UTF8.GetBytes(content);
            var data = new FormFile(new MemoryStream(bytes), 0, content.Length, "Data", "test.csv");

            //Act
            var result = service.ProcessUpload(data);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.NumberOfSuccesses);
            Assert.AreEqual(2, result.NumberOfFailures);
        }
    }
}