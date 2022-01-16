using CsvHelper;
using CsvHelper.Configuration;
using ENSEKTest.Models;
using ENSEKTest.Models.EFModels;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace ENSEKTest.Services
{
    /// <summary>
    /// Helper service for parsing CSV FormFiles into a Enumerable of MeterReadings.
    /// </summary>
    public class CSVParserService : IParserService<IFormFile, IEnumerable<MeterReading>>
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

                            if (this.CanParse(meterReadValue))
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

        /// <summary>
        /// Checks if property is valid for parsing.
        /// </summary>
        /// <param name="meterReadValue"></param>
        /// <returns></returns>
        private bool CanParse(string meterReadValue)
        {
            if (meterReadValue.Length != 5)
            {
                return false;
            }
            return true;
        }
    }
}