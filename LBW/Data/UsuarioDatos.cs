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
                                UsuarioID = Convert.ToInt32(dr["USUARIOID"]),
                                Nombre = dr["NOMBRE"].ToString(),
                                Email = dr["EMAIL"].ToString(),
                                FechaCreacion = Convert.ToDateTime(dr["FECHACREACION"]),
                                Clave = dr["CONTRASENA_HASH"].ToString()
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

        public Usuario ValidarUsuario(string _usuario, string _clave)
        {
            Console.WriteLine("Pass 2 😍");
            return ListaUsuario().Where(item => item.Nombre == _usuario && item.Clave == _clave).FirstOrDefault();
  
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
