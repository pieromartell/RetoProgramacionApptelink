using Dapper;
using RetoProgramacionApptelink.Models;
using RetoProgramacionApptelink.Models.Data;
using RetoProgramacionApptelink.Models.ViewModels;
using System.Data;

namespace RetoProgramacionApptelink.Repository
{
    public class DetalleFacturaRepository : IDetalleFacturaRepository
    {
        private readonly Conexion _conexion;

        public DetalleFacturaRepository(Conexion conexion)
        {
            _conexion = conexion; 
        }

        public bool addDetalleFactura(DetalleFacturaViewModel detalleFacturaView)
        {
            try
            {
                using(var Conexion = _conexion.ObtenerConexion())
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@IdFactura", detalleFacturaView.IdFactura, DbType.Int64);
                    parametros.Add("@CodigoProducto", detalleFacturaView.CodigoProducto, DbType.String);
                    parametros.Add("@Cantidad", detalleFacturaView.Cantidad, DbType.Int64);
                    parametros.Add("@Precio", detalleFacturaView.Precio, DbType.Decimal);
                    var validar = Conexion.Query<DetalleFactura>("sp_crearDetalle", parametros, commandType: CommandType.StoredProcedure);
                    if (validar != null)
                        return true;
                    else
                        return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool deleteDetalleFactura(int id)
        {
            var validar = getDetalleFactura(id);
            if(validar == null) return false;
            using (var Conexion = _conexion.ObtenerConexion())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@Id", id, DbType.Int64);
                var validares = Conexion.Query<DetalleFactura>("sp_eliminarDetalle", parametros, commandType: CommandType.StoredProcedure);
                if (validares != null)
                    return true;
                else
                    return false;
            }
        }

        public List<DetalleFactura> getAllDetalleFacturaByIdFactura(int id)
        {
            using (var Conexion = _conexion.ObtenerConexion())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@Id", id, DbType.Int64);
                return Conexion.Query<DetalleFactura>("sp_listarDetallePorIdFactura", parametros, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public DetalleFactura getDetalleFactura(int id)
        {
            using (var Conexion = _conexion.ObtenerConexion())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@Id", id,DbType.Int64);
                DetalleFactura detalle = Conexion.QueryFirstOrDefault<DetalleFactura>("sp_listarUnDetalle", parametros, commandType: CommandType.StoredProcedure);
                if (detalle == null)
                    return null;
                else
                    return detalle;
            }
        }
        public List<ListadoPreciosPorCodProductoViewModel> getAllProductoCodN()
        {
            using(var Conexion = _conexion.ObtenerConexion())
            {
                return Conexion.Query<ListadoPreciosPorCodProductoViewModel>("sp_listarProductosPorCodyNombre", commandType: CommandType.StoredProcedure).ToList();
            }
        }
        
        public ListadoPreciosPorCodProductoViewModel getAllListadoPrecio(string codigo)
        {
            try
            {
                using(var Conexion = _conexion.ObtenerConexion())
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@Codigo", codigo, DbType.String);
                    return Conexion.QueryFirstOrDefault<ListadoPreciosPorCodProductoViewModel>("sp_listarPrecioProductoByCod", parametros, commandType: CommandType.StoredProcedure);
                }
            }
            catch
            {
                return null;
            }
        }
        

    }
}
