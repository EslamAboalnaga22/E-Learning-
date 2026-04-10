using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearning.Core.Dtos.Authentication
{
    public class ForgetPasswordModel
    {
        [Required(ErrorMessage = "مـطـلـوب")]  
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        public string? WebLink { get; set; } = string.Empty;
    }
}
