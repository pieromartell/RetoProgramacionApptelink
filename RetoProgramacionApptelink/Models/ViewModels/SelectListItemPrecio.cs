using Microsoft.AspNetCore.Mvc.Rendering;

namespace RetoProgramacionApptelink.Models.ViewModels
{
    public class SelectListItemPrecio: SelectListItem
    {
        public decimal Precio { get; set; }
    }
}
