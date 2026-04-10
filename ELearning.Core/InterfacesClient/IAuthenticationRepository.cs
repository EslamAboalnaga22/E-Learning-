using ELearning.Core.Dtos.Authentication;

namespace ELearning.Core.InterfacesClient
{
    public interface IAuthenticationRepository
    {
        Task<AuthModel> RegisterUser(RegisterModel userRegisteration);
        Task<AuthModel> LogIn(LoginModel loginModel);
        Task Logout();
        Task<List<ApplicationUser>> GetAllUsers();
        Task<ApplicationUser> GetUserById(string userId);
        Task<ApplicationUser> GetUserByName(string userName);
        Task<UserRolesModel> GetUserWithRoles(string userId);
        Task AddUserRole(UserRolesModel usersRoles);
        Task<AuthModel> ChangePassword(ChangePasswordModel changePassword);
        Task ForgotPassword(ForgetPasswordModel forgotPassword);
        Task ResetPassword(ResetPasswordModel resetPassword);
    }
}
