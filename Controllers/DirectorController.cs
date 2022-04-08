using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using aspnet_movies.Models;

namespace aspnet_movies.Controllers;

public class DirectorController : Controller
{
    private readonly ILogger<DirectorController> _logger;

    public DirectorController(ILogger<DirectorController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(Director director)
    {
        if (director.Name != null) {
            Director.Add(director);
            return RedirectToAction("Index", "Home");
        } else {
            return View();
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
