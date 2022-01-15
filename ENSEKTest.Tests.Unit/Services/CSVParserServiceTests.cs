using ENSEKTest.Services;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace ENSEKTest.Tests.Unit
{
    [TestClass]
    public class CSVParserServiceTests
    {
        [TestMethod]
        public void Read_WithValidData_ReturnsPopulatedList()
        {
            //Arrange
            var service = new CSVParserService();
            var content = @"AccountId,MeterReadingDateTime,MeterReadValue
2344,22 / 04 / 2019 09:24,01002
2233,22 / 04 / 2019 12:25,00323
8766,22 / 04 / 2019 12:25,03440";
            var bytes = Encoding.UTF8.GetBytes(content);
            var data = new FormFile(new MemoryStream(bytes), 0, content.Length, "Data", "test.csv");

            //Act
            int numberOfFailures;
            var result = service.Read(data, out numberOfFailures);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2344, result.First().AccountId);
            Assert.AreEqual(new DateTime(2019, 4, 22, 9, 24, 0), result.First().MeterReadingDateTime);
            Assert.AreEqual(1002, result.First().MeterReadValue);
            Assert.AreEqual(8766, result.Last().AccountId);
            Assert.AreEqual(new DateTime(2019, 4, 22, 12, 25, 0), result.Last().MeterReadingDateTime);
            Assert.AreEqual(3440, result.Last().MeterReadValue);
            Assert.AreEqual(0, numberOfFailures);
        }

        [TestMethod]
        public void Read_WithInvalidData_ReturnsEmptyListAndNumberOfErrors()
        {
            //Arrange
            var service = new CSVParserService();
            var content = @"AccountId,MeterReadingDateTime,MeterReadValue
2349,22/04/2019 12:25,VOID
2344,08/05/2019 09:24,0X765
1235,13/05/2019 09:24,";
            var bytes = Encoding.UTF8.GetBytes(content);
            var data = new FormFile(new MemoryStream(bytes), 0, content.Length, "Data", "test.csv");

            //Act
            int numberOfFailures;
            var result = service.Read(data, out numberOfFailures);

            //Assert
            Assert.AreEqual(0, result.Count());
            Assert.AreEqual(3, numberOfFailures);
        }

        [TestMethod]
        public void Read_WithValidAndInvalidData_ReturnsPartiallyPopulatedListAndNumberOfErrors()
        {
            //Arrange
            var service = new CSVParserService();
            var content = @"AccountId,MeterReadingDateTime,MeterReadValue
2349,22/04/2019 12:25,VOID
1234,12/05/2019 09:24,09787
1235,13/05/2019 09:24,";
            var bytes = Encoding.UTF8.GetBytes(content);
            var data = new FormFile(new MemoryStream(bytes), 0, content.Length, "Data", "test.csv");

            //Act
            int numberOfFailures;
            var result = service.Read(data, out numberOfFailures);

            //Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(1234, result.First().AccountId);
            Assert.AreEqual(new DateTime(2019, 5, 12, 9, 24, 0), result.First().MeterReadingDateTime);
            Assert.AreEqual(9787, result.First().MeterReadValue);
            Assert.AreEqual(2, numberOfFailures);
        }

        [TestMethod]
        public void Read_WithExtraData_IgnoresExtraData()
        {
            //Arrange
            var service = new CSVParserService();
            var content = @"AccountId,MeterReadingDateTime,MeterReadValue
1241,11/04/2019 09:24,00436,X";
            var bytes = Encoding.UTF8.GetBytes(content);
            var data = new FormFile(new MemoryStream(bytes), 0, content.Length, "Data", "test.csv");

            //Act
            int numberOfFailures;
            var result = service.Read(data, out numberOfFailures);

            //Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(1241, result.First().AccountId);
            Assert.AreEqual(new DateTime(2019, 4, 11, 9, 24, 0), result.First().MeterReadingDateTime);
            Assert.AreEqual(436, result.First().MeterReadValue);
            Assert.AreEqual(0, numberOfFailures);
        }
    }
}