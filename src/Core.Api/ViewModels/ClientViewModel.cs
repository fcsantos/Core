using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Api.ViewModels
{
    public class ClientViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(250, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(14, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 9)]
        public string NIF { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string UserId { get; set; }

        public bool? IsActive { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "E-Mail em formato inválido.")]
        public string Email { get; set; }


        [ScaffoldColumn(false)]
        public string Active { get; set; }
    }
}