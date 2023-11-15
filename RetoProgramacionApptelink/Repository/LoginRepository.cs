using Dapper;
using RetoProgramacionApptelink.Models;
using RetoProgramacionApptelink.Models.Data;
using System.Data;

namespace RetoProgramacionApptelink.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly Conexion _conexio;

        public LoginRepository(Conexion conexion)
        {
            _conexio = conexion;
        }



        public bool login(LoginUser login)
        {
            try
            {
                using (var conexion = _conexio.ObtenerConexion())
                {
                    var parametros = new DynamicParameters();

                    parametros.Add("@username", login.username, DbType.String);
                    parametros.Add("@password", login.password, DbType.String);
                    parametros.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    conexion.Execute("sp_login", parametros, commandType: CommandType.StoredProcedure);

                    int returnValue = parametros.Get<int>("@ReturnValue");

                    if (returnValue == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }catch (Exception ex)
            {
                Console.WriteLine("Error executing sp_login: " + ex.Message);
                return false;
            }
            
        }
    }
}
