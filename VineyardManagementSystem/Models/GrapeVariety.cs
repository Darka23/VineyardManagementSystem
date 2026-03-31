using System.ComponentModel.DataAnnotations;
using VineyardManagementSystem.Enums;

namespace VineyardManagementSystem.Models
{
    public class GrapeVariety
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Името на сорта грозде е задължително.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Изберете цвят на гроздето.")]
        [Display(Name = "Цвят на гроздето")]
        public GrapeColor Color { get; set; }
    }
}
