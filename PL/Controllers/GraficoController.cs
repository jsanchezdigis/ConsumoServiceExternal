using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class GraficoController : Controller
    {
        public ActionResult Cake()
        {
            ML.Result resultGet = BL.Cine.GetAll();
            ML.Result result = BL.Cine.VentasGet();
            ML.Cine cine = new ML.Cine();
            if (result.Correct)
            {
                //cine.Cines = resultGet.Objects;
                cine.Cines = result.Objects;
                cine.Direcciones = resultGet.Objects;
                return View(cine);
            }
            else
            {
                return View(cine);
            }
        }
    }
}
