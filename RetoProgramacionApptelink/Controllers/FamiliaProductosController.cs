using Microsoft.AspNetCore.Mvc;
using RetoProgramacionApptelink.Models;
using RetoProgramacionApptelink.Models.ViewModels;
using RetoProgramacionApptelink.Repository;

namespace RetoProgramacionApptelink.Controllers
{
    public class FamiliaProductosController : Controller
    {
        private readonly IFamiliaProductoRepository repository;

        public FamiliaProductosController(IFamiliaProductoRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            var productos = repository.getAllFamilias();
            return View(productos);
        }

        public async Task<IActionResult> Create()
        {
            var model = new FamiliaProductosViewModel();
            return PartialView( "_CreateFamilia", model);
        }

        [HttpPost]
        public IActionResult Create(FamiliaProductosViewModel Fproducto)
        {
            if (!ModelState.IsValid)
                RedirectToAction("Index");

            var products = repository.addFamilia(Fproducto);
            if (products != false)
                return RedirectToAction("Index");
            else
                return RedirectToAction("Index");

        }

        public IActionResult Delete(int id)
        {
            repository.deleteFamilia(id);
            return RedirectToAction("Index");
        }

        public IActionResult edit(int id)
        {
            FamiliaProductos familiaProductos = repository.getFamiliaById(id);
            if (familiaProductos == null)
                return RedirectToAction("Index");
            else
                return PartialView("_EditarFamiliaProducto", familiaProductos);
        }

        [HttpPost]
        public IActionResult edit(FamiliaProductos Fproducto)
        {
            var validar = repository.editFamialia(Fproducto);
            if (validar == false)
                return PartialView("_EditarFamiliaProducto", Fproducto);
            else
                return RedirectToAction("Index");
        }


    }
}
