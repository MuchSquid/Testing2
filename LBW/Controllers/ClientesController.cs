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
    public class ClientesController : Controller
    {
        private LbwContext _context;

        public ClientesController(LbwContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var clientes = _context.Clientes.Select(i => new {
                i.IdCliente,
                i.IdSite,
                i.NameCliente,
                i.Description,
                i.Address,
                i.Contact,
                i.ChangedOn,
                i.ChangedBy,
                i.EmailAddrs,
                i.C_ClientesAgua
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdCliente" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(clientes, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Cliente();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Clientes.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdCliente });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Clientes.FirstOrDefaultAsync(item => item.IdCliente == key);
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
            var model = await _context.Clientes.FirstOrDefaultAsync(item => item.IdCliente == key);

            _context.Clientes.Remove(model);
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

        private void PopulateModel(Cliente model, IDictionary values) {
            string ID_CLIENTE = nameof(Cliente.IdCliente);
            string ID_SITE = nameof(Cliente.IdSite);
            string NAME_CLIENTE = nameof(Cliente.NameCliente);
            string DESCRIPTION = nameof(Cliente.Description);
            string ADDRESS = nameof(Cliente.Address);
            string CONTACT = nameof(Cliente.Contact);
            string CHANGED_ON = nameof(Cliente.ChangedOn);
            string CHANGED_BY = nameof(Cliente.ChangedBy);
            string EMAIL_ADDRS = nameof(Cliente.EmailAddrs);
            string C_CLIENTES_AGUA = nameof(Cliente.C_ClientesAgua);

            if(values.Contains(ID_CLIENTE)) {
                model.IdCliente = Convert.ToInt32(values[ID_CLIENTE]);
            }

            if(values.Contains(ID_SITE)) {
                model.IdSite = Convert.ToInt32(values[ID_SITE]);
            }

            if(values.Contains(NAME_CLIENTE)) {
                model.NameCliente = Convert.ToString(values[NAME_CLIENTE]);
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

            if(values.Contains(CHANGED_ON)) {
                model.ChangedOn = values[CHANGED_ON] != null ? Convert.ToDateTime(values[CHANGED_ON]) : (DateTime?)null;
            }

            if(values.Contains(CHANGED_BY)) {
                model.ChangedBy = Convert.ToString(values[CHANGED_BY]);
            }

            if(values.Contains(EMAIL_ADDRS)) {
                model.EmailAddrs = Convert.ToString(values[EMAIL_ADDRS]);
            }

            if(values.Contains(C_CLIENTES_AGUA)) {
                model.C_ClientesAgua = Convert.ToString(values[C_CLIENTES_AGUA]);
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