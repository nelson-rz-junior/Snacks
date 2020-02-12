using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Snacks.Context;
using Snacks.Services.Email.Interfaces;
using Snacks.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Snacks.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService, 
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(new RegisterViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            });
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RegisterViewModel registerViewModel)
        {
            IActionResult result;

            var user = await _userManager.GetUserAsync(User);

            user.Email = registerViewModel.Email;
            user.UserName = registerViewModel.Email;
            user.FirstName = registerViewModel.FirstName;
            user.LastName = registerViewModel.LastName;

            var updateUser = await _userManager.UpdateAsync(user);
            if (updateUser.Succeeded)
            {
                result = RedirectToAction("Index", "Home");
            }
            else
            {
                AddModelErrors(updateUser.Errors);
                result = View(registerViewModel);
            }

            return result;
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            IActionResult result = View();

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var checkPassword = await _userManager.CheckPasswordAsync(user, changePasswordViewModel.CurrentPassword);
                if (checkPassword)
                {
                    var changePassword = await _userManager.ChangePasswordAsync(user, changePasswordViewModel.CurrentPassword, changePasswordViewModel.NewPassword);
                    if (changePassword.Succeeded)
                    {
                        result = RedirectToAction("Edit", "Account");
                    }
                    else
                    {
                        AddModelErrors(changePassword.Errors);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "A senha atual é inválida.");
                }
            }

            return result;
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel)
        {
            IActionResult result;

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByEmailAsync(forgotPasswordViewModel.Email);
                    if (user != null)
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                        var callbackUrl = Url.Action("ResetPassword", "Account", new { token, email = user.Email }, Request.Scheme);

                        string nameTo = $"{ user.FirstName } { user.LastName }";
                        string emailTo = user.Email;
                        string subject = "Redefinição de senha";
                        string content = $"Link para redefinição de senha: <a href='{callbackUrl}'>{callbackUrl}</a>";

                        await _emailService.SendAsync(nameTo, emailTo, subject, content);
                    }

                    result = View("ForgotPasswordConfirmation");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while sending e-mail");
                    throw;
                }
            }
            else
            {
                result = View(forgotPasswordViewModel);
            }

            return result;
        }

        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            IActionResult result;

            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var verifyUserToken = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider,
                    UserManager<ApplicationUser>.ResetPasswordTokenPurpose, token);

                if (verifyUserToken)
                {
                    result = View(new ResetPasswordViewModel
                    {
                        Token = token,
                        Email = email
                    });
                }
                else
                {
                    result = RedirectToAction("ResetPasswordError", "Account");
                }
            }
            else
            {
                result = RedirectToAction("Login", "Account");
            }

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            IActionResult result;

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(resetPasswordViewModel.Email);
                if (user != null)
                {
                    var resetPassword = await _userManager.ResetPasswordAsync(user, resetPasswordViewModel.Token, resetPasswordViewModel.NewPassword);
                    if (resetPassword.Succeeded)
                    {
                        result = RedirectToAction("ResetPasswordConfirmation", "Account");
                    }
                    else
                    {
                        AddModelErrors(resetPassword.Errors);
                        result = View(resetPasswordViewModel);
                    }
                }
                else
                {
                    result = RedirectToAction("ResetPasswordConfirmation", "Account");
                }
            }
            else
            {
                result = View(resetPasswordViewModel);
            }

            return result;
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPasswordError()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            IActionResult result;

            if (!User.Identity.IsAuthenticated)
            {
                result = View(new LoginViewModel
                {
                    ReturnUrl = returnUrl
                });
            }
            else
            {
                if (string.IsNullOrWhiteSpace(returnUrl) || !Url.IsLocalUrl(returnUrl))
                {
                    result = RedirectToAction("Index", "Home");
                }
                else
                {
                    result = Redirect(returnUrl);
                }
            }

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            IActionResult result = View(loginViewModel);
            
            if (ModelState.IsValid)
            {
                string errorMessage = string.Empty;

                var user = await _userManager.FindByNameAsync(loginViewModel.Email);
                if (user != null)
                {
                    var loginUser = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (loginUser.Succeeded)
                    {
                        if (string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl) || !Url.IsLocalUrl(loginViewModel.ReturnUrl))
                        {
                            result = RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            result = Redirect(loginViewModel.ReturnUrl);
                        }
                    }
                    else
                    {
                        if (loginUser.IsLockedOut)
                        {
                            errorMessage = "Não foi possível completar a autenticação, pois o usuário informado encontra-se bloqueado.";
                        }
                        else if (loginUser.IsNotAllowed)
                        {
                            errorMessage = "Não foi possível completar a autenticação, pois o usuário informado não possui permissão para esta operação.";
                        }
                        else if (loginUser.RequiresTwoFactor)
                        {
                            errorMessage = "Não foi possível completar a autenticação, pois o 2FA está habilitado para esta conta.";
                        }
                        else
                        {
                            errorMessage = "Não foi possível completar a autenticação, favor verificar os dados.";
                        }
                    }
                }
                else
                {
                    errorMessage = "Não foi possível completar a autenticação, pois o usuário não foi encontrado.";
                }

                if (!string.IsNullOrWhiteSpace(errorMessage))
                {
                    ModelState.AddModelError("", errorMessage);
                }
            }

            return result;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            IActionResult result = View(registerViewModel);

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = registerViewModel.Email,
                    Email = registerViewModel.Email,
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName
                };

                var userRegister = await _userManager.CreateAsync(user, registerViewModel.Password);
                if (userRegister.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    result = RedirectToAction("Index", "Home");
                }
                else
                {
                    AddModelErrors(userRegister.Errors);
                }
            }

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private void AddModelErrors(IEnumerable<IdentityError> identityErrors)
        {
            foreach (var error in identityErrors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}