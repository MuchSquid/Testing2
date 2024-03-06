using LBW.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

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
                                FechaCreacion = Convert.ToDateTime(dr["FECHACREACION"])
                            });
                        }
                    }
                } 
                Console.WriteLine("Pass 1 😂");
                Console.WriteLine(_usuario);
                return _usuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Usuario ValidarUsuario(string _correo, string _clave)
        {
            Console.WriteLine("Pass 2 😍");
            return ListaUsuario().Where(item => item.Nombre == _correo && item.Email == _clave).FirstOrDefault();
  
        }
    }
}
