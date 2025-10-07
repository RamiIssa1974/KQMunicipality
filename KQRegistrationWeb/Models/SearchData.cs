using System.ComponentModel.DataAnnotations;

namespace KQRegistrationWeb.Models
{
    public class SearchData
    {
        [Required(ErrorMessage = "الرجاء ادخال رقم الهويه.")]
        [StringLength(9, ErrorMessage = "الرجاء ادخال رقم هويه مكون من 9 خانات.", MinimumLength = 9)]
        public string StudentIDN { get; set; }
        [Required(ErrorMessage = "الرجاء ادخال تاريخ الميلاد.")]
        public DateTime? StudentBirthDate { get; set; }
        public string? ErrorSearchMessage { get; set; }
        public StartData? StartData { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public int RegistrationTypeId { get; set; }
    }
}