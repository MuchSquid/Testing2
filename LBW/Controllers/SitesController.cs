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
    public class SitesController : Controller
    {
        private LbwContext _context;

        public SitesController(LbwContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var sites = _context.Sites.Select(i => new {
                i.IdSite,
                i.NameSite,
                i.Compania
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdSite" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(sites, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Site();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Sites.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdSite });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Sites.FirstOrDefaultAsync(item => item.IdSite == key);
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
            var model = await _context.Sites.FirstOrDefaultAsync(item => item.IdSite == key);

            _context.Sites.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Site model, IDictionary values) {
            string ID_SITE = nameof(Site.IdSite);
            string NAME_SITE = nameof(Site.NameSite);
            string COMPANIA = nameof(Site.Compania);

            if(values.Contains(ID_SITE)) {
                model.IdSite = Convert.ToInt32(values[ID_SITE]);
            }

            if(values.Contains(NAME_SITE)) {
                model.NameSite = Convert.ToString(values[NAME_SITE]);
            }

            if(values.Contains(COMPANIA)) {
                model.Compania = Convert.ToString(values[COMPANIA]);
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