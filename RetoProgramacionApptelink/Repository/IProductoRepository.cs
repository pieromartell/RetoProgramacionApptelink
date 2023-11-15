using RetoProgramacionApptelink.Models;
using RetoProgramacionApptelink.Models.ViewModels;

namespace RetoProgramacionApptelink.Repository
{
    public interface IProductoRepository
    {
        List<Producto> getAllProductos();
        Producto getProductoById(int id);
        bool addProducto(ProductoViewModel producto);
        bool editProducto(Producto producto);
        bool deleteProducto(int id);
        List<FamiliaIdNombreMostrarViewModel> getAllFamiliasIdNombre();
    }
}
