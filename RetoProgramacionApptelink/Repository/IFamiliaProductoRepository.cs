using RetoProgramacionApptelink.Models;
using RetoProgramacionApptelink.Models.ViewModels;

namespace RetoProgramacionApptelink.Repository
{
    public interface IFamiliaProductoRepository
    {
        List<FamiliaProductos> getAllFamilias();
        FamiliaProductos getFamiliaById(int id);
        bool addFamilia(FamiliaProductosViewModel Fproducto);
        bool editFamialia(FamiliaProductos Fproducto);
        bool deleteFamilia(int id);
    }
}
