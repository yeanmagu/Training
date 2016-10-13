using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using System.Net;
using System.Data.Entity;
namespace WebApplication2.Controllers
{
    public class PersonaSinAsistenteController : Controller
    {

       private BDContext db = new BDContext();
        // GET: PersonaSinAsistente
        public ActionResult Index()
        {
            return View(db.Personas.ToList());
        }

        //Metodo para crear un nuevoregistro que devuelve la vista vacia
        public ActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Nuevo([Bind(Include="ID,Nombre,Apellido")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Personas.Add(persona);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(persona);
        }

        //mETODO PARA EDITAR

        public ActionResult Editar(int? id)
        {
            if (id==null)
	        {
		        return new HttpStatusCodeResult (HttpStatusCode.BadRequest);
	        }
            Persona persona = db.Personas.Find(id);
            if (persona==null)
	        {
		        return HttpNotFound(); 
        	}
            return View(persona);
        }

        [HttpPost]

        public ActionResult Editar([Bind(Include = "ID,Nombre,Apellido")]Persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(persona).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View (persona);

        }

        // ELIMINAR 

        public ActionResult Eliminar(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Persona persona = db.Personas.Find(id);

            if (persona == null)
            {
                return HttpNotFound();
            }

            return View(persona);

        }

        // CONFIRMAR ELIMINAR

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]

        public ActionResult ConfirmarEliminar(int id)
        {
            Persona persona = db.Personas.Find(id);
            db.Personas.Remove(persona);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Detalles(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Persona persona = db.Personas.Find(id);

            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

    }
}