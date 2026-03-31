using System.ComponentModel.DataAnnotations;
using VineyardManagementSystem.Enums;

namespace VineyardManagementSystem.Models
{
    public class FieldActivity
    {
        [Key]
        public int Id { get; set; }
        public int PlotId { get; set; }
        public Plot? Plot { get; set; }

        [Required(ErrorMessage = "Изберете вид дейност.")]
        [Display(Name = "Вид дейност")] 
        public ActivityType ActivityType { get; set; }

        [Required(ErrorMessage = "Датата е задължителна.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Разходът е задължителен.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Разход (ев.)")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Запишете кратко описание.")]
        public string Description { get; set; }
    }
}
