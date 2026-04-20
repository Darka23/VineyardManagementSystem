using Microsoft.AspNetCore.Mvc.Rendering;
using VineyardManagementSystem.Models;

namespace VineyardManagementSystem.ViewModels
{
    public class PlotFormViewModel
    {
        public Plot Plot { get; set; } = new Plot();

        // Списъци за падащите менюта
        public IEnumerable<SelectListItem> VineyardList { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> VarietyList { get; set; } = new List<SelectListItem>();
    }
}