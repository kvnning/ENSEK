using ENSEKTest.Models.EFModels;
using Microsoft.AspNetCore.Http;

namespace ENSEKTest.Services
{
    public class CSVParserService : IParserService<IFormFile, MeterReading>
    {

        public IEnumerable<MeterReading> Read(IFormFile source, out int numberOfFailures)
        {
            numberOfFailures = 0;
            var reader = new StreamReader(source.OpenReadStream());
            var result = new List<MeterReading>();

            try
            {
                while (reader.Peek() >= 0)
                {
                    var line = reader.ReadLine();
                    if (this.CanParse(line))
                    {
                        result.Add(this.ParseLine(line));
                    }
                    else
                    {
                        numberOfFailures++;
                    }
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                reader.Dispose();
            }

            return result;
        }

        public MeterReading ParseLine(string data)
        {
            return new MeterReading();
        }

        public bool CanParse(string? data)
        {
            if (data == null)
            {
                return false;
            }
            return true;
        }
    }
}