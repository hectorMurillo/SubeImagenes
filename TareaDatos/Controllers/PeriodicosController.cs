using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TareaDatos.Repositories;
using TareaDatos.Models;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;

namespace TareaDatos.Controllers
{
    public class PeriodicosController : Controller
    {
        IDatosRepository periodicos;
        IImageStorageContainer imagenes;
        public PeriodicosController(IDatosRepository periods, IImageStorageContainer img)
        {
            periodicos = periods;
            imagenes = img;
        }
        public PeriodicosController()
        {

        }
        // GET: Periodico
        public ActionResult Index()
        {
            var model = periodicos.LeerPeriodicos();
            return View(model);
        }

        // GET: Periodico/Details/5
        public ActionResult Details(int id)
        {
            var model = periodicos.LeerPeriodicoPorID(id);
            return View(model);
        }

        // GET: Periodico/Create
        public ActionResult Create()
        {
            var model = new Periodico();
            return View(model);
        }

        // POST: Periodico/Create
        [HttpPost]
        public ActionResult Create(Periodico nuevo, HttpPostedFileBase imagen)
        {            

            var Url = string.Empty;
            if (imagen == null || string.IsNullOrWhiteSpace(imagen.FileName))
            {
                ModelState.AddModelError("Imagen", "Debe de subir un archivo");
            }
            else
            {
                Url = imagenes.GuardarImagen("imagenes", imagen.FileName, imagen.InputStream);
            }

            if (!ModelState.IsValid)
            {
                return View(nuevo);
            }
            try
            {                
                nuevo.Imagen = Url;
                periodicos.CrearPeriodico(nuevo);                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Periodico/Edit/5
        public ActionResult Edit(int id)
        {
            var model = periodicos.LeerPeriodicoPorID(id);
            return View(model);
        }

        // POST: Periodico/Edit/5
        [HttpPost]
        public ActionResult Edit(int id,Periodico editar, HttpPostedFileBase imagen)
        {
            var model = periodicos.LeerPeriodicoPorID(id);            
            var URL = string.Empty;            
            var bandera = true;
            if ((imagen == null || string.IsNullOrWhiteSpace(imagen.FileName)) && model.Imagen == null)
            {
                bandera = false;
                ModelState.AddModelError("Imagen", "Debe de subir un archivo");
            }
            else if (bandera && imagen != null)
            {
                URL = imagenes.GuardarImagen("imagenes", imagen.FileName, imagen.InputStream);
            }
            else if(bandera){ URL = model.Imagen; }
            try
            {
                editar.Imagen = URL;
                periodicos.ActualizarPeriodico(editar);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Periodico/Delete/5
        public ActionResult Delete(int id)
        {
            var model = periodicos.LeerPeriodicoPorID(id);
            if(model==null)
            {
                HttpNotFound();
            }
            return View(model);
        }

        // POST: Periodico/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Periodico periodicoABorrar)
        {
            try
            {
                // TODO: Add delete logic here
                periodicos.BorrarPeriodico(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
