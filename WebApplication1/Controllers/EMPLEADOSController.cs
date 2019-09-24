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
    public class EMPLEADOSController : Controller
    {
        private Entities db = new Entities();

        // GET: EMPLEADOS
        public ActionResult Index()
        {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }


            var eMPLEADOS = db.EMPLEADOS.Include(e => e.COMUNA).Include(e => e.USUARIOS);
            return View(eMPLEADOS.ToList());
        }

        public ActionResult Crear2()
        {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }


            var rol = db.ROLES.ToList();
            ViewBag.rol = rol;

            var provincia = db.PROVINCIA.ToList();
            ViewBag.provincia = provincia;

            var area = db.AREA.ToList();
            ViewBag.area = area;

            var cliente = db.CLIENTE.ToList();
            ViewBag.cliente = cliente;

            return View();
        }

       
        // GET: EMPLEADOS/Create
        public ActionResult Create()
        {
            var rol = db.ROLES.ToList();
            ViewBag.rol = rol;

            var provincia = db.PROVINCIA.ToList();
            ViewBag.provincia = provincia;

            var area = db.AREA.ToList();
            ViewBag.area = area;

         
            return View();
        }



        public JsonResult IngresoPersonaPost(int rut, string dv, string nombre, string apellido_paterno, string apellido_materno, int edad, int comuna, string direccion, string correo, string pass, int cliente, int area,int rol)

        {
            var resultado = true;
            //try
            //{

            EMPLEADOS query_p = new EMPLEADOS();
            var contador = db.EMPLEADOS.Where(w => w.RUN_EMPLEADO == rut && w.DV_EMPLEADO == dv).ToList();

            if (contador.Count() == 0)
            {
                /// INSERT DE USUARIO

                db.INSERTAR_USUARIOS(0, correo, pass);
                ///INSERTE DE PERSONAS
                var idUsuario = db.USUARIOS.FirstOrDefault(w => w.CORREO_USUARIO == correo).ID_USUARIO;

                db.INSERTAR_EMPLEADOS(0,nombre, rut, dv,apellido_paterno,apellido_materno,edad,direccion,comuna,idUsuario,area);
                ///INSERT DE ROLES

                var idPersona = db.EMPLEADOS.FirstOrDefault(m => m.ID_USUARIO == idUsuario).ID_EMPLEADO;

               db.INSERTAR_ROL_EMPLEADO(0, rol, idPersona);


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
            //}
            //catch (Exception ex)
            //{
            //    return Json(new { Result = "ERROR", Message = ex.Message });
            //}
        }

        [HttpPost]
        public JsonResult BorrarPersona(decimal id)
        {

            var contador = db.EMPLEADOS.Where(w => w.ID_EMPLEADO == id).ToList();
            if (contador.Count() > 0)
            {

                var idUsuario = db.EMPLEADOS.FirstOrDefault(m => m.ID_EMPLEADO == id).ID_USUARIO;

                db.BORRAR_EMPLEADO(id);

               


                db.BORRAR_USUARIO(idUsuario);

            }
            else
            {
                ViewBag.Error = "Usuario ya a sido borrado!";
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


        [HttpPost]
        public JsonResult UpdateDatosPersona(decimal persona_id, int rut, string dv, string nombre, string apellido1, string apellido2, int idArea)
        {
            try
            {
               db.ACTUALIZAR_EMPLEADO(persona_id, nombre, rut, dv, apellido1,apellido2,idArea);

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


        // GET: EMPLEADOS/Edit/5
        public ActionResult Edit(decimal id)
        {

            var area = db.AREA.ToList();
            ViewBag.area = area;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLEADOS eMPLEADOS = db.EMPLEADOS.Find(id);
            if (eMPLEADOS == null)
            {
                return HttpNotFound();
            }
            return View(eMPLEADOS);
        }

        // POST: EMPLEADOS/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_EMPLEADO,SNOMBRE_EMPLEADO,PAPELLIDO_EMPLEADO,SAPELLIDO_EMPLEADO,EDAD_EMPLEADO,RUN_EMPLEADO,DV_EMPLEADO,DIRECCION,ID_COMUNA,ID_USUARIO")] EMPLEADOS eMPLEADOS)
        {



            if (ModelState.IsValid)
            {
                db.Entry(eMPLEADOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_COMUNA = new SelectList(db.COMUNA, "ID_COMUNA", "NOMBRE_COMUNA", eMPLEADOS.ID_COMUNA);
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "CORREO_USUARIO", eMPLEADOS.ID_USUARIO);
            return View(eMPLEADOS);
        }

        // GET: EMPLEADOS/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLEADOS eMPLEADOS = db.EMPLEADOS.Find(id);
            if (eMPLEADOS == null)
            {
                return HttpNotFound();
            }
            return View(eMPLEADOS);
        }

        // POST: EMPLEADOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            EMPLEADOS eMPLEADOS = db.EMPLEADOS.Find(id);
            db.EMPLEADOS.Remove(eMPLEADOS);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult _DesplegableArea(decimal cliente)
        {
            ViewBag.area = db.AREA.Where(w => w.ID_CLIENTE == cliente).ToList();

            return PartialView();
        }


        public ActionResult _DesplegableComuna(decimal provincia) {
            ViewBag.comuna = db.COMUNA.Where(w => w.ID_PROVINCIA == provincia).ToList();

            return PartialView();
        }

    }
}
