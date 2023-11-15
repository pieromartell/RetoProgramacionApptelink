namespace RetoProgramacionApptelink.Models
{
    public class FamiliaProductos
    {
        public int IdFamilia { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}
