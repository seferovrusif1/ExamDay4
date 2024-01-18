using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.ViewModels.AuthVMs;

namespace WebApplication1.Controllers
{
    public class AuthController : Controller
    {
        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        UserManager<AppUser> _userManager { get; }
        SignInManager<AppUser> _signInManager { get; }
        RoleManager<IdentityRole> _roleManager { get; }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            AppUser user = new AppUser
            {
                FullName = vm.FullName,
                UserName = vm.UserName,
                Email = vm.Email,
            };
            var result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(vm);
            }
            var roleResult = await _userManager.AddToRoleAsync(user, "Member");
            if (!roleResult.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(vm);
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            return RedirectToAction(nameof(Login));

        }




        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            AppUser user;
            if (vm.UserNameOrEmail.Contains("@"))
            {
                user= await _userManager.FindByEmailAsync(vm.UserNameOrEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(vm.UserNameOrEmail);

            }
            if (user == null)
            {
                ModelState.AddModelError("", "Wrong Input");
                return View(vm);
            }
            var result= await _signInManager.CheckPasswordSignInAsync(user, vm.Password,true);
            if (!result.Succeeded)
            {
                return View(vm);
            }
            await _signInManager.SignInAsync(user, true);

            return RedirectToAction(nameof(Index),"Home");
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login),"Auth");
        }


        public async Task<bool> CreateInitial()
        {
            foreach (var item in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(item.ToString()))
                {
                    var result = await _roleManager.CreateAsync(new IdentityRole { Name = item.ToString() });
                    if (!result.Succeeded)
                    {
                        return false;
                    }                
                }
            }
            if (await _userManager.FindByEmailAsync("Admin@gmail.com")==null && await _userManager.FindByNameAsync("Admin") == null)
            {
                AppUser user = new AppUser
                {
                    FullName = "Admin",
                    UserName = "Admin",
                    Email = "Admin@gmail.com",
                };
                var result =await _userManager.CreateAsync(user, "Admin123");
                if (!result.Succeeded)
                {
                    return false;
                }
                var roleResult = await _userManager.AddToRoleAsync(user, "Admin");
                if (!roleResult.Succeeded)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
