namespace RetoProgramacionApptelink.Models
{
    public class DetalleFactura
    {
        public int IdItem { get; set; }
        public int IdFactura { get; set; }
        public string CodigoProducto { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal subtotal { get; set; }

    }
}
