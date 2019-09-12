using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ROLESController : Controller
    {
        private Entities db = new Entities();

        // GET: ROLES
        public ActionResult Index()
        {
            return View(db.ROLES.ToList());
        }

        // GET: ROLES/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROLES rOLES = db.ROLES.Find(id);
            if (rOLES == null)
            {
                return HttpNotFound();
            }
            return View(rOLES);
        }

        // GET: ROLES/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ROLES/Create
   
        [HttpPost]
        public JsonResult IngresoRol(string nombre_rol, string desc_rol)
        {
            var resultado = true;

            AREA query_p = new AREA();
            var contador = db.ROLES.Where(w => w.NOMBRE_ROL == nombre_rol).ToList();

            if (contador.Count() == 0)
            {
                /// INSERT DE AREA
                db.INSERTAR_ROL(0, nombre_rol,desc_rol);

            }
            else
            {
                resultado = false;
                ViewBag.Error = "rol ya existe en sistemas!";

            }

            return Json(new
            {
                Result = "OK" + resultado,
                resultado = resultado
            });

        }


        // GET: ROLES/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROLES rOLES = db.ROLES.Find(id);
            if (rOLES == null)
            {
                return HttpNotFound();
            }
            return View(rOLES);
        }

        // POST: ROLES/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_ROL,NOMBRE_ROL,DESC_ROL")] ROLES rOLES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rOLES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rOLES);
        }

        // GET: ROLES/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROLES rOLES = db.ROLES.Find(id);
            if (rOLES == null)
            {
                return HttpNotFound();
            }
            return View(rOLES);
        }

        // POST: ROLES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            ROLES rOLES = db.ROLES.Find(id);
            db.ROLES.Remove(rOLES);
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
    }
}
