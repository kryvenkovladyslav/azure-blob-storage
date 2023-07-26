using Microsoft.AspNetCore.Identity;

namespace WebApplication.Infrastructure.Common
{
    public static class IdentityErrors
    {
        public static IdentityError UserNotFound()
        {
            return new IdentityError
            {
                Code = nameof(UserNotFound),
                Description = "User with current username wan not found"
            };
        }

        public static IdentityError RoleNotFound()
        {
            return new IdentityError
            {
                Code = nameof(RoleNotFound),
                Description = "Role with current name wan not found"
            };
        }

        public static IdentityError InvalidUserName()
        {
            return new IdentityError
            {
                Code = nameof(InvalidUserName),
                Description = "Invalid username"
            };
        }

        public static IdentityError InvalidPassword()
        {
            return new IdentityError
            {
                Code = nameof(InvalidPassword),
                Description = "Invalid password"
            };
        }
    }
}