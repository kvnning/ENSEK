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

        public int ProcessUpload(IFormFile file)
        {
            var readingList = this.ParserService.Read(file, out var numberOfFailures);
            if (readingList.Any())
            {
                foreach(var reading in readingList)
                {
                    if (this.UploadService.CanUpload(reading))
                    {
                        this.UploadService.Upload(reading);
                    }
                    else
                    {
                        numberOfFailures++;
                    }
                }
            }
            return numberOfFailures;
        }
    }
}