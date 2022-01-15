using ENSEKTest.Models;
using ENSEKTest.Models.EFModels;
using Microsoft.AspNetCore.Http;

namespace ENSEKTest.Services
{
    public class MeterReadingUploadService : IMeterReadingUploadService
    {
        private IParserService<IFormFile, MeterReading> ParserService { get; set; }
        private IUploadService<MeterReading> UploadService { get; set; }

        public MeterReadingUploadService(IParserService<IFormFile, MeterReading> parserService, IUploadService<MeterReading> uploadService)
        {
            this.ParserService = parserService;
            this.UploadService = uploadService;
        }

        public MeterReadingUploadResult ProcessUpload(IFormFile file)
        {
            var numberOfSucesses = 0;
            var readingList = this.ParserService.Read(file, out var numberOfFailures);
            if (readingList.Any())
            {
                foreach(var reading in readingList)
                {
                    if (this.UploadService.CanUpload(reading))
                    {
                        if (this.UploadService.Upload(reading)) {
                            numberOfSucesses++;
                        }
                    }
                    else
                    {
                        numberOfFailures++;
                    }
                }
            }

            return new MeterReadingUploadResult
            {
                NumberOfFailures = numberOfFailures,
                NumberOfSuccesses = numberOfSucesses
            };
        }
    }
}