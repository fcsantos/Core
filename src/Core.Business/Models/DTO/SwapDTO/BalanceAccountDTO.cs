namespace Core.Business.Models.DTO.SwapDTO
{
    public class BalanceAccountDTO
    {
        public string id { get; set; } // varchar(MAX) -> string
        public string treasury_account_id { get; set; } // varchar(MAX) -> string
        public int currency { get; set; } // int -> int
        public int balance { get; set; } // int -> int
    }
}
