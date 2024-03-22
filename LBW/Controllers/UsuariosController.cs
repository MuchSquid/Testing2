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

namespace LBW.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UsuariosController : Controller
    {
        private LbwContext _context;

        public UsuariosController(LbwContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var usuarios = _context.Usuarios.Select(i => new {
                i.UsuarioID,
                i.NombreCompleto,
                i.Correo,
                i.Rol,
                i.GMT_OFFSET,
                i.UsuarioDeshabilitado,
                i.FechaDeshabilitado
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "UsuarioID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(usuarios, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Usuario();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Usuarios.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.UsuarioID });
        }

        [HttpPut]
        public async Task<IActionResult> Put(string key, string values) {
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
        public async Task Delete(string key) {
            var model = await _context.Usuarios.FirstOrDefaultAsync(item => item.UsuarioID == key);

            _context.Usuarios.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Usuario model, IDictionary values) {
            string USUARIO_ID = nameof(Usuario.UsuarioID);
            string NOMBRE_COMPLETO = nameof(Usuario.NombreCompleto);
            string CORREO = nameof(Usuario.Correo);
            string ROL = nameof(Usuario.Rol);
            string GMT_OFFSET = nameof(Usuario.GMT_OFFSET);
            string USUARIO_DESHABILITADO = nameof(Usuario.UsuarioDeshabilitado);
            string FECHA_DESHABILITADO = nameof(Usuario.FechaDeshabilitado);

            if(values.Contains(USUARIO_ID)) {
                model.UsuarioID = Convert.ToString(values[USUARIO_ID]);
            }

            if(values.Contains(NOMBRE_COMPLETO)) {
                model.NombreCompleto = Convert.ToString(values[NOMBRE_COMPLETO]);
            }

            if(values.Contains(CORREO)) {
                model.Correo = Convert.ToString(values[CORREO]);
            }

            if(values.Contains(ROL)) {
                model.Rol = values[ROL] != null ? Convert.ToBoolean(values[ROL]) : (bool?)null;
            }

            if(values.Contains(GMT_OFFSET)) {
                model.GMT_OFFSET = values[GMT_OFFSET] != null ? Convert.ToInt32(values[GMT_OFFSET]) : (int?)null;
            }

            if(values.Contains(USUARIO_DESHABILITADO)) {
                model.UsuarioDeshabilitado = values[USUARIO_DESHABILITADO] != null ? Convert.ToBoolean(values[USUARIO_DESHABILITADO]) : (bool?)null;
            }

            if(values.Contains(FECHA_DESHABILITADO)) {
                model.FechaDeshabilitado = values[FECHA_DESHABILITADO] != null ? Convert.ToDateTime(values[FECHA_DESHABILITADO]) : (DateTime?)null;
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