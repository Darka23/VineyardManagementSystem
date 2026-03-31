using System.ComponentModel.DataAnnotations;

namespace VineyardManagementSystem.Models
{
    public class Plot
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Кодът на парцела е задължителен.")]
        public string InternalCode { get; set; }

        public int VineyardId { get; set; }
        public Vineyard? Vineyard { get; set; } 

        public int GrapeVarietyId { get; set; }
        public GrapeVariety? GrapeVariety { get; set; }

        [Required(ErrorMessage = "Размерът на парцела е задължително.")]
        [Range(1, 10000)]
        public double AreaSize { get; set; } 
    }
}
