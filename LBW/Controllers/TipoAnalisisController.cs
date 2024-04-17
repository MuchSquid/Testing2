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
    public class TipoAnalisisController : Controller
    {
        private LbwContext _context;

        public TipoAnalisisController(LbwContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var tipoanalisiss = _context.TipoAnalisiss.Select(i => new {
                i.IdTipoA,
                i.NombreA,
                i.Descripcion,
                i.Removed
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdTipoA" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(tipoanalisiss, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new TipoAnalisis();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.TipoAnalisiss.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdTipoA });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.TipoAnalisiss.FirstOrDefaultAsync(item => item.IdTipoA == key);
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
            var model = await _context.TipoAnalisiss.FirstOrDefaultAsync(item => item.IdTipoA == key);

            _context.TipoAnalisiss.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(TipoAnalisis model, IDictionary values) {
            string ID_TIPO_A = nameof(TipoAnalisis.IdTipoA);
            string NOMBRE_A = nameof(TipoAnalisis.NombreA);
            string DESCRIPCION = nameof(TipoAnalisis.Descripcion);
            string REMOVED = nameof(TipoAnalisis.Removed);

            if(values.Contains(ID_TIPO_A)) {
                model.IdTipoA = Convert.ToInt32(values[ID_TIPO_A]);
            }

            if(values.Contains(NOMBRE_A)) {
                model.NombreA = Convert.ToString(values[NOMBRE_A]);
            }

            if(values.Contains(DESCRIPCION)) {
                model.Descripcion = Convert.ToString(values[DESCRIPCION]);
            }

            if(values.Contains(REMOVED)) {
                model.Removed = values[REMOVED] != null ? Convert.ToBoolean(values[REMOVED]) : (bool?)null;
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