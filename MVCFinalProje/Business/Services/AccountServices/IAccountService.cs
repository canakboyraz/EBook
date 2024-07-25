using Microsoft.AspNetCore.Identity;
using MVCFinalProje.Domain.Enums;
using MVCFinalProje.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Business.Services.AccountServices
{
    public interface IAccountService
    {
        Task<bool> AnyAsync(Expression<Func<IdentityUser, bool>> expression);

        Task<IdentityResult> CreateUserAsync(IdentityUser user, Roles role);

        Task<IdentityResult> DeleteByAsync(string userId);

        Task<IdentityResult> UpdateEmailAsync(string userId, string newEmail);
    }
}
