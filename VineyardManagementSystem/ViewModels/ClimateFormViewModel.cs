using Microsoft.AspNetCore.Mvc.Rendering;
using VineyardManagementSystem.Models;

namespace VineyardManagementSystem.ViewModels
{
    public class ClimateFormViewModel
    {
        public ClimateLog ClimateLog { get; set; } = new ClimateLog { LogDate = DateTime.Now };
        public IEnumerable<SelectListItem>? VineyardList { get; set; }
    }
}
