using System.Collections.Generic;

namespace Core.Api.SwapModels
{
    public class TreasuryAccountHolderResponse
    {
        public TreasuryAccountHolder treasury_account_holder { get; set; }
        public List<TreasuryAccount> treasury_accounts { get; set; }
    }

    public class TreasuryAccount
    {
        public string account_type { get; set; }
        public long added_time { get; set; }
        public List<BalanceAccount> balance_accounts { get; set; }
        public int currency { get; set; }
        public string id { get; set; }
        public string status { get; set; }
    }

    public class TreasuryAccountHolder
    {
        public long added_time { get; set; }
        public AddressesTAH addresses { get; set; }
        public string email { get; set; }
        public string id { get; set; }
        public string legal_name { get; set; }
        public string phone_number { get; set; }
        public string status { get; set; }
    }

    public class AddressesTAH
    {
        public string street { get; set; }
        public string number { get; set; }
        public string complement { get; set; }
        public string neighborhood { get; set; }
        public string postal_code { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string address_type { get; set; }
    }

    public class BalanceAccount
    {
        public int balance { get; set; }
        public int currency { get; set; }
        public string id { get; set; }
    }
}
