using ENSEKTest.Models.EFModels;
using Microsoft.AspNetCore.Http;

namespace ENSEKTest.Services
{
    public class DatabaseUploadService : IUploadService<MeterReading>
    {
        private ENSEKContext DbContext { get; set; }

        public DatabaseUploadService(ENSEKContext context)
        {
            this.DbContext = context;
        }

        public bool CanUpload(MeterReading item)
        {
            throw new NotImplementedException();
        }

        public bool Upload(MeterReading item)
        {
            throw new NotImplementedException();
        }
    }
}