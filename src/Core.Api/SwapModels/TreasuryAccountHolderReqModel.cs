using System.ComponentModel.DataAnnotations;

namespace Core.Api.SwapModels
{
    public class TreasuryAccountHolderRequest
    {
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "The unique identifier of the first Balance Account created for the Treasury Account.")]
        public string balance_account_id { get; set; }

        [Required]
        [StringLength(maximumLength: 3, ErrorMessage = "The currency of the account. Only BRL is available.")]
        public string currency { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "The CNPJ of the Treasury Account Holder (only numbers).")]
        public string document { get; set; }

        [Required]
        public string legal_name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "The phone number of the Treasury Account Holder with country code and area code (only numbers), for example: 5511999991111")]
        public string phone_number { get; set; }


        public Addresses address { get; set; }
    }

    public class Addresses
    {
        [Required]
        public string street { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Only numeric values are allowed.")]
        public string number { get; set; }


        public string complement { get; set; }

        [Required]
        public string neighborhood { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "The postal code of the address. Only numbers. For example: 1307610")]
        public string postal_code { get; set; }

        [Required]
        public string city { get; set; }

        [Required]
        [StringLength(maximumLength: 2, ErrorMessage = "The two characters abbreviation of the name of the state. For example: SP for São Paulo.")]
        public string state { get; set; }
    }
}
