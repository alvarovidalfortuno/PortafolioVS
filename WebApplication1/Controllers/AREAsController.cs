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
    public class AREAsController : Controller
    {
        private Entities db = new Entities();

        // GET: AREAs
        public ActionResult Index()
        {
            var aREA = db.AREA.Include(a => a.CLIENTE);
            return View(aREA.ToList());
        }



        // GET: AREAs/Create
        public ActionResult Create()
        {
            ViewBag.ID_CLIENTE = new SelectList(db.CLIENTE, "ID_CLIENTE", "NOMBRE_CLIENTE");
            return View();
        }



        [HttpPost]
        public JsonResult IngresoArea(string nombre_area, string desc_area,    int id_cliente )
        {
            var resultado = true;

            AREA query_p = new AREA();
            var contador = db.AREA.Where(w => w.NOMBRE_AREA == nombre_area && w.ID_CLIENTE == id_cliente).ToList();

            if (contador.Count() == 0)
            {
                /// INSERT DE AREA
                db.INSERTAR_AREA(0, nombre_area, desc_area, id_cliente);

            }
            else
            {
                resultado = false;
                ViewBag.Error = "Usuario ya existe en sistemas!";

            }

            return Json(new
            {
                Result = "OK" + resultado,
                resultado = resultado
            });

        }

        // GET: AREAs/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AREA aREA = db.AREA.Find(id);
            if (aREA == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_CLIENTE = new SelectList(db.CLIENTE, "ID_CLIENTE", "NOMBRE_CLIENTE", aREA.ID_CLIENTE);
            return View(aREA);
        }


        [HttpPost]
        public JsonResult UpdateArea(decimal id,string nombre_area,string desc_area)
        {
            try
            {
                db.ACTUALIZAR_AREA(id,nombre_area,desc_area);
 
                    return Json(new
                    {
                        Result = "OK"
                    });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        // GET: AREAs/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AREA aREA = db.AREA.Find(id);
            if (aREA == null)
            {
                return HttpNotFound();
            }
            return View(aREA);
        }
        [HttpPost]
        public JsonResult BorrarArea(decimal id)
        {
            var contador = db.AREA.Where(w => w.ID_AREA == id).ToList();
            if (contador.Count() > 0)
            {

                db.BORRAR_AREA(id);
            }
            else
            {
                ViewBag.Error = "Area ya a sido borrada!";
            }

            return Json(new
            {
                Result = "OK"
            });
            //}
            //catch (Exception ex)
            //{
            //    return Json(new { Result = "ERROR", Message = ex.Message });
            //}
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
