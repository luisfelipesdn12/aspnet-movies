using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using aspnet_movies.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace aspnet_movies.Controllers;

public class MovieController : Controller
{
    private readonly ILogger<MovieController> _logger;

    public MovieController(ILogger<MovieController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(Movie movie)
    {
        movie.Genre = Genre.GetById(Int32.Parse(Request.Form["genre"]));
        movie.Director = Director.GetById(Int32.Parse(Request.Form["director"]));

        // Ignore Director and Genre check
        ModelState["Director"]!.ValidationState = ModelValidationState.Valid;
        ModelState["Genre"]!.ValidationState = ModelValidationState.Valid;

        if (ModelState.IsValid) {
            Movie.Add(movie);
        }

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
