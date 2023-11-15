using RetoProgramacionApptelink.Models;
using RetoProgramacionApptelink.Models.ViewModels;

namespace RetoProgramacionApptelink.Repository
{
    public interface ICabeceraFacturaRepository
    {
        List<CabeceraFactura> getAllCabeceras();
        CabeceraFactura getCabeceraFactura(int id);
        CabeceraFactura crearCabeceraFactura(AgregarCabeceraViewModel cabeceraFactura);
        bool deleteCabecera(int id);
    }
}
