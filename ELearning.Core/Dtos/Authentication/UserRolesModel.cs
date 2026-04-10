namespace ELearning.Core.Dtos.Authentication
{
    public class UserRolesModel
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public List<SelectedRolesModel> Roles { get; set; } = [];
    }
}
