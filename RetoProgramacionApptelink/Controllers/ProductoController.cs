using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RetoProgramacionApptelink.Models;
using RetoProgramacionApptelink.Models.Data;
using RetoProgramacionApptelink.Models.ViewModels;
using RetoProgramacionApptelink.Repository;
using System.Data;

namespace RetoProgramacionApptelink.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IProductoRepository _repository;

        public ProductoController(IProductoRepository repository )
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            List<Producto> productos  = _repository.getAllProductos();
            List<FamiliaIdNombreMostrarViewModel> mostrar = _repository.getAllFamiliasIdNombre();
            ViewBag.FamiliaProductos = mostrar.Select(f => new SelectListItem
            {
                Value = f.IdFamilia.ToString(),
                Text = f.Nombre
            }).ToList();
            return View(productos);
        }

        public IActionResult Create()
        {
            return PartialView("_createProduct");
        }
        [HttpPost]
        public IActionResult Create(ProductoViewModel productoView)
        {
            if(!ModelState.IsValid)
                return RedirectToAction("Index");
            var validar = _repository.addProducto(productoView);
            if (validar == true)
                return RedirectToAction("Index");
            else
                return PartialView("_createProduct");
        }

        public IActionResult Edit(int id)
        {
            Producto producto = _repository.getProductoById(id);
            if (producto == null)
                return RedirectToAction("Index");
            else
                return PartialView("_EditarProducto", producto);
        }
        [HttpPost]
        public IActionResult Edit(Producto producto)
        {
            var validar = _repository.editProducto(producto);
            if (validar == false)
                return PartialView("_EditarProducto", producto);
            else
                return RedirectToAction("Index");
        }

        public IActionResult Detalles(int id)
        {
            Producto producto = _repository.getProductoById(id);
            if (producto == null)
                return RedirectToAction("Index");
            else
                return PartialView("_DetalleProducto", producto);
            
        }

        public IActionResult Delete(int id)
        {
            _repository.deleteProducto(id);
            return RedirectToAction("Index");
        }

    }
}
