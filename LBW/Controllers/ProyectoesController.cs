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
    public class ProyectoesController : Controller
    {
        private LbwContext _context;

        public ProyectoesController(LbwContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var proyectos = _context.Proyectos.Select(i => new {
                i.IdProyecto,
                i.ID_TL,
                i.ID_Cliente,
                i.Name,
                i.TemplateVersion,
                i.Description,
                i.Note,
                i.Status,
                i.DateCreated,
                i.DateRecieved,
                i.DateStarted,
                i.DateCompleted,
                i.DateReviewed,
                i.DateUpdated,
                i.Owner,
                i.NumSamples
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdProyecto" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(proyectos, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Proyecto();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Proyectos.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdProyecto });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Proyectos.FirstOrDefaultAsync(item => item.IdProyecto == key);
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
            var model = await _context.Proyectos.FirstOrDefaultAsync(item => item.IdProyecto == key);

            _context.Proyectos.Remove(model);
            await _context.SaveChangesAsync();
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

        private void PopulateModel(Proyecto model, IDictionary values) {
            string ID_PROYECTO = nameof(Proyecto.IdProyecto);
            string ID_TL = nameof(Proyecto.ID_TL);
            string ID_CLIENTE = nameof(Proyecto.ID_Cliente);
            string NAME = nameof(Proyecto.Name);
            string TEMPLATE_VERSION = nameof(Proyecto.TemplateVersion);
            string DESCRIPTION = nameof(Proyecto.Description);
            string NOTE = nameof(Proyecto.Note);
            string STATUS = nameof(Proyecto.Status);
            string DATE_CREATED = nameof(Proyecto.DateCreated);
            string DATE_RECIEVED = nameof(Proyecto.DateRecieved);
            string DATE_STARTED = nameof(Proyecto.DateStarted);
            string DATE_COMPLETED = nameof(Proyecto.DateCompleted);
            string DATE_REVIEWED = nameof(Proyecto.DateReviewed);
            string DATE_UPDATED = nameof(Proyecto.DateUpdated);
            string OWNER = nameof(Proyecto.Owner);
            string NUM_SAMPLES = nameof(Proyecto.NumSamples);

            if(values.Contains(ID_PROYECTO)) {
                model.IdProyecto = Convert.ToInt32(values[ID_PROYECTO]);
            }

            if(values.Contains(ID_TL)) {
                model.ID_TL = Convert.ToInt32(values[ID_TL]);
            }

            if(values.Contains(ID_CLIENTE)) {
                model.ID_Cliente = Convert.ToInt32(values[ID_CLIENTE]);
            }

            if(values.Contains(NAME)) {
                model.Name = Convert.ToString(values[NAME]);
            }

            if(values.Contains(TEMPLATE_VERSION)) {
                model.TemplateVersion = values[TEMPLATE_VERSION] != null ? Convert.ToInt32(values[TEMPLATE_VERSION]) : (int?)null;
            }

            if(values.Contains(DESCRIPTION)) {
                model.Description = Convert.ToString(values[DESCRIPTION]);
            }

            if(values.Contains(NOTE)) {
                model.Note = Convert.ToString(values[NOTE]);
            }

            if(values.Contains(STATUS)) {
                model.Status = Convert.ToString(values[STATUS]);
            }

            if(values.Contains(DATE_CREATED)) {
                model.DateCreated = values[DATE_CREATED] != null ? Convert.ToDateTime(values[DATE_CREATED]) : (DateTime?)null;
            }

            if(values.Contains(DATE_RECIEVED)) {
                model.DateRecieved = values[DATE_RECIEVED] != null ? Convert.ToDateTime(values[DATE_RECIEVED]) : (DateTime?)null;
            }

            if(values.Contains(DATE_STARTED)) {
                model.DateStarted = values[DATE_STARTED] != null ? Convert.ToDateTime(values[DATE_STARTED]) : (DateTime?)null;
            }

            if(values.Contains(DATE_COMPLETED)) {
                model.DateCompleted = values[DATE_COMPLETED] != null ? Convert.ToDateTime(values[DATE_COMPLETED]) : (DateTime?)null;
            }

            if(values.Contains(DATE_REVIEWED)) {
                model.DateReviewed = values[DATE_REVIEWED] != null ? Convert.ToDateTime(values[DATE_REVIEWED]) : (DateTime?)null;
            }

            if(values.Contains(DATE_UPDATED)) {
                model.DateUpdated = values[DATE_UPDATED] != null ? Convert.ToDateTime(values[DATE_UPDATED]) : (DateTime?)null;
            }

            if(values.Contains(OWNER)) {
                model.Owner = Convert.ToString(values[OWNER]);
            }

            if(values.Contains(NUM_SAMPLES)) {
                model.NumSamples = values[NUM_SAMPLES] != null ? Convert.ToInt32(values[NUM_SAMPLES]) : (int?)null;
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