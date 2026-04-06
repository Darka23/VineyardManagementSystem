using Microsoft.AspNetCore.Mvc;
using VineyardManagementSystem.Services;
using VineyardManagementSystem.Models;

public class VineyardsController : Controller
{
    private readonly IVineyardService _service;

    public VineyardsController(IVineyardService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var vineyards = await _service.GetVineyardsListAsync();
        return View(vineyards);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(Vineyard vineyard)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _service.CreateVineyardAsync(vineyard);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
        }
        return View(vineyard);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteVineyardAsync(id);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var vineyard = await _service.GetVineyardByIdAsync(id);
        if (vineyard == null)
        {
            return NotFound();
        }
        return View(vineyard);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Vineyard vineyard)
    {
        if (id != vineyard.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                await _service.UpdateVineyardAsync(vineyard);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
        }
        return View(vineyard);
    }
}