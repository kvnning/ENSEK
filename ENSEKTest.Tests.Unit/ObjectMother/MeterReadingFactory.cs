using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ENSEKTest.Models.EFModels
{
    public static class MeterReadingFactory
    {
        public static readonly DateTime MeterReadingDateTime = new DateTime(2019,4,22,9,24,0);
        public static readonly int MeterReadValue = 01002;

        public static MeterReading ValidMeterReading() =>
            new MeterReading
            {
                AccountId = AccountFactory.ValidAccountId,
                MeterReadingDateTime = MeterReadingDateTime,
                MeterReadValue = MeterReadValue
            };

        public static MeterReading InvalidMeterReading() =>
            new MeterReading
            {
                AccountId = AccountFactory.InvalidAccountId,
                MeterReadingDateTime = MeterReadingDateTime,
                MeterReadValue = MeterReadValue
            };
    }
}
