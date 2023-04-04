using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PL.Models;

namespace PL.Controllers
{
    public class PeliculaController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public PeliculaController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        public ActionResult GetPeliculasPopulares()
        {
            Pelicula pelicula = new Pelicula();

            using (var client = new HttpClient())
            {
                string urlApi = _configuration["urlApi"];//Colocar la url
                client.BaseAddress = new Uri(urlApi);

                var responseTask = client.GetAsync("popular?api_key=9531cdae14d8454260e379ee2dd22bf6&language=es-ES&page=1");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    dynamic resultJSON = JObject.Parse(readTask.Result.ToString());
                    readTask.Wait();
                    pelicula.Peliculas = new List<object>();

                    foreach (var resultItem in resultJSON.results)
                    {
                        Pelicula peliculaItem = new Pelicula();
                        peliculaItem.media_id = resultItem.id;
                        peliculaItem.Titulo = resultItem.title;
                        peliculaItem.Descripcion = resultItem.overview;
                        peliculaItem.Imagen = "https://www.themoviedb.org/t/p/w600_and_h900_bestv2" + resultItem.poster_path;
                        pelicula.Peliculas.Add(peliculaItem);
                    }
                }
            }
            return View(pelicula);
        }

        [HttpGet]
        public ActionResult AddFavoritos(Pelicula pelicula)
        {
            //Pelicula pelicula = new Pelicula();
            pelicula.favorite = true;
            pelicula.media_type = "movie";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["urlApiAccount"]);

                var postTask = client.PostAsJsonAsync("favorite?api_key=9531cdae14d8454260e379ee2dd22bf6&session_id=de756b638cccc081384a865beaf3a17add0a03a0",pelicula);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Se Agrego a favoritos";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al Agregar a favoritos";
                }
                return View("Modal");
            }
        }

        [HttpGet]
        public ActionResult DeleteFavoritos(Pelicula pelicula)
        {
            //Pelicula pelicula = new Pelicula();
            pelicula.favorite = false;
            pelicula.media_type = "movie";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["urlApiAccount"]);

                var postTask = client.PostAsJsonAsync("favorite?api_key=9531cdae14d8454260e379ee2dd22bf6&session_id=de756b638cccc081384a865beaf3a17add0a03a0", pelicula);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Se elimino de favoritos";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al Eliminar de favoritos";
                }
                return View("Modal");
            }
        }

        [HttpGet]
        public ActionResult GetPeliculasFavoritas()
        {
            Pelicula pelicula = new Pelicula();

            using (var client = new HttpClient())
            {
                string urlApi = _configuration["urlApiAccount"];//Colocar la url
                client.BaseAddress = new Uri(urlApi);

                var responseTask = client.GetAsync("favorite/movies?api_key=9531cdae14d8454260e379ee2dd22bf6&session_id=de756b638cccc081384a865beaf3a17add0a03a0&language=es-ES&sort_by=created_at.asc&page=1");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    dynamic resultJSON = JObject.Parse(readTask.Result.ToString());
                    readTask.Wait();
                    pelicula.Peliculas = new List<object>();

                    foreach (var resultItem in resultJSON.results)
                    {
                        Pelicula peliculaItem = new Pelicula();
                        peliculaItem.media_id = resultItem.id;
                        peliculaItem.Titulo = resultItem.title;
                        peliculaItem.Descripcion = resultItem.overview;
                        peliculaItem.Imagen = "https://www.themoviedb.org/t/p/w600_and_h900_bestv2" + resultItem.poster_path;
                        pelicula.Peliculas.Add(peliculaItem);
                    }
                }
            }
            return View(pelicula);
        }
    }
}
