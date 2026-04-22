using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VineyardManagementSystem.Models;
using VineyardManagementSystem.Services;
using VineyardManagementSystem.ViewModels;

namespace VineyardManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPlotService _plotService;
        private readonly IClimateService _climateService;
        private readonly IFieldActivityService _activityService;
        private readonly IHarvestService _harvestService;

        public HomeController(
            IPlotService plotService,
            IClimateService climateService,
            IFieldActivityService activityService,
            IHarvestService harvestService)
        {
            _plotService = plotService;
            _climateService = climateService;
            _activityService = activityService;
            _harvestService = harvestService;
        }

        public async Task<IActionResult> Index()
        {
            var plots = await _plotService.GetAllPlotsAsync();
            var climateLogs = await _climateService.GetAllLogsAsync();
            var activities = await _activityService.GetAllActivitiesAsync();
            var harvests = await _harvestService.GetAllHarvestsAsync();

            var model = new DashboardViewModel
            {
                TotalPlots = plots.Count(),
                // Взимаме последната температура (ако има такава)
                LatestTemperature = climateLogs.OrderByDescending(c => c.LogDate).FirstOrDefault()?.Temperature ?? 0,
                // Сумираме всички разходи
                TotalCosts = activities.Sum(a => a.Cost),
                // Сумираме цялото набрано грозде
                TotalHarvestKG = harvests.Sum(h => h.QuantityKG)
            };

            return View(model);
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
