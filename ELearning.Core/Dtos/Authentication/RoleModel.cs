using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearning.Core.Dtos.Authentication
{
    public class RoleModel
    {
        public string? Id { get; set; } = string.Empty;
        [Required(ErrorMessage = "Role Name Required")]
        public string Name { get; set; } = string.Empty;
    }
}
