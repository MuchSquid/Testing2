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
    public class AnalisisDetallesController : Controller
    {
        private LbwContext _context;

        public AnalisisDetallesController(LbwContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var analisisdetalles = _context.AnalisisDetalles.Select(i => new {
                i.IdComp,
                i.IdAnalisis,
                i.IdUnidad,
                i.NameComponent,
                i.Version,
                i.AnalisisData,
                i.Units,
                i.Minimun,
                i.Maximun,
                i.Reportable,
                i.ClampLow,
                i.ClampHigh
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdComp" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(analisisdetalles, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new AnalisisDetalle();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.AnalisisDetalles.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdComp });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.AnalisisDetalles.FirstOrDefaultAsync(item => item.IdComp == key);
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
            var model = await _context.AnalisisDetalles.FirstOrDefaultAsync(item => item.IdComp == key);

            _context.AnalisisDetalles.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> AnalisissLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Analisiss
                         orderby i.NameAnalisis
                         select new {
                             Value = i.IdAnalisis,
                             Text = i.NameAnalisis
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> UnidadesLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Unidades
                         orderby i.Nombre
                         select new {
                             Value = i.IdUnidad,
                             Text = i.Nombre
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(AnalisisDetalle model, IDictionary values) {
            string ID_COMP = nameof(AnalisisDetalle.IdComp);
            string ID_ANALISIS = nameof(AnalisisDetalle.IdAnalisis);
            string ID_UNIDAD = nameof(AnalisisDetalle.IdUnidad);
            string NAME_COMPONENT = nameof(AnalisisDetalle.NameComponent);
            string VERSION = nameof(AnalisisDetalle.Version);
            string ANALISIS_DATA = nameof(AnalisisDetalle.AnalisisData);
            string UNITS = nameof(AnalisisDetalle.Units);
            string MINIMUN = nameof(AnalisisDetalle.Minimun);
            string MAXIMUN = nameof(AnalisisDetalle.Maximun);
            string REPORTABLE = nameof(AnalisisDetalle.Reportable);
            string CLAMP_LOW = nameof(AnalisisDetalle.ClampLow);
            string CLAMP_HIGH = nameof(AnalisisDetalle.ClampHigh);

            if(values.Contains(ID_COMP)) {
                model.IdComp = Convert.ToInt32(values[ID_COMP]);
            }

            if(values.Contains(ID_ANALISIS)) {
                model.IdAnalisis = Convert.ToInt32(values[ID_ANALISIS]);
            }

            if(values.Contains(ID_UNIDAD)) {
                model.IdUnidad = Convert.ToInt32(values[ID_UNIDAD]);
            }

            if(values.Contains(NAME_COMPONENT)) {
                model.NameComponent = Convert.ToString(values[NAME_COMPONENT]);
            }

            if(values.Contains(VERSION)) {
                model.Version = values[VERSION] != null ? Convert.ToInt32(values[VERSION]) : (int?)null;
            }

            if(values.Contains(ANALISIS_DATA)) {
                model.AnalisisData = Convert.ToString(values[ANALISIS_DATA]);
            }

            if(values.Contains(UNITS)) {
                model.Units = Convert.ToString(values[UNITS]);
            }

            if(values.Contains(MINIMUN)) {
                model.Minimun = values[MINIMUN] != null ? Convert.ToInt32(values[MINIMUN]) : (int?)null;
            }

            if(values.Contains(MAXIMUN)) {
                model.Maximun = values[MAXIMUN] != null ? Convert.ToInt32(values[MAXIMUN]) : (int?)null;
            }

            if(values.Contains(REPORTABLE)) {
                model.Reportable = values[REPORTABLE] != null ? Convert.ToBoolean(values[REPORTABLE]) : (bool?)null;
            }

            if(values.Contains(CLAMP_LOW)) {
                model.ClampLow = Convert.ToString(values[CLAMP_LOW]);
            }

            if(values.Contains(CLAMP_HIGH)) {
                model.ClampHigh = Convert.ToString(values[CLAMP_HIGH]);
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