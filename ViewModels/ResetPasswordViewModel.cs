using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Snacks.ViewModels
{
    public class ResetPasswordViewModel
    {
		public string Email { get; set; }

		public string Token { get; set; }

		[Required(ErrorMessage = "Informe a nova senha")]
		[Display(Name = "Senha")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }

		[Required(ErrorMessage = "Informe a confirmação de senha")]
		[DataType(DataType.Password)]
		[Display(Name = "Confirmação de senha")]
		[Compare("NewPassword", ErrorMessage = "A senha e a confirmação de senha devem ser iguais")]
		public string ConfirmNewPassword { get; set; }
	}
}