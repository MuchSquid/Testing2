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
    public class ListasController : Controller
    {
        private LbwContext _context;

        public ListasController(LbwContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var listas = _context.Listas.Select(i => new {
                i.IdLista,
                i.List,
                i.NameLista,
                i.Value,
                i.OrderNumber
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdLista" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(listas, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Lista();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Listas.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdLista });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Listas.FirstOrDefaultAsync(item => item.IdLista == key);
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
            var model = await _context.Listas.FirstOrDefaultAsync(item => item.IdLista == key);

            _context.Listas.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Lista model, IDictionary values) {
            string ID_LISTA = nameof(Lista.IdLista);
            string LIST = nameof(Lista.List);
            string NAME_LISTA = nameof(Lista.NameLista);
            string VALUE = nameof(Lista.Value);
            string ORDER_NUMBER = nameof(Lista.OrderNumber);

            if(values.Contains(ID_LISTA)) {
                model.IdLista = Convert.ToInt32(values[ID_LISTA]);
            }

            if(values.Contains(LIST)) {
                model.List = Convert.ToString(values[LIST]);
            }

            if(values.Contains(NAME_LISTA)) {
                model.NameLista = Convert.ToString(values[NAME_LISTA]);
            }

            if(values.Contains(VALUE)) {
                model.Value = Convert.ToString(values[VALUE]);
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