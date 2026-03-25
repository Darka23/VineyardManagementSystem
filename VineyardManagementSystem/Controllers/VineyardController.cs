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

    // Списък с всички лозя
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
}