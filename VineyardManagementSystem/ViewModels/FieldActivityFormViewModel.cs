using Microsoft.AspNetCore.Mvc.Rendering;
using VineyardManagementSystem.Models;

namespace VineyardManagementSystem.ViewModels
{
    public class FieldActivityFormViewModel
    {
        public FieldActivity FieldActivity { get; set; } = new FieldActivity { Date = DateTime.Now };
        public IEnumerable<SelectListItem>? PlotList { get; set; }
    }
}
