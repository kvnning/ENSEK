using System;
using System.Collections.Generic;

namespace ENSEKTest.Models.EFModels
{
    public partial class Account
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}
