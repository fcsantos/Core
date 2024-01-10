using System;

namespace Core.Business.Models.DTO.SwapDTO
{
    public class TreasuryAccountHolderDTO
    {
        public Guid id { get; set; } // uniqueidentifier -> Guid
        public long? added_time { get; set; } // bigint -> long?
        public string balance_account_id { get; set; } // varchar(MAX) -> string
        public string currency { get; set; } // varchar(3) -> string
        public string document { get; set; } // varchar(14) -> string
        public string legal_name { get; set; } // varchar(MAX) -> string
        public string email { get; set; } // varchar(MAX) -> string
        public string? phone_number { get; set; } // varchar(13) -> string?
        public string? status { get; set; } // varchar(8) -> string?
        public string street { get; set; } // varchar(MAX) -> string
        public string number { get; set; } // varchar(MAX) -> string
        public string? complement { get; set; } // varchar(MAX) -> string?
        public string neighborhood { get; set; } // varchar(MAX) -> string
        public string postal_code { get; set; } // varchar(10) -> string
        public string city { get; set; } // varchar(MAX) -> string
        public string state { get; set; } // varchar(2) -> string
        public string? country { get; set; } // varchar(MAX) -> string?
        public string? address_type { get; set; } // varchar(MAX) -> string?
    }
}
