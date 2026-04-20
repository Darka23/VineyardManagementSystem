using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VineyardManagementSystem.Models;
using VineyardManagementSystem.Services;
using VineyardManagementSystem.ViewModels;

namespace VineyardManagementSystem.Controllers
{
    public class ClimateController : Controller
    {
        private readonly IClimateService _climateService;
        private readonly IVineyardService _vineyardService;

        public ClimateController(IClimateService climateService, IVineyardService vineyardService)
        {
            _climateService = climateService;
            _vineyardService = vineyardService;
        }

        public async Task<IActionResult> Index()
        {
            var logs = await _climateService.GetAllLogsAsync();
            return View(logs);
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new ClimateFormViewModel
            {
                VineyardList = await GetVineyardList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClimateFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _climateService.CreateLogAsync(viewModel.ClimateLog);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            viewModel.VineyardList = await GetVineyardList();
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var log = await _climateService.GetLogByIdAsync(id);
            if (log == null) return NotFound();

            var viewModel = new ClimateFormViewModel
            {
                ClimateLog = log,
                VineyardList = await GetVineyardList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClimateFormViewModel viewModel)
        {
            if (id != viewModel.ClimateLog.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _climateService.UpdateLogAsync(viewModel.ClimateLog);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            viewModel.VineyardList = await GetVineyardList();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _climateService.DeleteLogAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<IEnumerable<SelectListItem>> GetVineyardList()
        {
            var vineyards = await _vineyardService.GetVineyardsListAsync();
            return vineyards.Select(v => new SelectListItem
            {
                Value = v.Id.ToString(),
                Text = v.Name
            });
        }
    }
}