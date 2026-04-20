using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VineyardManagementSystem.Models;
using VineyardManagementSystem.Services;
using VineyardManagementSystem.ViewModels;

namespace VineyardManagementSystem.Controllers
{
    public class HarvestsController : Controller
    {
        private readonly IHarvestService _harvestService;
        private readonly IPlotService _plotService;

        public HarvestsController(IHarvestService harvestService, IPlotService plotService)
        {
            _harvestService = harvestService;
            _plotService = plotService;
        }

        public async Task<IActionResult> Index() => View(await _harvestService.GetAllHarvestsAsync());

        public async Task<IActionResult> Create()
        {
            var vm = new HarvestFormViewModel { PlotList = await GetPlotSelectList() };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HarvestFormViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _harvestService.CreateHarvestAsync(vm.Harvest);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex) { ModelState.AddModelError("", ex.Message); }
            }
            vm.PlotList = await GetPlotSelectList();
            return View(vm);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var harvest = await _harvestService.GetByIdAsync(id);
            if (harvest == null) return NotFound();

            var vm = new HarvestFormViewModel { Harvest = harvest, PlotList = await GetPlotSelectList() };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HarvestFormViewModel vm)
        {
            if (id != vm.Harvest.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _harvestService.UpdateHarvestAsync(vm.Harvest);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex) { ModelState.AddModelError("", ex.Message); }
            }
            vm.PlotList = await GetPlotSelectList();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _harvestService.DeleteHarvestAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<IEnumerable<SelectListItem>> GetPlotSelectList()
        {
            var plots = await _plotService.GetAllPlotsAsync();
            return plots.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = $"{p.InternalCode} ({p.Vineyard?.Name})"
            });
        }
    }
}