using System.ComponentModel.DataAnnotations;

namespace Snacks.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Informe a senha atual")]
        [Display(Name = "Senha atual")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Informe a nova senha")]
        [Display(Name = "Nova senha")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Informe a confirmação de senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmação de senha")]
        [Compare("NewPassword", ErrorMessage = "A senha e a confirmação de senha devem ser iguais")]
        public string ConfirmNewPassword { get; set; }
    }
}