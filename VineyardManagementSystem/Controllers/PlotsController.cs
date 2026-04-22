using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VineyardManagementSystem.Models;
using VineyardManagementSystem.Services;
using VineyardManagementSystem.ViewModels;

namespace VineyardManagementSystem.Controllers
{
    [Authorize]
    public class PlotsController : Controller
    {
        private readonly IPlotService _plotService;
        private readonly IVineyardService _vineyardService;
        private readonly IGrapeVarietyService _varietyService;

        public PlotsController(IPlotService plotService, IVineyardService vineyardService, IGrapeVarietyService varietyService)
        {
            _plotService = plotService;
            _vineyardService = vineyardService;
            _varietyService = varietyService;
        }

        public async Task<IActionResult> Index()
        {
            var plots = await _plotService.GetAllPlotsAsync();
            return View(plots);
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new PlotFormViewModel
            {
                // Тук пълним списъците за падащите менюта
                VineyardList = await GetVineyardSelectList(),
                VarietyList = await GetVarietySelectList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlotFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _plotService.CreatePlotAsync(viewModel.Plot);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            // Ако има грешка, трябва пак да напълним списъците, преди да върнем View-то
            viewModel.VineyardList = await GetVineyardSelectList();
            viewModel.VarietyList = await GetVarietySelectList();
            return View(viewModel);
        }

        private async Task<IEnumerable<SelectListItem>> GetVineyardSelectList()
        {
            var vineyards = await _vineyardService.GetVineyardsListAsync();
            return vineyards.Select(v => new SelectListItem
            {
                Value = v.Id.ToString(),
                Text = v.Name
            });
        }

        private async Task<IEnumerable<SelectListItem>> GetVarietySelectList()
        {
            var varieties = await _varietyService.GetAllGrapeVarietiesAsync();
            return varieties.Select(v => new SelectListItem
            {
                Value = v.Id.ToString(),
                Text = v.Name
            });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var plot = await _plotService.GetPlotByIdAsync(id);
            if (plot == null) return NotFound();

            var viewModel = new PlotFormViewModel
            {
                Plot = plot,
                VineyardList = await GetVineyardSelectList(),
                VarietyList = await GetVarietySelectList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PlotFormViewModel viewModel)
        {
            if (id != viewModel.Plot.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _plotService.UpdatePlotAsync(viewModel.Plot);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            viewModel.VineyardList = await GetVineyardSelectList();
            viewModel.VarietyList = await GetVarietySelectList();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _plotService.DeletePlotAsync(id);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}