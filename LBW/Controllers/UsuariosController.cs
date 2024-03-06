using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using LBW.Models.Entity;
using Microsoft.CodeAnalysis.Scripting;
using BCrypt.Net;


namespace LBW.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UsuariosController : Controller
    {
        private LbwContext _context;

        public IActionResult Usuarios()
        {
            @ViewBag.perfil = "active";
            return View();
        }

        public UsuariosController(LbwContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var usuarios = _context.Usuarios.Select(i => new {
                i.UsuarioID,
                i.Nombre,
                i.Email,
                i.Clave,
                i.FechaCreacion
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "UsuarioID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(usuarios, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> HashearYActualizarContrasenas()
        {
            // Este es solo un ejemplo y no debe usarse en producción sin las debidas precauciones.
            var usuarios = await _context.Usuarios.ToListAsync();

            foreach (var usuario in usuarios)
            {
                // Aquí deberías tener la contraseña en texto plano temporalmente, asumiendo que ya existe
                string contrasenaPlana = usuario.Clave; // Esta propiedad debería existir temporalmente

                // Genera el hash de la contraseña
                string contrasenaHash = BCrypt.Net.BCrypt.HashPassword(contrasenaPlana);

                // Almacena el hash en la nueva columna
                usuario.Clave  = contrasenaHash;
             //   bool esCorrecta = BCrypt.Net.BCrypt.Verify(contrasenaIntroducidaPorElUsuario, hashAlmacenado);
         
                // Opcional: Eliminar la contraseña en texto plano si ya no es necesaria
                // usuario.ContrasenaPlana = null;
            }

            // Guarda los cambios en la base de datos
            await _context.SaveChangesAsync();

            return Ok("Contraseñas actualizadas.");
        }


        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Usuario();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            model.Clave = BCrypt.Net.BCrypt.HashPassword(model.Clave);
            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Usuarios.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.UsuarioID });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Usuarios.FirstOrDefaultAsync(item => item.UsuarioID == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key) {
            var model = await _context.Usuarios.FirstOrDefaultAsync(item => item.UsuarioID == key);

            _context.Usuarios.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Usuario model, IDictionary values) {
            string USUARIO_ID = nameof(Usuario.UsuarioID);
            string NOMBRE = nameof(Usuario.Nombre);
            string EMAIL = nameof(Usuario.Email);
            string CLAVE = nameof(Usuario.Clave);
            string FECHA_CREACION = nameof(Usuario.FechaCreacion);

            if(values.Contains(USUARIO_ID)) {
                model.UsuarioID = Convert.ToInt32(values[USUARIO_ID]);
            }

            if(values.Contains(NOMBRE)) {
                model.Nombre = Convert.ToString(values[NOMBRE]);
            }

            if(values.Contains(EMAIL)) {
                model.Email = Convert.ToString(values[EMAIL]);
            }

            if (values.Contains(CLAVE))
            {
                model.Clave = Convert.ToString(values[CLAVE]);
            }

            if (values.Contains(FECHA_CREACION)) {
                model.FechaCreacion = Convert.ToDateTime(values[FECHA_CREACION]);
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = new List<string>();

            foreach(var entry in modelState) {
                foreach(var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}