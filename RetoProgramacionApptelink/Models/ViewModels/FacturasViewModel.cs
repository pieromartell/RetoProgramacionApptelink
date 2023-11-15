namespace RetoProgramacionApptelink.Models.ViewModels
{
    public class FacturasViewModel
    {
        public AgregarCabeceraViewModel AgregarCabecera { get; set; }
        public List<DetalleFactura> ListdetalleFacturas { get; set; }
        public DetalleFacturaViewModel DetalleFacturaViewModel { get; set;}
        public int idFactura { get; set; }
    }
}
