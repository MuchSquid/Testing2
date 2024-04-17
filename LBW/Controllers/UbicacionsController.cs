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
    public class UbicacionsController : Controller
    {
        private LbwContext _context;

        public UbicacionsController(LbwContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var ubicaciones = _context.Ubicaciones.Select(i => new {
                i.ID_LOCATION,
                i.Name_location,
                i.Description,
                i.Address,
                i.Contact
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "ID_LOCATION" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(ubicaciones, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Ubicacion();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Ubicaciones.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.ID_LOCATION });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Ubicaciones.FirstOrDefaultAsync(item => item.ID_LOCATION == key);
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
            var model = await _context.Ubicaciones.FirstOrDefaultAsync(item => item.ID_LOCATION == key);

            _context.Ubicaciones.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Ubicacion model, IDictionary values) {
            string ID_LOCATION = nameof(Ubicacion.ID_LOCATION);
            string NAME_LOCATION = nameof(Ubicacion.Name_location);
            string DESCRIPTION = nameof(Ubicacion.Description);
            string ADDRESS = nameof(Ubicacion.Address);
            string CONTACT = nameof(Ubicacion.Contact);

            if(values.Contains(ID_LOCATION)) {
                model.ID_LOCATION = Convert.ToInt32(values[ID_LOCATION]);
            }

            if(values.Contains(NAME_LOCATION)) {
                model.Name_location = Convert.ToString(values[NAME_LOCATION]);
            }

            if(values.Contains(DESCRIPTION)) {
                model.Description = Convert.ToString(values[DESCRIPTION]);
            }

            if(values.Contains(ADDRESS)) {
                model.Address = Convert.ToString(values[ADDRESS]);
            }

            if(values.Contains(CONTACT)) {
                model.Contact = Convert.ToString(values[CONTACT]);
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