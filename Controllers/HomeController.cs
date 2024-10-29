using ASPNETMVCCheckboxDemo.Models;
using ASPNETMVCCheckboxDemo.Repositories; // Esto debe coincidir con el espacio de nombres de ClienteRepository
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASPNETMVCCheckboxDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ClienteRepository _clienteRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, AccessDbConnection dbConnection)
        {
            _logger = logger;
            _clienteRepository = new ClienteRepository(dbConnection);
        }

        public IActionResult Index()
        {
            List<Cliente> clientes = _clienteRepository.GetAll();
            return View(clientes);
        }

        [HttpPost]
        public IActionResult Index(HomeViewModel model)
        {
            var isBlogActive = model.IsBlogActive;
            return RedirectToAction("Index");
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
}
