using System.ComponentModel.DataAnnotations;

namespace VV_TestWork.Core
{
    public enum Gender
    {
        [Display(Name = "Неизвестно")]
        Unknown,
        [Display(Name = "Мужчина")]
        Male,
        [Display(Name = "Женщина")]
        Female
    }
}
