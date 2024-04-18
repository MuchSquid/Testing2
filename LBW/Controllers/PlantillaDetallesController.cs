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
    public class PlantillaDetallesController : Controller
    {
        private LbwContext _context;

        public PlantillaDetallesController(LbwContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var plantilladetalles = _context.PlantillaDetalles.Select(i => new {
                i.Id_TLE,
                i.Id_TL,
                i.Id_Analysis,
                i.Name,
                i.Analysis,
                i.OrderNumber
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "Id_TLE" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(plantilladetalles, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new PlantillaDetalle();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.PlantillaDetalles.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.Id_TLE });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.PlantillaDetalles.FirstOrDefaultAsync(item => item.Id_TLE == key);
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
            var model = await _context.PlantillaDetalles.FirstOrDefaultAsync(item => item.Id_TLE == key);

            _context.PlantillaDetalles.Remove(model);
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
        public async Task<IActionResult> PlantillasLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Plantillas
                         orderby i.NameTlist
                         select new {
                             Value = i.IdTL,
                             Text = i.NameTlist
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(PlantillaDetalle model, IDictionary values) {
            string ID_TLE = nameof(PlantillaDetalle.Id_TLE);
            string ID_TL = nameof(PlantillaDetalle.Id_TL);
            string ID_ANALYSIS = nameof(PlantillaDetalle.Id_Analysis);
            string NAME = nameof(PlantillaDetalle.Name);
            string ANALYSIS = nameof(PlantillaDetalle.Analysis);
            string ORDER_NUMBER = nameof(PlantillaDetalle.OrderNumber);

            if(values.Contains(ID_TLE)) {
                model.Id_TLE = Convert.ToInt32(values[ID_TLE]);
            }

            if(values.Contains(ID_TL)) {
                model.Id_TL = Convert.ToInt32(values[ID_TL]);
            }

            if(values.Contains(ID_ANALYSIS)) {
                model.Id_Analysis = Convert.ToInt32(values[ID_ANALYSIS]);
            }

            if(values.Contains(NAME)) {
                model.Name = Convert.ToString(values[NAME]);
            }

            if(values.Contains(ANALYSIS)) {
                model.Analysis = Convert.ToString(values[ANALYSIS]);
            }

            if(values.Contains(ORDER_NUMBER)) {
                model.OrderNumber = values[ORDER_NUMBER] != null ? Convert.ToInt32(values[ORDER_NUMBER]) : (int?)null;
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