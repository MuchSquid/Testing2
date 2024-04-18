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
    public class MuestrasController : Controller
    {
        private LbwContext _context;

        public MuestrasController(LbwContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var muestras = _context.Muestras.Select(i => new {
                i.IdSample,
                i.IdPm,
                i.IdCliente,
                i.IdLocation,
                i.SampleNumber,
                i.TextID,
                i.Status,
                i.ChangedOn,
                i.OriginalSample,
                i.LoginDate,
                i.LoginBy,
                i.SampleDate,
                i.RecdDate,
                i.ReceivedBy,
                i.DateStarted,
                i.DueDate,
                i.DateCompleted,
                i.DateReviewed,
                i.PreBy,
                i.Reviewer,
                i.SamplingPoint,
                i.SampleType,
                i.IdProject,
                i.SampleName,
                i.Location,
                i.Customer
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdSample" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(muestras, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Muestra();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Muestras.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdSample });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Muestras.FirstOrDefaultAsync(item => item.IdSample == key);
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
            var model = await _context.Muestras.FirstOrDefaultAsync(item => item.IdSample == key);

            _context.Muestras.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> UbicacionesLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Ubicaciones
                         orderby i.Name_location
                         select new {
                             Value = i.ID_LOCATION,
                             Text = i.Name_location
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> PuntoMuestrasLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.PuntoMuestras
                         orderby i.NamePm
                         select new {
                             Value = i.IdPm,
                             Text = i.NamePm
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> ProyectosLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Proyectos
                         orderby i.Name
                         select new {
                             Value = i.IdProyecto,
                             Text = i.Name
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

        private void PopulateModel(Muestra model, IDictionary values) {
            string ID_SAMPLE = nameof(Muestra.IdSample);
            string ID_PM = nameof(Muestra.IdPm);
            string ID_CLIENTE = nameof(Muestra.IdCliente);
            string ID_LOCATION = nameof(Muestra.IdLocation);
            string SAMPLE_NUMBER = nameof(Muestra.SampleNumber);
            string TEXT_ID = nameof(Muestra.TextID);
            string STATUS = nameof(Muestra.Status);
            string CHANGED_ON = nameof(Muestra.ChangedOn);
            string ORIGINAL_SAMPLE = nameof(Muestra.OriginalSample);
            string LOGIN_DATE = nameof(Muestra.LoginDate);
            string LOGIN_BY = nameof(Muestra.LoginBy);
            string SAMPLE_DATE = nameof(Muestra.SampleDate);
            string RECD_DATE = nameof(Muestra.RecdDate);
            string RECEIVED_BY = nameof(Muestra.ReceivedBy);
            string DATE_STARTED = nameof(Muestra.DateStarted);
            string DUE_DATE = nameof(Muestra.DueDate);
            string DATE_COMPLETED = nameof(Muestra.DateCompleted);
            string DATE_REVIEWED = nameof(Muestra.DateReviewed);
            string PRE_BY = nameof(Muestra.PreBy);
            string REVIEWER = nameof(Muestra.Reviewer);
            string SAMPLING_POINT = nameof(Muestra.SamplingPoint);
            string SAMPLE_TYPE = nameof(Muestra.SampleType);
            string ID_PROJECT = nameof(Muestra.IdProject);
            string SAMPLE_NAME = nameof(Muestra.SampleName);
            string LOCATION = nameof(Muestra.Location);
            string CUSTOMER = nameof(Muestra.Customer);

            if(values.Contains(ID_SAMPLE)) {
                model.IdSample = Convert.ToInt32(values[ID_SAMPLE]);
            }

            if(values.Contains(ID_PM)) {
                model.IdPm = Convert.ToInt32(values[ID_PM]);
            }

            if(values.Contains(ID_CLIENTE)) {
                model.IdCliente = Convert.ToInt32(values[ID_CLIENTE]);
            }

            if(values.Contains(ID_LOCATION)) {
                model.IdLocation = Convert.ToInt32(values[ID_LOCATION]);
            }

            if(values.Contains(SAMPLE_NUMBER)) {
                model.SampleNumber = Convert.ToString(values[SAMPLE_NUMBER]);
            }

            if(values.Contains(TEXT_ID)) {
                model.TextID = Convert.ToString(values[TEXT_ID]);
            }

            if(values.Contains(STATUS)) {
                model.Status = Convert.ToString(values[STATUS]);
            }

            if(values.Contains(CHANGED_ON)) {
                model.ChangedOn = values[CHANGED_ON] != null ? Convert.ToDateTime(values[CHANGED_ON]) : (DateTime?)null;
            }

            if(values.Contains(ORIGINAL_SAMPLE)) {
                model.OriginalSample = values[ORIGINAL_SAMPLE] != null ? Convert.ToInt32(values[ORIGINAL_SAMPLE]) : (int?)null;
            }

            if(values.Contains(LOGIN_DATE)) {
                model.LoginDate = values[LOGIN_DATE] != null ? Convert.ToDateTime(values[LOGIN_DATE]) : (DateTime?)null;
            }

            if(values.Contains(LOGIN_BY)) {
                model.LoginBy = Convert.ToString(values[LOGIN_BY]);
            }

            if(values.Contains(SAMPLE_DATE)) {
                model.SampleDate = values[SAMPLE_DATE] != null ? Convert.ToDateTime(values[SAMPLE_DATE]) : (DateTime?)null;
            }

            if(values.Contains(RECD_DATE)) {
                model.RecdDate = values[RECD_DATE] != null ? Convert.ToDateTime(values[RECD_DATE]) : (DateTime?)null;
            }

            if(values.Contains(RECEIVED_BY)) {
                model.ReceivedBy = Convert.ToString(values[RECEIVED_BY]);
            }

            if(values.Contains(DATE_STARTED)) {
                model.DateStarted = values[DATE_STARTED] != null ? Convert.ToDateTime(values[DATE_STARTED]) : (DateTime?)null;
            }

            if(values.Contains(DUE_DATE)) {
                model.DueDate = values[DUE_DATE] != null ? Convert.ToDateTime(values[DUE_DATE]) : (DateTime?)null;
            }

            if(values.Contains(DATE_COMPLETED)) {
                model.DateCompleted = values[DATE_COMPLETED] != null ? Convert.ToDateTime(values[DATE_COMPLETED]) : (DateTime?)null;
            }

            if(values.Contains(DATE_REVIEWED)) {
                model.DateReviewed = values[DATE_REVIEWED] != null ? Convert.ToDateTime(values[DATE_REVIEWED]) : (DateTime?)null;
            }

            if(values.Contains(PRE_BY)) {
                model.PreBy = Convert.ToString(values[PRE_BY]);
            }

            if(values.Contains(REVIEWER)) {
                model.Reviewer = Convert.ToString(values[REVIEWER]);
            }

            if(values.Contains(SAMPLING_POINT)) {
                model.SamplingPoint = Convert.ToString(values[SAMPLING_POINT]);
            }

            if(values.Contains(SAMPLE_TYPE)) {
                model.SampleType = Convert.ToString(values[SAMPLE_TYPE]);
            }

            if(values.Contains(ID_PROJECT)) {
                model.IdProject = Convert.ToInt32(values[ID_PROJECT]);
            }

            if(values.Contains(SAMPLE_NAME)) {
                model.SampleName = Convert.ToString(values[SAMPLE_NAME]);
            }

            if(values.Contains(LOCATION)) {
                model.Location = Convert.ToString(values[LOCATION]);
            }

            if(values.Contains(CUSTOMER)) {
                model.Customer = Convert.ToString(values[CUSTOMER]);
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