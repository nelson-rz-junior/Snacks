﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Snacks.Areas.Admin.ViewModels
{
    public class CreateUserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Informe o nome")]
        [Display(Name = "Nome")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Informe o sobrenome")]
        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Informe o e-mail")]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "O e-mail está em um formato incorreto")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Informe a confirmação de senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmação de senha")]
        [Compare("Password", ErrorMessage = "A senha e a confirmação de senha devem ser iguais")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Informe o perfil de usuário")]
        [Display(Name = "Perfil")]
        public string Role { get; set; }

        public List<SelectListItem> RoleItems { get; set; }
    }
}