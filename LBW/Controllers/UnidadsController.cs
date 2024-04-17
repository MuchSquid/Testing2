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
    public class UnidadsController : Controller
    {
        private LbwContext _context;

        public UnidadsController(LbwContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var unidades = _context.Unidades.Select(i => new {
                i.IdUnidad,
                i.Nombre,
                i.DisplayString,
                i.ChangedBy,
                i.ChangedOn,
                i.Removed,
                i.Description
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdUnidad" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(unidades, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Unidad();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Unidades.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdUnidad });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Unidades.FirstOrDefaultAsync(item => item.IdUnidad == key);
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
            var model = await _context.Unidades.FirstOrDefaultAsync(item => item.IdUnidad == key);

            _context.Unidades.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Unidad model, IDictionary values) {
            string ID_UNIDAD = nameof(Unidad.IdUnidad);
            string NOMBRE = nameof(Unidad.Nombre);
            string DISPLAY_STRING = nameof(Unidad.DisplayString);
            string CHANGED_BY = nameof(Unidad.ChangedBy);
            string CHANGED_ON = nameof(Unidad.ChangedOn);
            string REMOVED = nameof(Unidad.Removed);
            string DESCRIPTION = nameof(Unidad.Description);

            if(values.Contains(ID_UNIDAD)) {
                model.IdUnidad = Convert.ToInt32(values[ID_UNIDAD]);
            }

            if(values.Contains(NOMBRE)) {
                model.Nombre = Convert.ToString(values[NOMBRE]);
            }

            if(values.Contains(DISPLAY_STRING)) {
                model.DisplayString = Convert.ToString(values[DISPLAY_STRING]);
            }

            if(values.Contains(CHANGED_BY)) {
                model.ChangedBy = Convert.ToString(values[CHANGED_BY]);
            }

            if(values.Contains(CHANGED_ON)) {
                model.ChangedOn = values[CHANGED_ON] != null ? Convert.ToDateTime(values[CHANGED_ON]) : (DateTime?)null;
            }

            if(values.Contains(REMOVED)) {
                model.Removed = Convert.ToString(values[REMOVED]);
            }

            if(values.Contains(DESCRIPTION)) {
                model.Description = Convert.ToString(values[DESCRIPTION]);
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