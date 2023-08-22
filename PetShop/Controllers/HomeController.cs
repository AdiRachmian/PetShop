using Microsoft.AspNetCore.Mvc;
using PetShop.Models;
using PetShop.Services;
using System.Diagnostics;

namespace PetShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IAnimalService _animalService;

        public HomeController(ILogger<HomeController> logger, IAnimalService animalService)
        {
            _logger = logger;
            _animalService = animalService;
        }

        public IActionResult Index()
        {
            List<Animal> TopAnimals;
            TopAnimals = _animalService.GetTopComments();

            return View(TopAnimals);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
