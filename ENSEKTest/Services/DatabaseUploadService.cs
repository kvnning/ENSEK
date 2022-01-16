using ENSEKTest.Models.EFModels;
using Microsoft.AspNetCore.Http;

namespace ENSEKTest.Services
{
    /// <summary>
    /// Service for uploading MeterReadings to a database. Implementation of IUploadService.
    /// </summary>
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

        /// <summary>
        /// Attempts to upload a MeterReading to the database. Returns true when successful.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
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