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
    public class ResultadoesController : Controller
    {
        private LbwContext _context;

        public ResultadoesController(LbwContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var resultados = _context.Resultados.Select(i => new {
                i.IdResult,
                i.IdSample,
                i.IdUnidad,
                i.IdComponent,
                i.IdAnalysis,
                i.SampleNumber,
                i.ResultNumber,
                i.OrderNum,
                i.AnalysisData,
                i.NameComponent,
                i.ReportedName,
                i.Status,
                i.Reportable,
                i.ChangedOn,
                i.Instrument
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "IdResult" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(resultados, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Resultado();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Resultados.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.IdResult });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Resultados.FirstOrDefaultAsync(item => item.IdResult == key);
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
            var model = await _context.Resultados.FirstOrDefaultAsync(item => item.IdResult == key);

            _context.Resultados.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> MuestrasLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Muestras
                         orderby i.SampleNumber
                         select new {
                             Value = i.IdSample,
                             Text = i.SampleNumber
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
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
        public async Task<IActionResult> UnidadesLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Unidades
                         orderby i.Nombre
                         select new {
                             Value = i.IdUnidad,
                             Text = i.Nombre
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(Resultado model, IDictionary values) {
            string ID_RESULT = nameof(Resultado.IdResult);
            string ID_SAMPLE = nameof(Resultado.IdSample);
            string ID_UNIDAD = nameof(Resultado.IdUnidad);
            string ID_COMPONENT = nameof(Resultado.IdComponent);
            string ID_ANALYSIS = nameof(Resultado.IdAnalysis);
            string SAMPLE_NUMBER = nameof(Resultado.SampleNumber);
            string RESULT_NUMBER = nameof(Resultado.ResultNumber);
            string ORDER_NUM = nameof(Resultado.OrderNum);
            string ANALYSIS_DATA = nameof(Resultado.AnalysisData);
            string NAME_COMPONENT = nameof(Resultado.NameComponent);
            string REPORTED_NAME = nameof(Resultado.ReportedName);
            string STATUS = nameof(Resultado.Status);
            string REPORTABLE = nameof(Resultado.Reportable);
            string CHANGED_ON = nameof(Resultado.ChangedOn);
            string INSTRUMENT = nameof(Resultado.Instrument);

            if(values.Contains(ID_RESULT)) {
                model.IdResult = Convert.ToInt32(values[ID_RESULT]);
            }

            if(values.Contains(ID_SAMPLE)) {
                model.IdSample = Convert.ToInt32(values[ID_SAMPLE]);
            }

            if(values.Contains(ID_UNIDAD)) {
                model.IdUnidad = Convert.ToInt32(values[ID_UNIDAD]);
            }

            if(values.Contains(ID_COMPONENT)) {
                model.IdComponent = Convert.ToInt32(values[ID_COMPONENT]);
            }

            if(values.Contains(ID_ANALYSIS)) {
                model.IdAnalysis = Convert.ToInt32(values[ID_ANALYSIS]);
            }

            if(values.Contains(SAMPLE_NUMBER)) {
                model.SampleNumber = Convert.ToString(values[SAMPLE_NUMBER]);
            }

            if(values.Contains(RESULT_NUMBER)) {
                model.ResultNumber = values[RESULT_NUMBER] != null ? Convert.ToDecimal(values[RESULT_NUMBER], CultureInfo.InvariantCulture) : (decimal?)null;
            }

            if(values.Contains(ORDER_NUM)) {
                model.OrderNum = values[ORDER_NUM] != null ? Convert.ToInt32(values[ORDER_NUM]) : (int?)null;
            }

            if(values.Contains(ANALYSIS_DATA)) {
                model.AnalysisData = Convert.ToString(values[ANALYSIS_DATA]);
            }

            if(values.Contains(NAME_COMPONENT)) {
                model.NameComponent = Convert.ToString(values[NAME_COMPONENT]);
            }

            if(values.Contains(REPORTED_NAME)) {
                model.ReportedName = Convert.ToString(values[REPORTED_NAME]);
            }

            if(values.Contains(STATUS)) {
                model.Status = Convert.ToString(values[STATUS]);
            }

            if(values.Contains(REPORTABLE)) {
                model.Reportable = Convert.ToString(values[REPORTABLE]);
            }

            if(values.Contains(CHANGED_ON)) {
                model.ChangedOn = values[CHANGED_ON] != null ? Convert.ToDateTime(values[CHANGED_ON]) : (DateTime?)null;
            }

            if(values.Contains(INSTRUMENT)) {
                model.Instrument = Convert.ToString(values[INSTRUMENT]);
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