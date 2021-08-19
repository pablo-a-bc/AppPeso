using Datos.Datos;
using Datos.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Datos.Operations
{
    public class DatosPesoDatos
    {
    
       public async Task<ResponseDataBase>Insert(float peso, string fecha,long idusuario)
       {
            ResponseDataBase response = new();
            try
            {
                using (app_pesoContext db = new())
                {
                    var usuario = Usuariovalido(idusuario);
                    if(usuario==false)
                    {
                        response.Resp = false;
                        response.Message = "Usuario no válido en base de datos";
                        return response;
                    }
                    string hourMinute = DateTime.Now.ToString("HH:mm");
                    var insert = new Datospeso()
                    {
                        Peso = peso,
                        Fecha = DateTime.Parse(fecha),
                        Hora = TimeSpan.Parse(hourMinute),
                        UsuarioIdusuario = idusuario,
                    };
                    await db.AddAsync(insert);
                    await db.SaveChangesAsync();
                    //reviso inserción en la base
                    var search = await db.Datospesos.FirstOrDefaultAsync(x => x.IdDatoPeso == insert.IdDatoPeso);
                    if(search==null)
                    {
                        response.Resp = false;
                        response.Message = "No se insertaron los datos";
                        return response;
                    }
                    response.Resp = true;
                    response.Message = "Insertado";
                    return response;
                };
            }
            catch (Exception ex)
            {

                response.Resp = false;
                response.Message = ex.ToString();
                return response;
            }
         

       }

        public bool Usuariovalido(long idsusuario)
        {
            using (app_pesoContext db = new())
            {
                var usuario = db.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == idsusuario);
                if (usuario == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }


            };
        }


        public async Task<ResponsePesoMinMax> PesoMinMax(long idusuario)
        {
            ResponsePesoMinMax response = new();
            ResponseDataBase resp = new();
            try
            {
                using (app_pesoContext db = new())
                {

                    var usuario = Usuariovalido(idusuario);
                    if (usuario == false)
                    {
                        resp.Resp = false;
                        resp.Message = "Usuario no válido en base de datos";
                        response.Response = resp;
                        return response;
                    }

                    var pesousuario = await db.Datospesos.Where(x => x.UsuarioIdusuario == idusuario).ToListAsync();
                    if(pesousuario.Count==0)
                    {

                        resp.Resp = false;
                        resp.Message = "Usuario sin pesos registrados";
                        response.Response = resp;
                        return response;
                    }
                    var min = pesousuario.Min(x => x.Fecha);
                    var max = pesousuario.Max(x => x.Fecha);
                    var searchfirstweight = pesousuario.FirstOrDefault(x => x.Fecha == min);
                    var searchlastweight = pesousuario.FirstOrDefault(x => x.Fecha == max);
                    float firstweight = (float)searchfirstweight.Peso;
                    float lastweight = (float)searchlastweight.Peso;
                    float dif = firstweight - lastweight;
                    response.PesoMin = firstweight;
                    response.PesoMax = lastweight;
                    response.DifPeso = dif;
                    resp.Resp = true;
                    resp.Message = "Ok";
                    response.Response = resp;
                    return response;
                };
            }
            catch (Exception ex)
            {

                resp.Message = ex.ToString();
                resp.Resp = false;
                response.Response = resp;
                return response;
            }
        }

    }
}
