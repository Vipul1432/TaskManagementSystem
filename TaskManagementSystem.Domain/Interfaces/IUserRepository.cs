using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Data.Repository
{
    public interface IUserRepository
    {
        Task<IdentityResult> RegisterUserAsync(IdentityUser user, string password);
        Task<SignInResult> LoginUserAsync(string username, string password);
        Task LogoutUserAsync();
    }
}
