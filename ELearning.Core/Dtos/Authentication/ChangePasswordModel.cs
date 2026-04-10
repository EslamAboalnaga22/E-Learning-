using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearning.Core.Dtos.Authentication
{
    public class ChangePasswordModel
    {
        //[Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "مـطـلـوب")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "مـطـلـوب")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "مـطـلـوب")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "غير متطابقان")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}
