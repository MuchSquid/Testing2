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
    public class PlantasController : Controller
    {
        private LbwContext _context;

        public PlantasController(LbwContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var plantas = _context.Plantas.Select(i => new {
                i.IdPlanta,
                i.IdCliente,
                i.IdSite,
                i.NamePl,
                i.ChangedBy,
                i.ChangedOn,
                i.Removed,
                i.Description
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdPlanta" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(plantas, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Planta();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Plantas.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdPlanta });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Plantas.FirstOrDefaultAsync(item => item.IdPlanta == key);
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
            var model = await _context.Plantas.FirstOrDefaultAsync(item => item.IdPlanta == key);

            _context.Plantas.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> SitesLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Sites
                         orderby i.NameSite
                         select new {
                             Value = i.IdSite,
                             Text = i.NameSite
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
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

        private void PopulateModel(Planta model, IDictionary values) {
            string ID_PLANTA = nameof(Planta.IdPlanta);
            string ID_CLIENTE = nameof(Planta.IdCliente);
            string ID_SITE = nameof(Planta.IdSite);
            string NAME_PL = nameof(Planta.NamePl);
            string CHANGED_BY = nameof(Planta.ChangedBy);
            string CHANGED_ON = nameof(Planta.ChangedOn);
            string REMOVED = nameof(Planta.Removed);
            string DESCRIPTION = nameof(Planta.Description);

            if(values.Contains(ID_PLANTA)) {
                model.IdPlanta = Convert.ToInt32(values[ID_PLANTA]);
            }

            if(values.Contains(ID_CLIENTE)) {
                model.IdCliente = Convert.ToInt32(values[ID_CLIENTE]);
            }

            if(values.Contains(ID_SITE)) {
                model.IdSite = Convert.ToInt32(values[ID_SITE]);
            }

            if(values.Contains(NAME_PL)) {
                model.NamePl = Convert.ToString(values[NAME_PL]);
            }

            if(values.Contains(CHANGED_BY)) {
                model.ChangedBy = Convert.ToString(values[CHANGED_BY]);
            }

            if(values.Contains(CHANGED_ON)) {
                model.ChangedOn = values[CHANGED_ON] != null ? Convert.ToDateTime(values[CHANGED_ON]) : (DateTime?)null;
            }

            if(values.Contains(REMOVED)) {
                model.Removed = values[REMOVED] != null ? Convert.ToBoolean(values[REMOVED]) : (bool?)null;
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