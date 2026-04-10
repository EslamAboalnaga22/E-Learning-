using ELearning.Core.Dtos.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearning.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser> GetUserByIdAsync(string UserId);
        Task<ApplicationUser> GetUserByNameAsync(string Username);
        Task<ApplicationUser> GetUserByEmailAsync(string Useremail);
        Task<UserRolesModel> GetUserWithRolesAsync(string UserId);
    }
}
