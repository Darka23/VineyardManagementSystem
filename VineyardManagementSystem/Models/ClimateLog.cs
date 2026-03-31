using System.ComponentModel.DataAnnotations;

namespace VineyardManagementSystem.Models
{
    public class ClimateLog
    {
        [Key]
        public int Id { get; set; }
        public int VineyardId { get; set; }
        public Vineyard? Vineyard { get; set; }

        [Required(ErrorMessage = "Датата на измерване е задължителна.")]
        [Display(Name = "Дата на измерване")]
        public DateTime LogDate { get; set; }

        [Required(ErrorMessage = "Температурата е задължителна.")]
        [Display(Name = "Температура (°C)")]
        public double Temperature { get; set; }

        [Required(ErrorMessage = "Влажността е задължителна.")]
        [Display(Name = "Влажност (%)")]
        public double Humidity { get; set; }

        [Required(ErrorMessage = "Валежите са задължителни.")]
        [Display(Name = "Валежи (mm)")]
        public double Rainfall { get; set; }
    }
}
