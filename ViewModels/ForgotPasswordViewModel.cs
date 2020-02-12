using System.ComponentModel.DataAnnotations;

namespace Snacks.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Informe o e-mail")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "O e-mail está em um formato incorreto")]
        public string Email { get; set; }
    }
}
