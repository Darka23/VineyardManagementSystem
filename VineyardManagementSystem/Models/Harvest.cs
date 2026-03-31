using System.ComponentModel.DataAnnotations;

namespace VineyardManagementSystem.Models
{
    public class Harvest
    {
        [Key]
        public int Id { get; set; }
        public int PlotId { get; set; }
        public Plot? Plot { get; set; }

        [Required(ErrorMessage = "Датата на гроздобер е задължителна.")]
        [Display(Name = "Дата на гроздобер")]
        public DateTime HarvestDate { get; set; }

        [Required(ErrorMessage = "Името на масива е задължително.")]
        [Display(Name = "Количество (кг)")]
        public double QuantityKG { get; set; }

        [Required(ErrorMessage = "Захарност (%) е задължителна.")]
        [Display(Name = "Захарност (%)")]
        public double SugarContent { get; set; }
    }
}
