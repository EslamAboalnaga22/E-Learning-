namespace ELearning.Infrastructure.Repositories
{
    public class UserRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : IUserRepository
    {
        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            var usersList = await userManager.Users.ToListAsync();

            if (usersList.Count == 0)
                return null;

            var user = usersList.Select(x => new ApplicationUser()
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                UserRoles = string.Join(",", userManager.GetRolesAsync(x).Result.ToArray())
            });

            if (user is null)
                return null;

            return user;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string UserId)
        {
            var user = await userManager.FindByIdAsync(UserId);

            user.UserRoles = string.Join(",", userManager.GetRolesAsync(user).Result.ToArray());

            if (user is null)
                return null;

            return user;
        }

        public async Task<ApplicationUser> GetUserByNameAsync(string Username)
        {
            var user = await userManager.FindByNameAsync(Username);

            user.UserRoles = string.Join(",", userManager.GetRolesAsync(user).Result.ToArray());

            if (user is null)
                return null;

            return user;
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string Useremail)
        {
            var user = await userManager.FindByEmailAsync(Useremail);

            user.UserRoles = string.Join(",", userManager.GetRolesAsync(user).Result.ToArray());

            if (user is null)
                return null;

            return user;
        }
        public async Task<UserRolesModel> GetUserWithRolesAsync(string UserId)
        {
            var user = await userManager.FindByIdAsync(UserId);

            if (user is null)
                return null;

            var roles = await roleManager.Roles.ToListAsync();

            UserRolesModel rolesDto = new()
            {
                Id = user.Id,
                UserName = user.UserName!,
                Roles = roles.Select(x => new SelectedRolesModel
                {
                    Name = x.Name!,
                    IsSelected = userManager.IsInRoleAsync(user, x.Name!).Result
                }).ToList()
            };

            return rolesDto;
        }
    }
}
