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
                    var usuario = await db.Usuarios.FirstOrDefaultAsync(x=>x.IdUsuario== idusuario);
                    if(usuario==null)
                    {
                        response.Resp = false;
                        response.Message = "Usuario no válido en base de datos";
                        return response;
                    }

                    var today = DateTime.Now.ToLocalTime().ToString();
                    var insert = new Datospeso()
                    {
                        Peso = peso,
                        Fecha = DateTime.Parse(fecha),
                        Hora = TimeSpan.Parse(today),
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

    }
}
