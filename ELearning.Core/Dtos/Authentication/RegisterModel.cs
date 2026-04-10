using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearning.Core.Dtos.Authentication
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "مـطـلـوب")]
        [StringLength(50, ErrorMessage = "الحد الأقصى 50 حرف")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "مـطـلـوب")]
        [StringLength(50, ErrorMessage = "الحد الأقصى 50 حرف")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "مـطـلـوب")]
        [StringLength(80, ErrorMessage = "الحد الأقصى 80 حرف"), DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "مـطـلـوب")]
        [StringLength(80, ErrorMessage = "الحد الأقصى 80 حرف"), DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "مـطـلـوب")]
        [Compare("Password", ErrorMessage ="غير متطابقان")]
        [StringLength(80, ErrorMessage = "الحد الأقصى 80 حرف"), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
