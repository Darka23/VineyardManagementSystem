using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VineyardManagementSystem.Models;
using VineyardManagementSystem.Services;
using VineyardManagementSystem.ViewModels;

namespace VineyardManagementSystem.Controllers
{
    [Authorize]
    public class FieldActivitiesController : Controller
    {
        private readonly IFieldActivityService _activityService;
        private readonly IPlotService _plotService;

        public FieldActivitiesController(IFieldActivityService activityService, IPlotService plotService)
        {
            _activityService = activityService;
            _plotService = plotService;
        }

        public async Task<IActionResult> Index() => View(await _activityService.GetAllActivitiesAsync());

        public async Task<IActionResult> Create()
        {
            var vm = new FieldActivityFormViewModel { PlotList = await GetPlotSelectList() };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FieldActivityFormViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _activityService.CreateActivityAsync(vm.FieldActivity);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex) { ModelState.AddModelError("", ex.Message); }
            }
            vm.PlotList = await GetPlotSelectList();
            return View(vm);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var activity = await _activityService.GetByIdAsync(id);
            if (activity == null) return NotFound();
            return View(new FieldActivityFormViewModel { FieldActivity = activity, PlotList = await GetPlotSelectList() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FieldActivityFormViewModel vm)
        {
            if (id != vm.FieldActivity.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    await _activityService.UpdateActivityAsync(vm.FieldActivity);
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
            await _activityService.DeleteActivityAsync(id);
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