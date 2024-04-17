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
    public class InstrumentoesController : Controller
    {
        private LbwContext _context;

        public InstrumentoesController(LbwContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var instrumentos = _context.Instrumentos.Select(i => new {
                i.IdInstrumento,
                i.IdCodigo,
                i.Descripcion,
                i.Nombre,
                i.Tipo,
                i.Vendor,
                i.Habilitado,
                i.FechaCalibrado,
                i.FechaCaducidad
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdInstrumento" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(instrumentos, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Instrumento();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Instrumentos.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdInstrumento });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Instrumentos.FirstOrDefaultAsync(item => item.IdInstrumento == key);
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
            var model = await _context.Instrumentos.FirstOrDefaultAsync(item => item.IdInstrumento == key);

            _context.Instrumentos.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Instrumento model, IDictionary values) {
            string ID_INSTRUMENTO = nameof(Instrumento.IdInstrumento);
            string ID_CODIGO = nameof(Instrumento.IdCodigo);
            string DESCRIPCION = nameof(Instrumento.Descripcion);
            string NOMBRE = nameof(Instrumento.Nombre);
            string TIPO = nameof(Instrumento.Tipo);
            string VENDOR = nameof(Instrumento.Vendor);
            string HABILITADO = nameof(Instrumento.Habilitado);
            string FECHA_CALIBRADO = nameof(Instrumento.FechaCalibrado);
            string FECHA_CADUCIDAD = nameof(Instrumento.FechaCaducidad);

            if(values.Contains(ID_INSTRUMENTO)) {
                model.IdInstrumento = Convert.ToInt32(values[ID_INSTRUMENTO]);
            }

            if(values.Contains(ID_CODIGO)) {
                model.IdCodigo = Convert.ToString(values[ID_CODIGO]);
            }

            if(values.Contains(DESCRIPCION)) {
                model.Descripcion = Convert.ToString(values[DESCRIPCION]);
            }

            if(values.Contains(NOMBRE)) {
                model.Nombre = Convert.ToString(values[NOMBRE]);
            }

            if(values.Contains(TIPO)) {
                model.Tipo = Convert.ToString(values[TIPO]);
            }

            if(values.Contains(VENDOR)) {
                model.Vendor = Convert.ToString(values[VENDOR]);
            }

            if(values.Contains(HABILITADO)) {
                model.Habilitado = values[HABILITADO] != null ? Convert.ToBoolean(values[HABILITADO]) : (bool?)null;
            }

            if(values.Contains(FECHA_CALIBRADO)) {
                model.FechaCalibrado = values[FECHA_CALIBRADO] != null ? Convert.ToDateTime(values[FECHA_CALIBRADO]) : (DateTime?)null;
            }

            if(values.Contains(FECHA_CADUCIDAD)) {
                model.FechaCaducidad = values[FECHA_CADUCIDAD] != null ? Convert.ToDateTime(values[FECHA_CADUCIDAD]) : (DateTime?)null;
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