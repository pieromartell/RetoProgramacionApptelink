using RetoProgramacionApptelink.Models;
using RetoProgramacionApptelink.Models.ViewModels;

namespace RetoProgramacionApptelink.Repository
{
    public interface IDetalleFacturaRepository
    {
        DetalleFactura getDetalleFactura(int id);
        List<DetalleFactura> getAllDetalleFacturaByIdFactura(int id);
        bool addDetalleFactura(DetalleFacturaViewModel detalleFacturaView);
        bool deleteDetalleFactura(int id);
        List<ListadoPreciosPorCodProductoViewModel> getAllProductoCodN();
        ListadoPreciosPorCodProductoViewModel getAllListadoPrecio(string codigo);

    }
}
