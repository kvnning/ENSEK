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
            //Ensure all readings are new
            if (this.DbContext.MeterReadings.Any(
                x => x.AccountId == item.AccountId 
                && x.MeterReadingDateTime >= item.MeterReadingDateTime))
            {
                return false;
            }
            //Ensure accounts exist
            if (!this.DbContext.Accounts.Any(x => x.AccountId == item.AccountId))
            {
                return false;
            }
            return true;
        }

        public bool Upload(MeterReading item)
        {
            try
            {
                this.DbContext.MeterReadings.Add(item);
                this.DbContext.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}