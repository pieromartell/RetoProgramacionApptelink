namespace RetoProgramacionApptelink.Models
{
    public class CabeceraFactura
    {
        public int IdFactura { get; set; }
        public string NroFacturas { get; set; }
        public string RucCliente { get; set; }
        public string RazonSocialCLiente { get; set; }
        public decimal Subtotal { get; set; }
        public decimal PorcentajeIGV { get; set; }
        public List<DetalleFactura> listDetalleFactura { get; set; }

    }
}
