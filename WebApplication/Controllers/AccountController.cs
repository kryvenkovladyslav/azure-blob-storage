using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using WebApplication.Models.ViewModels;
using Abstract.Models;
using Abstract.Interfaces;
using AutoMapper;
using WebApplication.Infrastructure.Extensions;
using WebApplication.Infrastructure.Common;
using Abstract.Common;
using System.Security.Claims;

namespace WebApplication.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserRoleService<ApplicationUser, ApplicationRole, Guid> userRoleService;
        private readonly IAuthenticationService<ApplicationUser, Guid> authenticationService;
        private readonly IUserService<ApplicationUser, Guid> userService;
        private readonly IMapper mapper;

        public AccountController(
            IUserRoleService<ApplicationUser, ApplicationRole, Guid> userRoleService,
            IAuthenticationService<ApplicationUser, Guid> authenticationService,
            IUserService<ApplicationUser, Guid> userService,
            IMapper mapper)
        {
            this.authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            this.userRoleService = userRoleService ?? throw new ArgumentNullException(nameof(userRoleService));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return this.View(nameof(this.Login));
        }

        [HttpPost]
        [AllowAnonymous]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginViewModel userModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(userModel);
            }

            var user = await this.userService.FirstOrDefaultAsync(u => u.UserName == userModel.UserName);

            if (user == null)
            {
                this.AddErrorToModelState(IdentityErrors.InvalidUserName());
                return this.View(userModel);
            }

            var signInResult = await this.authenticationService.SignInWithPasswordAsync(user, userModel.Password);

            if (signInResult.Succeeded)
            {
                return this.Redirect(userModel.ReturnUrl ?? new PathString($"/Home/Index"));
            }

            this.AddErrorToModelState(IdentityErrors.InvalidPassword());
            return this.View(userModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return this.View(nameof(this.Register));
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel userModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(userModel);
            }

            var mappedUser = this.mapper.Map<ApplicationUser>(userModel);

            var creationResult = await this.userService.CreateAsync(mappedUser, userModel.Password);

            if (creationResult.Succeeded)
            {
                var role = Enum.GetName(ApplicationRoles.User);

                await this.userRoleService.AddUserToRoleAsync(mappedUser, new ApplicationRole(role));

                return this.RedirectToAction(nameof(this.AccountCreatedSuccessfully));
            }

            this.AddErrorToModelState(creationResult);
            return this.View(userModel);
        }

        [HttpGet]
        [Authorize(Policy = ClaimTypes.Role)]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await this.authenticationService.LogoutAsync();
            return this.RedirectToAction(nameof(this.Login));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccountCreatedSuccessfully()
        {
            return this.View(nameof(this.AccountCreatedSuccessfully));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return this.View(nameof(this.AccessDenied));
        }
    }
}