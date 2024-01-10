using System;

namespace Core.Business.Models.DTO.SwapDTO
{
    public class TreasuryAccountDTO
    {
        public string id { get; set; } // varchar(MAX) -> string
        public Guid treasury_account_holder_id { get; set; } // uniqueidentifier -> Guid
        public int currency { get; set; } // int -> int
        public string account_type { get; set; } // varchar(8) -> string
        public long? added_time { get; set; } // bigint -> long?
        public string? status { get; set; } // varchar(8) -> string?
    }
}
