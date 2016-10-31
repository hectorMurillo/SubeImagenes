using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TareaDatos.Repositories;
using TareaDatos.Models;
using System.IO;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using TareaDatos.App_Start;
namespace TareaDatos.Controllers
{   
    public class BachesController : Controller
    {
        private IDatosRepository _baches;
        private IImageStorageContainer _imagenes;

        const string p1 = "¿Qué tanto tráfico pasa cerca de este bache? (Poco,Medio,Mucho)";
        const string p2 = "¿Qué tan peligroso es el bache? (Poco, Medio, Mucho)";
        public BachesController(IDatosRepository baches, IImageStorageContainer imgs)
        {
            _baches = baches;
            _imagenes = imgs;
        }
        // GET: Baches
        public ActionResult Index()
        {
            var model = _baches.LeerBaches();
            return View(model);
        }

        // GET: Baches/Details/5
        public ActionResult Details(int id)
        {
            var model = _baches.LeerBachePorID(id);
            return View(model);
        }

        // GET: Baches/Create
        public ActionResult Create()
        {
            var model = new Bache();
            model.Pregunta1 = p1;
            model.Pregunta2 = p2;
            return View(model);
        }

        // POST: Baches/Create
        [HttpPost]
        public ActionResult Create(Bache nuevo, HttpPostedFileBase imagen)
        {
            var Url = string.Empty;
            if (imagen == null || string.IsNullOrWhiteSpace(imagen.FileName))
            {
                ModelState.AddModelError("Imagen", "Debe de subir un archivo");
            }
            else
            {
                Url = _imagenes.GuardarImagen("imagenes", imagen.FileName, imagen.InputStream);
            }

            if (!ModelState.IsValid)
            {
                return View(nuevo);
            }
            try
            {
                nuevo.Imagen = Url;
                //DATOS CON CLASE METADATOS
                //var obtenerGPS = new metadatos();
                //obtenerGPS.valoresGPS(nuevo.Imagen);

                //nuevo.Latitud = obtenerGPS._latitud;
                //nuevo.Longitud = obtenerGPS._longitud;
                var image = _imagenes.obtenerImagen("imagenes", imagen.FileName, imagen.InputStream);
                //OBTENER DATOS PARA IMAGE 
                IEnumerable<MetadataExtractor.Directory> directories = ImageMetadataReader.ReadMetadata(image);
                var subIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
                var subGpsDirectory = directories.OfType<GpsDirectory>().FirstOrDefault();
                var longitude = subGpsDirectory?.GetDescription(GpsDirectory.TagLongitude);
                var latitude = subGpsDirectory?.GetDescription(GpsDirectory.TagLatitude);
                var dateTime = subIfdDirectory?.GetDescription(ExifDirectoryBase.TagDateTimeOriginal);
                if (longitude == null || latitude==null)
                {
                    ModelState.AddModelError("longitud", "Faltan EXIF a la imagen");
                }

                Ayuda aux = new Ayuda();
                aux.Conversion(latitude, longitude);
                nuevo.Latitud=aux.totalLatitud.ToString();
                nuevo.Longitud = aux.totalLongitud.ToString();                                          
                nuevo.Fecha = dateTime;
                nuevo.Pregunta1 = p1;
                nuevo.Pregunta2 = p2;
                //GuardarImagen
                _baches.CrearBache(nuevo);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Baches/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _baches.LeerBachePorID(id);
            return View(model);
        }


        // POST: Baches/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Bache editar, HttpPostedFileBase imagen)
        {
            var model = _baches.LeerPeriodicoPorID(id);
            var URL = string.Empty;
            var bandera = true;
            if ((imagen == null || string.IsNullOrWhiteSpace(imagen.FileName)) && model.Imagen == null)
            {
                bandera = false;
                ModelState.AddModelError("Imagen", "Debe de subir un archivo");
            }
            else if (bandera && imagen != null)
            {
                URL = _imagenes.GuardarImagen("imagenes", imagen.FileName, imagen.InputStream);
            }
            else if (bandera) { URL = model.Imagen; }
            try
            {
                editar.Imagen = URL;
                _baches.ActualizarBache(editar);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Baches/Delete/5
        public ActionResult Delete(int id)
        {
            var model = _baches.LeerBachePorID(id);
            if (model == null)
            {
                HttpNotFound();
            }
            return View(model);
        }

        // POST: Baches/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Bache BacheBorra)
        {
            try
            {
                _baches.BorrarBache(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
