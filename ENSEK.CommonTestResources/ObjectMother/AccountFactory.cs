using ENSEKTest.Models.EFModels;

namespace ENSEKTest.CommonTestResources
{
    public static class AccountFactory
    {
        public static readonly int ValidAccountId = 2344;
        public static readonly int InvalidAccountId = 1;
        public static readonly string FirstName = "Tommy";
        public static readonly string LastName = "Test";

        public static Account ValidAccount() =>
            new Account
            {
                AccountId = ValidAccountId,
                FirstName = FirstName,
                LastName = LastName
            };
    }
}
