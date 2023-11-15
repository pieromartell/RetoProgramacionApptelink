using Dapper;
using RetoProgramacionApptelink.Models;
using RetoProgramacionApptelink.Models.Data;
using RetoProgramacionApptelink.Models.ViewModels;
using System.Data;
using System.Text.RegularExpressions;

namespace RetoProgramacionApptelink.Repository
{
    public class CabeceraFacturaRepository : ICabeceraFacturaRepository
    {
        private readonly Conexion _conexion;

        public CabeceraFacturaRepository(Conexion conexion)
        {
            _conexion = conexion;
        }

        public CabeceraFactura crearCabeceraFactura(AgregarCabeceraViewModel cabeceraFactura)
        {
            try
            {
                using (var Conexion = _conexion.ObtenerConexion())
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("@NroFacturas", cabeceraFactura.NroFacturas, DbType.String);
                    parametros.Add("@RucCliente", cabeceraFactura.RucCliente, DbType.String);
                    parametros.Add("@RazonSocialCLiente", cabeceraFactura.RazonSocialCliente, DbType.String);
                    var validar = Conexion.QueryFirstOrDefault<CabeceraFactura>("sp_crearCabecera", parametros, commandType: CommandType.StoredProcedure);
                    if (validar != null)
                        return validar;
                    else
                        return null;
                }
            }
            catch(Exception e)
            {
                return null;
            }

        }

        public bool deleteCabecera(int id)
        {
            try
            {
                var cabeceraValidar = getCabeceraFactura(id);
                if (cabeceraValidar == null)
                    return false;
                else
                {
                    using (var Conexion = _conexion.ObtenerConexion())
                    {
                        var parametros = new DynamicParameters();
                        parametros.Add("@Id", id, DbType.Int64);
                        var validar = Conexion.Query<CabeceraFactura>("sp_EliminarCabecera", parametros, commandType: CommandType.StoredProcedure);
                        if (validar != null)
                            return true;
                        else
                            return false;
                    }
                }
            }
            catch
            {
                return false;
            }

        }


        public List<CabeceraFactura> getAllCabeceras()
        {
            using(var Conexion = _conexion.ObtenerConexion())
            {
                return Conexion.Query<CabeceraFactura>("sp_listarCabecera", commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public CabeceraFactura getCabeceraFactura(int id)
        {

            using (var Conexion = _conexion.ObtenerConexion())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@Id", id, DbType.Int64);
                CabeceraFactura cabecera = Conexion.QueryFirstOrDefault<CabeceraFactura>("sp_listarUnaCabecera", parametros, commandType: CommandType.StoredProcedure);
                if (cabecera == null)
                    return null;
                else
                    return cabecera;

            }
        }
    }
}
