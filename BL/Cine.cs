using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Cine
    {
        public static ML.Result Add(ML.Cine cine)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezCineContext context = new DL.JsanchezCineContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"CineAdd " +
                        $"'{cine.Latitud}'," +
                        $"'{cine.Longitud}'," +
                        $"'{cine.Direccion}'," +
                        $"'{cine.Venta}'," +
                        $"'{cine.Zona.IdZona}'");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se inserto el registro";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Update(ML.Cine cine)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezCineContext context = new DL.JsanchezCineContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"CineUpdate " +
                        $"'{cine.IdCine}'," +
                        $"'{cine.Latitud}'," +
                        $"'{cine.Longitud}'," +
                        $"'{cine.Direccion}'," +
                        $"'{cine.Venta}'," +
                        $"'{cine.Zona.IdZona}'");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se Actualizo el registro";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Delete(ML.Cine cine)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezCineContext context = new DL.JsanchezCineContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"CineDelete '{cine.IdCine}'");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se Actualizo el registro";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezCineContext context = new DL.JsanchezCineContext())
                {
                    var query = context.Cines.FromSqlRaw($"CineGetAll").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Cine cine = new ML.Cine();

                            cine.IdCine = ((Int32)obj.IdCine);
                            cine.Latitud = obj.Latitud;
                            cine.Longitud = obj.Longitud;
                            cine.Direccion = obj.Direccion;
                            cine.Venta = obj.Venta;

                            cine.Zona = new ML.Zona();
                            cine.Zona.IdZona = obj.IdZona.Value;
                            cine.Zona.Nombre = obj.Nombre;

                            result.Objects.Add(cine);
                        }
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetById(int IdCine)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezCineContext context = new DL.JsanchezCineContext())
                {
                    var query = context.Cines.FromSqlRaw($"CineGetById '{IdCine}'").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        result.Object = new List<object>();
                        var obj = query;
                        {
                            ML.Cine cine = new ML.Cine();

                            cine.IdCine = ((Int32)obj.IdCine);
                            cine.Latitud = obj.Latitud;
                            cine.Longitud = obj.Longitud;
                            cine.Direccion = obj.Direccion;
                            cine.Venta = obj.Venta;

                            cine.Zona = new ML.Zona();
                            cine.Zona.IdZona = obj.IdZona.Value;
                            cine.Zona.Nombre = obj.Nombre;

                            result.Object = cine;
                        }
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result VentasGet()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezCineContext context = new DL.JsanchezCineContext())
                {
                    var query = context.Cines.FromSqlRaw($"VentasGet").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Cine cine = new ML.Cine();

                            cine.IdCine = obj.IdCine;
                            cine.Venta = obj.Ventas.ToString();
                            cine.Porcentaje = obj.Porcentaje;

                            cine.Zona = new ML.Zona();
                            cine.Zona.Nombre = obj.Zonas;

                            result.Objects.Add(cine);
                        }
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
