using System.ComponentModel.DataAnnotations;

namespace VineyardManagementSystem.Models
{
    public class Vineyard
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Името на масива е задължително.")]
        [StringLength(100, ErrorMessage = "Името не може да е над 100 символа.")]
        [Display(Name = "Име на масива")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Моля, въведете местоположение.")]
        [Display(Name = "Местоположение")]
        public string Location { get; set; } = string.Empty;

        [Required(ErrorMessage = "Площта е задължителна.")]
        [Range(0.1, 5000, ErrorMessage = "Площта трябва да е между 0.1 и 5000 дка.")]
        [Display(Name = "Площ (дка)")]
        public double Size { get; set; }

        [Required(ErrorMessage = "Моля, изберете дата на засаждане.")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата на засаждане")]
        public DateTime PlantingDate { get; set; }
    }
}
