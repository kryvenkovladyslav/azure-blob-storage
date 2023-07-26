using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Abstract.Interfaces
{
    public interface IUserService<TUser, TKey>
          where TUser : IdentityUser<TKey>
          where TKey : IEquatable<TKey>
    {
        public Task<IdentityResult> CreateAsync(TUser applicationUser, string password);

        public Task<IdentityResult> UpdateAsync(TUser applicationUser);

        public Task<IdentityResult> DeleteAsync(TUser applicationUser);

        public Task<TUser> FirstOrDefaultAsync(Expression<Func<TUser, bool>> expression, CancellationToken token = default);

        public Task<ICollection<TUser>> GetUsersAsync(CancellationToken token = default);

        public Task<ICollection<TUser>> GetUsersAsync(Expression<Func<TUser, bool>> expression, CancellationToken token = default);

        public Task<ICollection<TResult>> SelectFromUsersAsync<TResult>(ISelectionSpecification<TUser, TResult> specification, CancellationToken token = default);

        public Task<TResult> SelectFromUserAsync<TResult>(ISelectionSpecification<TUser, TResult> specification, CancellationToken token = default);
    }
}