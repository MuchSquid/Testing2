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
    public class AnalisisController : Controller
    {
        private LbwContext _context;

        public AnalisisController(LbwContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var analisiss = _context.Analisiss.Select(i => new {
                i.IdAnalisis,
                i.IdTipoA,
                i.NameAnalisis,
                i.Version,
                i.Active,
                i.CommonName,
                i.Description,
                i.AliasName,
                i.ChangedOn,
                i.ChangedBy
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdAnalisis" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(analisiss, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Analisis();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Analisiss.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdAnalisis });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Analisiss.FirstOrDefaultAsync(item => item.IdAnalisis == key);
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
            var model = await _context.Analisiss.FirstOrDefaultAsync(item => item.IdAnalisis == key);

            _context.Analisiss.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> TipoAnalisissLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.TipoAnalisiss
                         orderby i.NombreA
                         select new {
                             Value = i.IdTipoA,
                             Text = i.NombreA
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(Analisis model, IDictionary values) {
            string ID_ANALISIS = nameof(Analisis.IdAnalisis);
            string ID_TIPO_A = nameof(Analisis.IdTipoA);
            string NAME_ANALISIS = nameof(Analisis.NameAnalisis);
            string VERSION = nameof(Analisis.Version);
            string ACTIVE = nameof(Analisis.Active);
            string COMMON_NAME = nameof(Analisis.CommonName);
            string DESCRIPTION = nameof(Analisis.Description);
            string ALIAS_NAME = nameof(Analisis.AliasName);
            string CHANGED_ON = nameof(Analisis.ChangedOn);
            string CHANGED_BY = nameof(Analisis.ChangedBy);

            if(values.Contains(ID_ANALISIS)) {
                model.IdAnalisis = Convert.ToInt32(values[ID_ANALISIS]);
            }

            if(values.Contains(ID_TIPO_A)) {
                model.IdTipoA = Convert.ToInt32(values[ID_TIPO_A]);
            }

            if(values.Contains(NAME_ANALISIS)) {
                model.NameAnalisis = Convert.ToString(values[NAME_ANALISIS]);
            }

            if(values.Contains(VERSION)) {
                model.Version = values[VERSION] != null ? Convert.ToInt32(values[VERSION]) : (int?)null;
            }

            if(values.Contains(ACTIVE)) {
                model.Active = values[ACTIVE] != null ? Convert.ToBoolean(values[ACTIVE]) : (bool?)null;
            }

            if(values.Contains(COMMON_NAME)) {
                model.CommonName = Convert.ToString(values[COMMON_NAME]);
            }

            if(values.Contains(DESCRIPTION)) {
                model.Description = Convert.ToString(values[DESCRIPTION]);
            }

            if(values.Contains(ALIAS_NAME)) {
                model.AliasName = Convert.ToString(values[ALIAS_NAME]);
            }

            if(values.Contains(CHANGED_ON)) {
                model.ChangedOn = values[CHANGED_ON] != null ? Convert.ToDateTime(values[CHANGED_ON]) : (DateTime?)null;
            }

            if(values.Contains(CHANGED_BY)) {
                model.ChangedBy = Convert.ToString(values[CHANGED_BY]);
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