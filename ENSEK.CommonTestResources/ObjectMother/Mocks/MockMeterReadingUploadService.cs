using ENSEKTest.Models;
using ENSEKTest.Services;
using Microsoft.AspNetCore.Http;

namespace ENSEKTest.CommonTestResources
{
    public class MockMeterReadingUploadService : IMeterReadingUploadService
    {
        public int NumberOfFailures { get; set; }
        public int NumberOfSuccesses { get; set; }

        public MockMeterReadingUploadService(int numberOfFailures, int numberOfSuccesses)
        {
            this.NumberOfFailures = numberOfFailures;
            this.NumberOfSuccesses = numberOfSuccesses;
        }

        public MeterReadingUploadResult ProcessUpload(IFormFile file)
        {
            return new MeterReadingUploadResult
            {
                NumberOfFailures = this.NumberOfFailures,
                NumberOfSuccesses = this.NumberOfSuccesses,
            };
        }
    }
}
