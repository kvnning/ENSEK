using CsvHelper;
using CsvHelper.Configuration;
using ENSEKTest.Models;
using ENSEKTest.Models.EFModels;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace ENSEKTest.Services
{
    public class CSVParserService : IParserService<IFormFile, MeterReading>
    {
        public IEnumerable<MeterReading> Read(IFormFile source, out int numberOfFailures)
        {
            numberOfFailures = 0;
            var results = new List<MeterReading>();
            using (var reader = new StreamReader(source.OpenReadStream()))
            {
                using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
                {
                    csv.Read();
                    csv.ReadHeader();

                    while (csv.Read())
                    {
                        MeterReading reading = null;
                        try
                        {
                            var accountId = csv.GetField<int>("AccountId");
                            var meterReadingDateTime = csv.GetField<DateTime>("MeterReadingDateTime");
                            var meterReadValue = csv.GetField<string>("MeterReadValue");

                            if (this.CanParse(accountId, meterReadingDateTime, meterReadValue))
                            {
                                reading = new MeterReading
                                {
                                    AccountId = accountId,
                                    MeterReadingDateTime = meterReadingDateTime,
                                    MeterReadValue = int.Parse(meterReadValue)
                                };
                            }
                            else
                            {
                                numberOfFailures++;
                            }
                        }
                        catch (Exception e)
                        {
                            numberOfFailures++;
                        }

                        if (reading != null)
                        {
                            results.Add(reading);
                        }
                    }
                };
            };

            return results;
        }

        public bool CanParse(int accountId, DateTime meterReadingDateTime, string meterReadValue)
        {
            if (meterReadValue.Length != 5)
            {
                return false;
            }
            return true;
        }
    }
}