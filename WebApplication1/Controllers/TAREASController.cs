using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TAREASController : Controller
    {
        private Entities db = new Entities();

        public ActionResult Index()
        {

            if (Session["usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult Create()
        {
            return View();
        }




        [HttpPost]

        public async Task<JsonResult> CrearTarea()
        {

            //try
            //{

            var usuario = Session["usuario"];

            var correo = db.EMPLEADOS
              .Join(db.USUARIOS, emp => emp.ID_USUARIO, us => us.ID_USUARIO, (emp, us) => new { emp, us })
              .Where(w => w.us.CORREO_USUARIO == usuario.ToString())
                .Select(s => new USUARIOS
                {   
                    CORREO_USUARIO = s.us.CORREO_USUARIO
                });




            


            var fecha_creacion = DateTime.Now;


            var contador = db.TAREAS.OrderByDescending(o => o.ID_TAREA).FirstOrDefault().ID_TAREA;
            var id = contador + 1;


            var nombre_tarea = Request.Form["nombre_tarea"].ToString();
            var descripcion_tarea = Request.Form["descripcion_tarea"].ToString();
            var deadline = Int32.Parse(Request.Form["deadline"].ToString());


            TAREAS tareita = new TAREAS();
                tareita.NOMBRE_TAREA = nombre_tarea;
                tareita.DESC_TAREA = descripcion_tarea;
                tareita.DEADLINE = deadline;
                tareita.FECHA_INICIO = fecha_creacion;
                tareita.ID_TAREA = id;
                db.TAREAS.Add(tareita);
                db.SaveChanges();


                var nombre_documento = "";
                var id_documento = 0;


            //}


            //catch (Exception ex)
            //{
            //    return Json(new { Result = "ERROR", Message = ex.Message });
            //}


            return Json(new { Result = "OK"}, JsonRequestBehavior.AllowGet);

        }
    }
}