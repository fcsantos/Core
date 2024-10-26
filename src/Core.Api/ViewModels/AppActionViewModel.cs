﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Api.ViewModels
{
    public class AppActionViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid ControllerId { get; set; }
        public string ControllerName { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Nome da Action")]
        public string ActionName { get; set; }

    }

}
