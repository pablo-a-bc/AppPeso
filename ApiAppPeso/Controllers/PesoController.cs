using ApiAppPeso.Models;
using ApiAppPeso.Services;
using ApiAppPeso.Utils.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAppPeso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PesoController : ControllerBase
    {
        readonly DatosPesoService Service = new();
        [HttpPost("insertpeso")]
        public async Task<ActionResult<StandardResponseModel>> BatchUsabilidad(DatoPesoInsert entrada)
        {
            ActionResult<StandardResponseModel> response = await Service.insertDatoPeso(entrada);
            return response;
        }
    }
}
