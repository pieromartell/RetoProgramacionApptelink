using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using RetoProgramacionApptelink.Models;
using RetoProgramacionApptelink.Models.ViewModels;
using RetoProgramacionApptelink.Repository;

namespace RetoProgramacionApptelink.Controllers
{
    public class FacturasController : Controller
    {

        private readonly ICabeceraFacturaRepository _repository;
        private readonly IDetalleFacturaRepository _repository2;

        public FacturasController(ICabeceraFacturaRepository repository, IDetalleFacturaRepository repository2)
        {
            _repository = repository;
            _repository2 = repository2;
        }

        public IActionResult Index()
        {
            var model = _repository.getAllCabeceras();
            return View(model);
        }

        public IActionResult create()
        {
            List<ListadoPreciosPorCodProductoViewModel> listado = _repository2.getAllProductoCodN();

            ViewBag.subtotal = 0;
            ViewBag.igv = 0;
            ViewBag.tota = 0;
            ViewBag.listadoProducto1 = listado;
            if (TempData["Confirmar"] == null)
            {
                TempData["Confirmar"] = false;
            }
            bool confirmar = (bool)TempData["Confirmar"];
            if (confirmar != null && confirmar != false)
            {
                ViewBag.ListadoProducto1 = listado.Select(f => new SelectListItemPrecio
                {
                    Value = f.Codigo,
                    Text = f.Nombre,
                    Precio = f.Precio
                }).ToList();
                int idFactura = (int)TempData["IdFactura"];
                var listadodetalle = _repository2.getAllDetalleFacturaByIdFactura(idFactura);
                decimal sumaSubtotales = listadodetalle.Sum(item => item.subtotal);
                ViewBag.subtotal = sumaSubtotales;
                decimal igv = (sumaSubtotales * 18) / 100;
                ViewBag.igv = igv;
                decimal total = sumaSubtotales + igv;
                ViewBag.total = total;
                FacturasViewModel model = new FacturasViewModel();
                model.ListdetalleFacturas = listadodetalle;
                return View(model);
            }
            else
                return View();
            
        }
        [HttpPost]
        public IActionResult create(AgregarCabeceraViewModel agregarCabecera)
        {
            List<ListadoPreciosPorCodProductoViewModel> listado = _repository2.getAllProductoCodN();
            ViewBag.ListadoProducto1 = listado.Select(f => new SelectListItemPrecio
            {
                Value = f.Codigo,
                Text = f.Nombre,
                Precio = f.Precio
            }).ToList();
            var validar = _repository.crearCabeceraFactura(agregarCabecera);
            if (validar != null)
            {
                ViewBag.IdFactura = validar.IdFactura;
                TempData["IdFactura"] = validar.IdFactura;
                TempData.Keep("IdFactura");
                FacturasViewModel model = new FacturasViewModel();
                model.AgregarCabecera = agregarCabecera;
                return View(model);
            }
            else
                return View();

        }

        public IActionResult createDetalle()
        {
            var detalleFacturaView = new DetalleFacturaViewModel
            {
                IdFactura = (int)TempData["IdFactura"]
            };

            return PartialView("_crearDetalleFactura", detalleFacturaView);
        }

        [HttpPost]
        public IActionResult createDetalle(DetalleFacturaViewModel detalleFacturaView)
        {
            if (!ModelState.IsValid) return View();
            var validar = _repository2.addDetalleFactura(detalleFacturaView);
            if (validar != false)
            {
                TempData["Confirmar"] = true;
                TempData.Keep("Confirmar");
                TempData["IdFactura"] = detalleFacturaView.IdFactura;
                TempData.Keep("IdFactura");
                return RedirectToAction("create");
            }
            else
                return View();
        }

        public IActionResult delete(int id)
        {
            var validar = _repository.deleteCabecera(id);
            if (validar != null)
                return RedirectToAction("Index");
            else
            {
                return null;
            }
        }
        public IActionResult volverALaLista()
        {
            TempData["Confirmar"] = null;
            TempData["IdFactura"] = null;
            return RedirectToAction("Index");

        }

        public IActionResult listarDetallesItem(int id)
        {
            var validar = _repository2.getAllDetalleFacturaByIdFactura(id);
            List<ListadoPreciosPorCodProductoViewModel> listado = _repository2.getAllProductoCodN();
            decimal sumaSubtotales = validar.Sum(item => item.subtotal);
            ViewBag.subtotal = sumaSubtotales;
            decimal igv = (sumaSubtotales * 18) / 100;
            ViewBag.igv = igv;
            decimal total = sumaSubtotales + igv;
            ViewBag.total = total;
            ViewBag.ListadoProducto = listado.Select(f => new SelectListItemPrecio
            {
                Value = f.Codigo,
                Text = f.Nombre,
                Precio = f.Precio
            }).ToList();

            FacturasViewModel model = new FacturasViewModel();
            model.ListdetalleFacturas = validar;
            model.idFactura = id;
            return View("listarDetallesItem", model);
        }


        [HttpPost]
        public IActionResult crearDetalleListado(FacturasViewModel facturaViewModel)
        {
            DetalleFacturaViewModel detalleFactura = facturaViewModel.DetalleFacturaViewModel;
            var validar = _repository2.addDetalleFactura(detalleFactura);
            if (validar != false)
                return RedirectToAction("listarDetallesItem", new { id = detalleFactura.IdFactura });
            else
                return RedirectToAction("listarDetallesItem", new { id = detalleFactura.IdFactura });
        }

        public IActionResult deleteDetalle(int id, int idfactura)
        {
            var validar = _repository2.deleteDetalleFactura(id);
            if (validar != null)
            {
                TempData["Confirmar"] = true;
                TempData.Keep("Confirmar");
                TempData["IdFactura"] = idfactura;
                TempData.Keep("IdFactura");
                return RedirectToAction("create");
            }
            else
            {
                return null;
            }
        }


        public IActionResult deleteDetalleListado(int id,int idfactura)
        {
            var validar = _repository2.deleteDetalleFactura(id);
            if (validar != null)
                return RedirectToAction("listarDetallesItem",new { id = idfactura });
            else
            {
                return null;
            }
        }
    }
}
