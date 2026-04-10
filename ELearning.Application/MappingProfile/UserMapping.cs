namespace ELearning.Application.MappingProfile
{
    public class UserMapping
    {
        public static UserDtoDetails MapToDto(ApplicationUser user)
        {
            if (user is null)
                return null;

            return new UserDtoDetails
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName!,
                Email = user.Email!,
                UserRoles = user.UserRoles!
            };
        }

        //public static Module MapToEntity(ModuleDtoRequest moduleDto)
        //{
        //    if (moduleDto is null)
        //        return null;

        //    return new Module
        //    {
        //        Title = moduleDto.Title,
        //        Description = moduleDto.Description,
        //        CourseId = moduleDto.CourseId,
        //    };
        //}
    }
}
