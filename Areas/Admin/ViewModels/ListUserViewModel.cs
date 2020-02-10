using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Snacks.Areas.Admin.ViewModels
{
    public class ListUserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Informe o usuário")]
        [Display(Name = "Usuário")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Informe o e-mail")]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "O e-mail está em um formato incorreto")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe o perfil de usuário")]
        [Display(Name = "Perfil")]
        public string Role { get; set; }

        public List<SelectListItem> RoleItems { get; set; }
    }
}
