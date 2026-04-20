using Microsoft.AspNetCore.Mvc.Rendering;
using VineyardManagementSystem.Models;

namespace VineyardManagementSystem.ViewModels
{
    public class HarvestFormViewModel
    {
        public Harvest Harvest { get; set; } = new Harvest { HarvestDate = DateTime.Now };
        public IEnumerable<SelectListItem>? PlotList { get; set; }
    }
}
