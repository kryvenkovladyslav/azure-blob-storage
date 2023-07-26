using Abstract.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class IdentityUserService<TUser, TKey> : IUserService<TUser, TKey>
           where TUser : IdentityUser<TKey>
           where TKey : IEquatable<TKey>
    {
        private readonly UserManager<TUser> userManager;

        public IdentityUserService(UserManager<TUser> userManager)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public virtual async Task<IdentityResult> CreateAsync(TUser applicationUser, string password)
        {
            if (applicationUser == null)
            {
                throw new ArgumentNullException(nameof(applicationUser));
            }

            return await this.userManager.CreateAsync(applicationUser, password);
        }

        public virtual async Task<IdentityResult> UpdateAsync(TUser applicationUser)
        {
            if (applicationUser == null)
            {
                throw new ArgumentNullException(nameof(applicationUser));
            }

            return await this.userManager.UpdateAsync(applicationUser);
        }

        public virtual async Task<IdentityResult> DeleteAsync(TUser applicationUser)
        {
            if (applicationUser == null)
            {
                throw new ArgumentNullException(nameof(applicationUser));
            }

            return await this.userManager.DeleteAsync(applicationUser);
        }

        public virtual async Task<TUser> FirstOrDefaultAsync(Expression<Func<TUser, bool>> expression, CancellationToken token = default)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return await this.userManager.Users.FirstOrDefaultAsync(expression, token);
        }

        public virtual async Task<ICollection<TUser>> GetUsersAsync(CancellationToken token = default)
        {
            return await this.userManager.Users.ToListAsync(token) ?? new List<TUser>();
        }

        public virtual async Task<ICollection<TUser>> GetUsersAsync(Expression<Func<TUser, bool>> expression, CancellationToken token = default)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return await this.userManager.Users.Where(expression).ToListAsync(token) ?? new List<TUser>();
        }

        public virtual async Task<ICollection<TResult>> SelectFromUsersAsync<TResult>(ISelectionSpecification<TUser, TResult> specification, CancellationToken token = default)
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var query = this.userManager.Users;
            query = specification.Where != null ? query.Where(specification.Where) : query;

            return await query.Select(specification.Select).ToListAsync(token);
        }

        public virtual async Task<TResult> SelectFromUserAsync<TResult>(ISelectionSpecification<TUser, TResult> specification, CancellationToken token = default)
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var query = this.userManager.Users;
            query = specification.Where != null ? query.Where(specification.Where) : query;

            return await query.Select(specification.Select).FirstOrDefaultAsync(token);
        }
    }
}