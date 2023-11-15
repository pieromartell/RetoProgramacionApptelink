using Dapper;
using RetoProgramacionApptelink.Models;
using RetoProgramacionApptelink.Models.Data;
using RetoProgramacionApptelink.Models.ViewModels;
using System.Data;
using System.Text.RegularExpressions;

namespace RetoProgramacionApptelink.Repository
{
    public class FamiliaProductoRepository : IFamiliaProductoRepository
    {
        private readonly Conexion _conexion;

        public FamiliaProductoRepository(Conexion conexion)
        {
            _conexion = conexion;
        }

        public bool addFamilia(FamiliaProductosViewModel Fproducto)
        {
            try
            {
                using (var conexion = _conexion.ObtenerConexion())
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@Codigo", Fproducto.Codigo, DbType.String);
                    parametros.Add("@Nombre", Fproducto.Nombre, DbType.String);
                    var validador = conexion.Execute("sp_crearFamilia", parametros, commandType: CommandType.StoredProcedure);
                    if (validador > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }

        }

        public bool deleteFamilia(int id)
        {
            var productoVerificar = getFamiliaById(id);
            if (productoVerificar != null)
            {
                using (var conexion = _conexion.ObtenerConexion())
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@Id", id, DbType.Int64);
                    var validador = conexion.Query<FamiliaProductos>("sp_eliminarFamilia", parametros, commandType: CommandType.StoredProcedure);
                    if (validador != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        public bool editFamialia(FamiliaProductos Fproducto)
        {
            var productoVerificar = getFamiliaById(Fproducto.IdFamilia);
            if(productoVerificar != null)
            {
                using(var conexion = _conexion.ObtenerConexion())
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@Id", Fproducto.IdFamilia, DbType.Int64);
                    parametros.Add("@Codigo", Fproducto.Codigo, DbType.String);
                    parametros.Add("@Nombre", Fproducto.Nombre, DbType.String);
                    parametros.Add("@Activo", Fproducto.Activo, DbType.Boolean);
                    parametros.Add("@FechaCreacion", Fproducto.FechaCreacion, DbType.Date);
                    var validador = conexion.Query<FamiliaProductos>("sp_editarFamilia", parametros, commandType: CommandType.StoredProcedure);
                    if(validador != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        public List<FamiliaProductos> getAllFamilias()
        {
            using(var conexion = _conexion.ObtenerConexion())
            {
                return conexion.Query<FamiliaProductos>("sp_listarFamilia", commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public FamiliaProductos getFamiliaById(int id)
        {
            using (var conexion = _conexion.ObtenerConexion())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@id", id, DbType.Int32);
                return conexion.QueryFirstOrDefault<FamiliaProductos>("sp_listarUnaFamilia", parametros, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
