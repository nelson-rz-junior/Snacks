using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Snacks.Areas.Admin.ViewModels;
using Snacks.Context;
using Snacks.Services.Identity.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snacks.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRolesConfig _roleConfig;

        public UserController(UserManager<ApplicationUser> userManager, IRolesConfig rolesConfig)
        {
            _userManager = userManager;
            _roleConfig = rolesConfig;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Index()
        {
            var userViewModel = new List<ListUserViewModel>();

            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userViewModel.Add(new ListUserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = string.Join(',', roles)
                });
            }

            return View(userViewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            IActionResult result;

            var user = await _userManager.Users
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userViewModel = new ListUserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = string.Join(',', roles),
                    RoleItems = _roleConfig.GetRoles()
                };

                result = View(userViewModel);
            }
            else
            {
                result = View("NotFound");
            }

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id, ListUserViewModel model)
        {
            IActionResult result = null;

            if (ModelState.IsValid)
            {
                var user = await _userManager.Users
                    .Where(u => u.Id == id)
                    .FirstOrDefaultAsync();

                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;

                    var updateUser = await _userManager.UpdateAsync(user);
                    if (updateUser.Succeeded)
                    {
                        var userRoles = await _userManager.GetRolesAsync(user);
                        var userRole = userRoles.FirstOrDefault();

                        if (!model.Role.Equals(userRole, StringComparison.OrdinalIgnoreCase))
                        {
                            var removeUserFromRole = await _userManager.RemoveFromRoleAsync(user, userRole);
                            if (removeUserFromRole.Succeeded)
                            {
                                var addUserToRole = await _userManager.AddToRoleAsync(user, model.Role);
                                if (addUserToRole.Succeeded)
                                {
                                    result = RedirectToAction("Index", "User");
                                }
                                else
                                {
                                    AddModelErrors(addUserToRole.Errors);
                                }
                            }
                            else
                            {
                                AddModelErrors(removeUserFromRole.Errors);
                            }
                        }
                        else
                        {
                            result = RedirectToAction("Index", "User");
                        }
                    }
                    else
                    {
                        AddModelErrors(updateUser.Errors);
                    }
                }
                else
                {
                    result = View("NotFound");
                }
            }

            if (result == null)
            {
                model.RoleItems = _roleConfig.GetRoles();
                result = View(model);
            }

            return result;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var createUserViewModel = new CreateUserViewModel
            {
                RoleItems = _roleConfig.GetRoles()
            };

            return View(createUserViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateUserViewModel createUserViewModel)
        {
            IActionResult result = null;

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = createUserViewModel.Email,
                    Email = createUserViewModel.Email,
                    FirstName = createUserViewModel.FirstName,
                    LastName = createUserViewModel.LastName
                };

                var userRegister = await _userManager.CreateAsync(user, createUserViewModel.Password);
                if (userRegister.Succeeded)
                {
                    var userRole = await _userManager.AddToRoleAsync(user, createUserViewModel.Role);
                    if (userRole.Succeeded)
                    {
                        result = RedirectToAction("Index", "User");
                    }
                    else
                    {
                        AddModelErrors(userRole.Errors);
                    }
                }
                else
                {
                    AddModelErrors(userRegister.Errors);
                }
            }

            if (result == null)
            {
                createUserViewModel.RoleItems = _roleConfig.GetRoles();
                result = View(createUserViewModel);
            }

            return result;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            IActionResult result = null;

            var user = await _userManager.Users
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                var listUserViewModel = new ListUserViewModel
                {
                    Id = id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };

                var userRoles = await _userManager.GetRolesAsync(user);

                var userRole = userRoles.FirstOrDefault();
                if (userRole != null)
                {
                    listUserViewModel.Role = userRole;
                }

                result = View(listUserViewModel);
            }
            else
            {
                result = View("NotFound");
            }

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            IActionResult result = null;

            var user = await _userManager.Users
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var userRole = userRoles.FirstOrDefault();

                await _userManager.RemoveFromRoleAsync(user, userRole);
                await _userManager.DeleteAsync(user);
                
                result = RedirectToAction("Index", "User");
            }
            else
            {
                result = View("NotFound");
            }

            return result;
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