using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VineyardManagementSystem.Models;
using VineyardManagementSystem.Services;

namespace VineyardManagementSystem.Controllers
{
    [Authorize]
    public class GrapeVarietiesController : Controller
    {
        private readonly IGrapeVarietyService _service;

        public GrapeVarietiesController(IGrapeVarietyService service)
        {
            _service = service;
        }


        public IActionResult Index()
        {

            var varieties = _service.GetAllGrapeVarietiesAsync().Result;
            return View(varieties);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(GrapeVariety variety)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.CreateGrapeVarietyAsync(variety);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(variety);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteGrapeVarietyAsync(id);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var variety = await _service.GetGrapeVarietyByIdAsync(id);
            if (variety == null) return NotFound();
            return View(variety);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(GrapeVariety variety)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateGrapeVarietyAsync(variety);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(variety);
        }
    }
}
