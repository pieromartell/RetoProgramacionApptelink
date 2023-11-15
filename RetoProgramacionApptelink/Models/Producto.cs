namespace RetoProgramacionApptelink.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int IdFamilia { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public virtual FamiliaProductos FamiliaProductos { get; set; } = null!;


    }
}
