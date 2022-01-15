using System;
using System.Collections.Generic;

namespace ENSEKTest.Models.EFModels
{
    public partial class MeterReading
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public DateTime MeterReadingDateTime { get; set; }
        public int MeterReadValue { get; set; }
    }
}
