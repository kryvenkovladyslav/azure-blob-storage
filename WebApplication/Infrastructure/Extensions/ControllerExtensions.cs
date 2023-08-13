using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Infrastructure.Extensions
{
    public static class ControllerExtensions
    {
        public static void AddErrorToModelState(this Controller controller, IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                controller.AddErrorToModelState(error);
            }
        }

        public static void AddErrorToModelState(this Controller controller, IdentityError error)
        {
            controller.ModelState.AddModelError(error.Code, error.Description);
        }
    }
}