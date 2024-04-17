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
    public class PuntoMuestrasController : Controller
    {
        private LbwContext _context;

        public PuntoMuestrasController(LbwContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var puntomuestras = _context.PuntoMuestras.Select(i => new {
                i.IdPm,
                i.IdPlanta,
                i.NamePm,
                i.ChangedBy,
                i.ChangedOn,
                i.Description,
                i.C_CodPunto
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdPm" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(puntomuestras, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new PuntoMuestra();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.PuntoMuestras.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdPm });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.PuntoMuestras.FirstOrDefaultAsync(item => item.IdPm == key);
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
            var model = await _context.PuntoMuestras.FirstOrDefaultAsync(item => item.IdPm == key);

            _context.PuntoMuestras.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> PlantasLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Plantas
                         orderby i.NamePl
                         select new {
                             Value = i.IdPlanta,
                             Text = i.NamePl
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(PuntoMuestra model, IDictionary values) {
            string ID_PM = nameof(PuntoMuestra.IdPm);
            string ID_PLANTA = nameof(PuntoMuestra.IdPlanta);
            string NAME_PM = nameof(PuntoMuestra.NamePm);
            string CHANGED_BY = nameof(PuntoMuestra.ChangedBy);
            string CHANGED_ON = nameof(PuntoMuestra.ChangedOn);
            string DESCRIPTION = nameof(PuntoMuestra.Description);
            string C_COD_PUNTO = nameof(PuntoMuestra.C_CodPunto);

            if(values.Contains(ID_PM)) {
                model.IdPm = Convert.ToInt32(values[ID_PM]);
            }

            if(values.Contains(ID_PLANTA)) {
                model.IdPlanta = Convert.ToInt32(values[ID_PLANTA]);
            }

            if(values.Contains(NAME_PM)) {
                model.NamePm = Convert.ToString(values[NAME_PM]);
            }

            if(values.Contains(CHANGED_BY)) {
                model.ChangedBy = Convert.ToString(values[CHANGED_BY]);
            }

            if(values.Contains(CHANGED_ON)) {
                model.ChangedOn = values[CHANGED_ON] != null ? Convert.ToDateTime(values[CHANGED_ON]) : (DateTime?)null;
            }

            if(values.Contains(DESCRIPTION)) {
                model.Description = Convert.ToString(values[DESCRIPTION]);
            }

            if(values.Contains(C_COD_PUNTO)) {
                model.C_CodPunto = Convert.ToString(values[C_COD_PUNTO]);
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