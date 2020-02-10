using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Snacks.Context;
using Snacks.ViewModels;
using System.Threading.Tasks;

namespace Snacks.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            IActionResult result = View(loginViewModel);
            string errorMessage = string.Empty;

            if (ModelState.IsValid)
            {
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
                    foreach (var error in userRegister.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}