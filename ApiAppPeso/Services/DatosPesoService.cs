using ApiAppPeso.Models;
using ApiAppPeso.Utils.Helpers;
using Datos.Operations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAppPeso.Services
{
    
    public class DatosPesoService
    {
        private static readonly NLog.ILogger log = NLog.LogManager.GetCurrentClassLogger();

        public async Task<ActionResult<StandardResponseModel>> insertDatoPeso(DatoPesoInsert request)
        {
            StandardResponseModel response = new StandardResponseModel();
            try
            {
                log.Info("insertDatoPeso Request:" + JsonConvert.SerializeObject(request));
                if(request.Peso <=0){
                    log.Error("insertDatoPeso" + "404 : Peso No válido");
                    throw new KeyNotFoundException("Peso No válido");
                }
                if(DateTime.Parse(request.Fecha) < DateTime.Now.Date){
                    log.Error("insertDatoPeso" + "404: Fecha no válida");
                    throw new KeyNotFoundException("404: Fecha no válida");
                }
                float peso= request.Peso;
                string date= request.Fecha;
                long user= request.Idusuario;
                DatosPesoDatos operation = new();
                var respBase= await operation.Insert(peso,date,user);
                if(!respBase.Resp){
                    log.Error("insertDatoPeso BaseDatos" + "404:"+respBase.Message);
                    throw new KeyNotFoundException("404:"+ respBase.Message);
                }
                else {
                    response.StatusCode = 200;
                    response.Message = respBase.Message;
                    return response;
                }
            }
            catch (Exception ex)
            {
                log.Error("insertDatoPeso" + "500:" + ex.ToString());
                throw;
            }

        }



    }
}
