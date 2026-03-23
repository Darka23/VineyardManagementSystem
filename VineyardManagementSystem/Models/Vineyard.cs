using System.ComponentModel.DataAnnotations;

namespace VineyardManagementSystem.Models
{
    public class Vineyard
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Името е задължително")]
        [Display(Name = "Име на масива")]
        public string Name { get; set; }

        [Display(Name = "Местоположение")]
        public string Location { get; set; }

        [Display(Name = "Площ (дка)")]
        public double Size { get; set; }

        [Display(Name = "Дата на засаждане")]
        public DateTime PlantingDate { get; set; }
    }
}
