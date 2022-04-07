using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using aspnet_movies.Models;

namespace aspnet_movies.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var directors = Director.GetAll();

        // print each director's name
        foreach (var director in directors)
        {
            Console.WriteLine(director.Name);
        }

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
