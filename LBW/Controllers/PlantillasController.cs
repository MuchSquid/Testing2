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
    public class PlantillasController : Controller
    {
        private LbwContext _context;

        public PlantillasController(LbwContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var plantillas = _context.Plantillas.Select(i => new {
                i.IdTL,
                i.IdCliente,
                i.NameTlist,
                i.Description,
                i.ChangedOn,
                i.ChangedBy,
                i.Removed
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdTL" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(plantillas, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Plantilla();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Plantillas.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdTL });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Plantillas.FirstOrDefaultAsync(item => item.IdTL == key);
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
            var model = await _context.Plantillas.FirstOrDefaultAsync(item => item.IdTL == key);

            _context.Plantillas.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> ClientesLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Clientes
                         orderby i.NameCliente
                         select new {
                             Value = i.IdCliente,
                             Text = i.NameCliente
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(Plantilla model, IDictionary values) {
            string ID_TL = nameof(Plantilla.IdTL);
            string ID_CLIENTE = nameof(Plantilla.IdCliente);
            string NAME_TLIST = nameof(Plantilla.NameTlist);
            string DESCRIPTION = nameof(Plantilla.Description);
            string CHANGED_ON = nameof(Plantilla.ChangedOn);
            string CHANGED_BY = nameof(Plantilla.ChangedBy);
            string REMOVED = nameof(Plantilla.Removed);

            if(values.Contains(ID_TL)) {
                model.IdTL = Convert.ToInt32(values[ID_TL]);
            }

            if(values.Contains(ID_CLIENTE)) {
                model.IdCliente = Convert.ToInt32(values[ID_CLIENTE]);
            }

            if(values.Contains(NAME_TLIST)) {
                model.NameTlist = Convert.ToString(values[NAME_TLIST]);
            }

            if(values.Contains(DESCRIPTION)) {
                model.Description = Convert.ToString(values[DESCRIPTION]);
            }

            if(values.Contains(CHANGED_ON)) {
                model.ChangedOn = values[CHANGED_ON] != null ? Convert.ToDateTime(values[CHANGED_ON]) : (DateTime?)null;
            }

            if(values.Contains(CHANGED_BY)) {
                model.ChangedBy = Convert.ToString(values[CHANGED_BY]);
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