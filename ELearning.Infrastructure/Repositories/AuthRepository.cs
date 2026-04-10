using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace ELearning.Infrastructure.Repositories
{
    public class AuthRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, JWT jwt, IEmailSender emailSender) : IAuthRepository
    {
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);
            List<Claim> rolesClaims = [];

            foreach (var role in roles)
                rolesClaims.Add(new Claim(ClaimTypes.Role, role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id.ToString()),
                new Claim(ClaimTypes.Name , user.UserName),
            }
            .Union(userClaims)
            .Union(rolesClaims);

            var semmtiricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));

            var signingCredentials = new SigningCredentials(semmtiricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                signingCredentials: signingCredentials,
                claims: claims,
                expires: DateTime.Now.AddHours(1)
            );

            return jwtSecurityToken;
        }
        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            var UserName = $"{model.FirstName}{model.LastName}";
            if (await userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthModel { Message = "Email is already Exist" };

            if (await userManager.FindByNameAsync(UserName) is not null)
                return new AuthModel { Message = "UserName is already Exist" };

            ApplicationUser user = new()
            {
                UserName = UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in errors)
                    errors += $"{error}";

                return new AuthModel { Message = errors };
            }

            await userManager.AddToRoleAsync(user, "User");

            var jwtSecurityToken = await CreateJwtToken(user);

            return new AuthModel
            {
                UserName = user.UserName,
                Email = user.Email,
                IsAuthenticated = true,
                Roles = ["User"],
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            };
        }
        public async Task<AuthModel> LoginAsync(LoginModel model)
        {
            AuthModel authModel = new();

            var user = await userManager.FindByEmailAsync(model.Email);

            if (user is null || !await userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Email or Password is incorrect";
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken(user);

            var rolesList = await userManager.GetRolesAsync(user);

            authModel.Email = user.Email;
            authModel.UserName = user.UserName;
            authModel.IsAuthenticated = true;
            authModel.Roles = [.. rolesList];
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return authModel;
        }
        public async Task<IEnumerable<IdentityRole>> GetAllRolesAsync()
        {
            var roles = await roleManager.Roles.ToListAsync();

            if (roles.Count == 0)
                return null;

            return roles;
        }
        public async Task<IdentityRole> GetRoleByIdAsync(string RoleId)
        {
            var role = await roleManager.FindByIdAsync(RoleId);

            if (role is null)
                return null;

            return role;
        }
        public async Task<string> CreateRoleAsync(RoleModel model)
        {
            if (model == null)
                return "Something went wrong";

            var identityRole = new IdentityRole()
            {
                Name = model.Name,
            };

            var result = await roleManager.CreateAsync(identityRole);

            return result.Succeeded ? string.Empty : "Something went wrong";
        }
        public async Task<string> AssignRoleAsync(UserRolesModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);

            if (user is null)
                return "Invalid UserId or Role";

            foreach (var role in model.Roles)
            {
                if (!await roleManager.RoleExistsAsync(role.Name))
                    return "Invalid UserId or Role";

                if (role.IsSelected)
                    await userManager.AddToRoleAsync(user, role.Name);
                else
                    await userManager.RemoveFromRoleAsync(user, role.Name);
            }

            return "Assigned Successfully";
        }
        public async Task<string> UpdateRoleAsync(RoleModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id!);

            if(role is null)
                return "Something went wrong";

            role.Name = model.Name;

            var result = await roleManager.UpdateAsync(role);

            return result.Succeeded ? "Updated Successfully" : "Something went wrong";
        }
        public async Task<bool> DeleteRoleAsync(string RoleId)
        {
            var role = await roleManager.FindByIdAsync(RoleId);

            if (role is null)
                return false;

            var result = await roleManager.DeleteAsync(role);

            return result.Succeeded ? true : false;
        }
        public async Task<bool> ChangePasswordAsync(ChangePasswordModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user is null)
                return false;

            var result = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
                return false;

            return true;
        }
        public async Task<bool> ForgetPasswordAsync(ForgetPasswordModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user is null)
                return false;

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            var passwrodResetLink = $"{model.WebLink}?email={model.Email}&token={token}";

            //await emailSender.SendEmailAsync(user.Email, "Reset Password", $"<P>Hi {user.UserName}, </P> <P>{token}</p>");
            await emailSender.SendEmailAsync(user.Email, "Reset Password", $"<P>Hi {user.UserName}, </P> <P>To reset your password please <a href={passwrodResetLink}>CLICK HERE</a></p> <P>{token}</p>");

            return true;
        }
        public async Task<bool> ResetPasswordAsync(ResetPasswordModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user is null)
                return false;

            var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);

            return result.Succeeded;
        }
    }
}
