using Dapper;
using RetoProgramacionApptelink.Models;
using RetoProgramacionApptelink.Models.Data;
using RetoProgramacionApptelink.Models.ViewModels;
using System.Data;

namespace RetoProgramacionApptelink.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly Conexion _conexion;

        public ProductoRepository(Conexion conexion)
        {
            _conexion = conexion;
        }

        public bool addProducto(ProductoViewModel producto)
        {
            try
            {
                using (var Conexion = _conexion.ObtenerConexion())
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@Codigo", producto.Codigo, DbType.String);
                    parametros.Add("@Nombre", producto.Nombre, DbType.String);
                    parametros.Add("@IdFamilia", producto.IdFamilia, DbType.String);
                    parametros.Add("@Precio", producto.Precio, DbType.Decimal);
                    parametros.Add("@Stock", producto.stock, DbType.Int64);
                    var validar = Conexion.Query<Producto>("sp_crearProducto", parametros, commandType: CommandType.StoredProcedure);
                    if (validar != null)
                        return true;
                    else
                        return false;
                }
            }catch{
                return false;
            }

        }

        public bool deleteProducto(int id)
        {
            Producto producto = getProductoById(id);
            if (producto == null)
                return false;
            else
            {
                using(var Conexion = _conexion.ObtenerConexion())
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@Id", id, DbType.Int64);
                    var validar = Conexion.Query<Producto>("sp_eliminarProducto", parametros, commandType: CommandType.StoredProcedure);
                    if (validar != null)
                        return true;
                    else
                        return false;
                }
            }
            throw new NotImplementedException();
        }

        public List<Producto> getAllProductos()
        {
            using(var Conexion = _conexion.ObtenerConexion())
            {
                return Conexion.Query<Producto>("sp_listarProducto", commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public Producto getProductoById(int id)
        {
            using(var Conexion = _conexion.ObtenerConexion())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@Id", id, DbType.Int64);
                return Conexion.QueryFirstOrDefault<Producto>("sp_listarUnProducto", parametros, commandType: CommandType.StoredProcedure);
            }
        }

        public bool editProducto(Producto producto)
        {
            Producto valid = getProductoById(producto.IdProducto);
            if (valid == null)
                return false;
            else
            {
                using (var Conexion = _conexion.ObtenerConexion())
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@Id", producto.IdProducto, DbType.Int64);
                    parametros.Add("@Codigo", producto.Codigo, DbType.String);
                    parametros.Add("@Nombre", producto.Nombre, DbType.String);
                    parametros.Add("@IdFamilia", producto.IdFamilia, DbType.String);
                    parametros.Add("@Precio", producto.Precio, DbType.Int64);
                    parametros.Add("@Stock", producto.Stock, DbType.Int64);
                    parametros.Add("@Activo", producto.Activo, DbType.Boolean);
                    parametros.Add("@FechaCreacion", producto.FechaCreacion, DbType.Date);
                    var validar = Conexion.Query<Producto>("sp_editarProducto", parametros, commandType: CommandType.StoredProcedure);
                    if (validar != null)
                        return true;

                    else
                        return false;
                }
            }
        }

        public List<FamiliaIdNombreMostrarViewModel> getAllFamiliasIdNombre()
        {
            using (var Conexion = _conexion.ObtenerConexion())
            {
                return Conexion.Query<FamiliaIdNombreMostrarViewModel>("sp_listarFamiliasPorId", commandType: CommandType.StoredProcedure).ToList();
            }
        }
    }
}
