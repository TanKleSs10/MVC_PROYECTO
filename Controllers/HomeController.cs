using ASPNETMVCCheckboxDemo.Models;
using ASPNETMVCCheckboxDemo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace ASPNETMVCCheckboxDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ClienteRepository _clienteRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            var dbConnection = AccessDbConnection.GetInstance(configuration);
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

        [HttpPost]
        public IActionResult Search(SearchModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Query)) { 
                ModelState.AddModelError("SearchField", "Por favor, seleccione un campo de búsqueda."); 
                ModelState.AddModelError("Query", "El campo de búsqueda no puede estar vacío."); 
                return View("Index", _clienteRepository.GetAll()); 
            }

            var results = _clienteRepository.Search(model.SearchField, model.Query, model.Order);
            return View("Index", results);
        }

        public IActionResult DisplayCliente(int id)
        {
            var cliente = _clienteRepository.GetAll().FirstOrDefault(c => c.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }
    }
}
