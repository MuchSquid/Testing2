using LBW.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Text;
using System.Security.Cryptography;
namespace LBW.Data
{
    public class UsuarioDatos
    {
        public List<Usuario> ListaUsuario()
        {
            try
            {
                var _usuario = new List<Usuario>();
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("SP_Usuario_listar", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            _usuario.Add(new Usuario()
                            {
                                UsuarioID = dr["UsuarioID"] != DBNull.Value ? dr["UsuarioID"].ToString() : string.Empty,
                                NombreCompleto = dr["NombreCompleto"] != DBNull.Value ? dr["NombreCompleto"].ToString() : string.Empty,
                                Correo = dr["Correo"] != DBNull.Value ? dr["Correo"].ToString() : string.Empty,
                                Rol = dr["ROL"] != DBNull.Value ? Convert.ToBoolean(dr["ROL"]) : false, // Asumiendo valor predeterminado
                                GMT_OFFSET = dr["GMT_OFFSET"] != DBNull.Value ? Convert.ToInt32(dr["GMT_OFFSET"]) : 0,
                                UsuarioDeshabilitado = dr["UsuarioDeshabilitado"] != DBNull.Value ? Convert.ToBoolean(dr["ROL"]) : false,// Asumiendo valor predeterminado
                                FechaDeshabilitado = dr["FechaDeshabilitado"] != DBNull.Value ? Convert.ToDateTime(dr["FechaDeshabilitado"]) : DateTime.MinValue // Asumiendo valor predeterminado
                            });
                        }
                    }
                } 
                Console.WriteLine("Pass 1 😂");
                Console.WriteLine(_usuario.ToString());
                
                return _usuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Usuario ValidarUsuario(string _usuario)
        {
            Console.WriteLine("Pass 2 😍",_usuario);
            return ListaUsuario().Where(item => item.UsuarioID == _usuario ).FirstOrDefault();
        }

        public static string ConvertirSha256(string texto)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));
                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        }
    }
}
