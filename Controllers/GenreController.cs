using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using aspnet_movies.Models;

namespace aspnet_movies.Controllers;

public class GenreController : Controller
{
    private readonly ILogger<GenreController> _logger;

    public GenreController(ILogger<GenreController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(Genre genre)
    {
        if (genre.Name != null) {
            Genre.Add(genre);
        }

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
