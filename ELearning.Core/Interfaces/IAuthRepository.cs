using ELearning.Core.Dtos.Authentication;

namespace ELearning.Core.Interfaces
{
    public interface IAuthRepository
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> LoginAsync(LoginModel model);
        Task<IEnumerable<IdentityRole>> GetAllRolesAsync();
        Task<IdentityRole> GetRoleByIdAsync(string RoleId);
        Task<string> CreateRoleAsync(RoleModel model);
        Task<string> AssignRoleAsync(UserRolesModel model);
        Task<string> UpdateRoleAsync(RoleModel model);
        Task<bool> DeleteRoleAsync(string RoleId);
        Task<bool> ChangePasswordAsync(ChangePasswordModel model);
        Task<bool> ForgetPasswordAsync(ForgetPasswordModel model);
        Task<bool> ResetPasswordAsync(ResetPasswordModel model);
    }
}
