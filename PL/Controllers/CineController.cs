using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class CineController : Controller
    {
        public ActionResult GetAll()
        {
            ML.Result result = BL.Cine.GetAll();
            ML.Cine cine = new ML.Cine();
            if (result.Correct)
            {
                cine.Cines = result.Objects;
                return View(cine);
            }
            else
            {
                return View(cine);
            }
        }

        [HttpGet]
        //Formulario
        public ActionResult Form(int IdCine)
        {
            ML.Result resultZona = BL.Zona.GetAll();

            ML.Cine cine = new ML.Cine();
            cine.Zona = new ML.Zona();

            if (resultZona.Correct)
            {
                cine.Zona.Zonas = resultZona.Objects;
            }
            if (IdCine == 0/*null*/)
            {
                //Add Formulario vacio
                return View(cine);
            }
            else
            {
                ML.Result result = BL.Cine.GetById(IdCine);

                if (result.Correct)
                {
                    cine = (ML.Cine)result.Object;
                    cine.Zona.Zonas = resultZona.Objects;
                    return View(cine);
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al consultar la informacion";
                    return View("Modal");
                }
            }
        }
        [HttpPost]
        public ActionResult Form(ML.Cine cine)
        {
            ML.Result result = new ML.Result();
            if (cine.IdCine == 0)
            {
                //Add
                result = BL.Cine.Add(cine);
                if (result.Correct)
                {
                    ViewBag.Message = "Se completo el registro satidfactoriamente";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al insertar el registro";
                }
                return View("Modal");
            }
            else
            {
                //Update
                result = BL.Cine.Update(cine);
                if (result.Correct)
                {
                    ViewBag.Message = "Se completo el registro satidfactoriamente";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al insertar el registro";
                }
                return View("Modal");
            }
            ML.Result resultZona = BL.Zona.GetAll();
            cine.Zona.Zonas = resultZona.Objects;
            return View(cine);
        }

        [HttpGet]
        public ActionResult Delete(ML.Cine cine)
        {
            ML.Result result = BL.Cine.Delete(cine);
            if (result.Correct)
            {
                ViewBag.Message = "Se elimino el registro satisfactoriamente";
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al eliminar el registro";
            }
            return View("Modal");
        }
    }
}
