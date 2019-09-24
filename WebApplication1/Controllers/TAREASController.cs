using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TAREASController : Controller
    {
        private Entities db = new Entities();

        public ActionResult Tab1() {

            if (Session["usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }



            ViewBag.tarea = db.TAREAS
                .Join(db.EMPLEADOS, t => t.ID_USUARIO, e => e.ID_USUARIO, (t, e) => new { t, e })
                .ToList()
                .Select(s => new indexTarea
                {
                    responsable = s.e.SAPELLIDO_EMPLEADO,
                    estado = Int32.Parse(s.t.ID_ESTADO.ToString()),
                    fecha_inicio = s.t.FECHA_INICIO.ToString(),
                    fecha_termino = s.t.FECHA_TERMINO.ToString(),
                    tarea_id = Int32.Parse(s.t.ID_TAREA.ToString()),
                    descripcion = s.t.DESC_TAREA,
                    deadline = Int32.Parse(s.t.DEADLINE.ToString())


                });


            return PartialView();

        }


        public ActionResult Tab2()
        {

            if (Session["usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }



            ViewBag.tarea = db.TAREAS
                .Join(db.EMPLEADOS, t => t.ID_USUARIO, e => e.ID_USUARIO, (t, e) => new { t, e })
                .ToList()
                .Select(s => new indexTarea
                {
                    responsable = s.e.SAPELLIDO_EMPLEADO,
                    estado = Int32.Parse(s.t.ID_ESTADO.ToString()),
                    fecha_inicio = s.t.FECHA_INICIO.ToString(),
                    fecha_termino = s.t.FECHA_TERMINO.ToString(),
                    tarea_id = Int32.Parse(s.t.ID_TAREA.ToString()),
                    descripcion = s.t.DESC_TAREA,
                    deadline = Int32.Parse(s.t.DEADLINE.ToString())


                });


            return PartialView();

        }


        public ActionResult Index()
        {
          


            return View();


        }

        public ActionResult Create()
        {

            if (Session["usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }


        public ActionResult FinalizarTarea(decimal tarea) {

            var area = db.AREA.ToList();
            ViewBag.area = area;


            var estado = db.ESTADO_TAREA.ToList();
            ViewBag.estado = estado;

            var tareaDatos = tarea;
            ViewBag.tarea = tareaDatos;

            return View();
        }


        [HttpPost]
        public async Task<JsonResult> FinalizarTareaPost(int tarea, string descripcion, int estado)
        {
            //try
            //{

            if (estado == 3)
            {


                var query = db.TAREA_ASIGNADA.FirstOrDefault(w => w.ID_TAREA == tarea);

                query.ID_ESTADO = 3;

                db.SaveChanges();

                var query2 = db.TAREAS.FirstOrDefault(w => w.ID_TAREA == tarea);

                query2.ID_ESTADO = 3;

                // aqui le meto el di del creador id je je je



                ////var persona = db.USUARIOS
                ////    .Join(db.EMPLEADOS, e => e.ID_USUARIO, u => u.ID_USUARIO, (e, u) => new { e, u })
                ////    .Where(w => w.u.ID_EMPLEADO == responsable).FirstOrDefault().e.CORREO_USUARIO;




                //var message = new MailMessage();

                //string body = "";

                //body = body + "Estimado se a rechazado la tarea:   " + tarea + "+:<br/>";

                //body = body + " ATTE: Unidad de Desarrollo";

                //message.From = new MailAddress("nicolas.hernandezsalazar@gmail.com");// desde 
                //message.To.Add(new MailAddress(persona.ToString())); //hasta
                ////message.CC.Add(new MailAddress("mllempi@derecho.uchile.cl"));
                //message.IsBodyHtml = true;
                //message.Body = body;
                //message.Subject = "ASIGNACION DE TAREA EN SISTEMA";


                //try
                //{
                //    await CorreoController.EnviarCorreoAdjunto(message);
                //}
                //catch (Exception)
                //{
                //    return Json(new { Result = "ERROR" }, JsonRequestBehavior.AllowGet);
                //}




            }
            if (estado == 2)
            {
                var query = db.TAREAS.FirstOrDefault(w => w.ID_TAREA == tarea);

                query.ID_ESTADO = 2;

                db.SaveChanges();

                var query2 = db.TAREAS.FirstOrDefault(w => w.ID_TAREA == tarea);

                query2.ID_ESTADO = 2;

                db.SaveChanges();
            }



            if (estado == 1)
            {
                var query = db.TAREA_ASIGNADA.FirstOrDefault(w => w.ID_TAREA == tarea);

                query.ID_ESTADO = 1;
                //query.ID_USUARIO = USUARIOS;                //query.ID_USUARIO = USUARIOS;

                db.SaveChanges();

                var query2 = db.TAREAS.FirstOrDefault(w => w.ID_TAREA == tarea);

                query2.ID_ESTADO = 2;

                db.SaveChanges();
            }





            return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);

            //}
            //catch (Exception ex)
            //{
            //    return Json(new { Result = "ERROR", Message = ex.Message });
            //}
        }







        [HttpPost]
        public async Task<JsonResult> FinalizarHitoPost(int tarea, string descripcion, int estado)
        {
            //try
            //{

            if (estado == 3)
            {


                var query = db.TAREA_ASIGNADA.FirstOrDefault(w => w.ID_TAREA == tarea);

                query.ID_ESTADO = 3;

                db.SaveChanges();

                var query2 = db.TAREAS.FirstOrDefault(w => w.ID_TAREA == tarea);

                query2.ID_ESTADO = 3;

                // aqui le meto el di del creador id je je je



                ////var persona = db.USUARIOS
                ////    .Join(db.EMPLEADOS, e => e.ID_USUARIO, u => u.ID_USUARIO, (e, u) => new { e, u })
                ////    .Where(w => w.u.ID_EMPLEADO == responsable).FirstOrDefault().e.CORREO_USUARIO;




                //var message = new MailMessage();

                //string body = "";

                //body = body + "Estimado se a rechazado la tarea:   " + tarea + "+:<br/>";

                //body = body + " ATTE: Unidad de Desarrollo";

                //message.From = new MailAddress("nicolas.hernandezsalazar@gmail.com");// desde 
                //message.To.Add(new MailAddress(persona.ToString())); //hasta
                ////message.CC.Add(new MailAddress("mllempi@derecho.uchile.cl"));
                //message.IsBodyHtml = true;
                //message.Body = body;
                //message.Subject = "ASIGNACION DE TAREA EN SISTEMA";


                //try
                //{
                //    await CorreoController.EnviarCorreoAdjunto(message);
                //}
                //catch (Exception)
                //{
                //    return Json(new { Result = "ERROR" }, JsonRequestBehavior.AllowGet);
                //}




            }
            if (estado == 2)
            {
                var query = db.TAREA_ASIGNADA.FirstOrDefault(w => w.ID_TAREA == tarea);

                query.ID_ESTADO = 2;

                db.SaveChanges();

                var query2 = db.TAREAS.FirstOrDefault(w => w.ID_TAREA == tarea);

                query2.ID_ESTADO = 2;

                db.SaveChanges();
            }



            if (estado == 1)
            {
                var query = db.TAREA_ASIGNADA.FirstOrDefault(w => w.ID_TAREA == tarea);

                query.ID_ESTADO = 1;
                //query.ID_USUARIO = USUARIOS;                //query.ID_USUARIO = USUARIOS;

                db.SaveChanges();

                var query2 = db.TAREAS.FirstOrDefault(w => w.ID_TAREA == tarea);

                query2.ID_ESTADO = 2;

                db.SaveChanges();
            }





            return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);

            //}
            //catch (Exception ex)
            //{
            //    return Json(new { Result = "ERROR", Message = ex.Message });
            //}
        }






        public ActionResult CrearHito(decimal tarea) {

            if (Session["usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }


            var area = db.AREA.ToList();
            ViewBag.area = area;


            var tareaDatos = tarea;
            ViewBag.tarea = tareaDatos;


            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CrearHito(decimal tarea, int responsable, string descripcion)
        {
            //try
            //{
            TAREA_HITO tareaH = new TAREA_HITO();

            decimal autoIncrementable = 0;
            decimal contador = 0;

            try
            {
                contador = db.TAREA_HITO.OrderByDescending(o => o.ID_HITO).FirstOrDefault().ID_HITO;

                autoIncrementable = contador + 1;
            }
            catch (Exception)
            {
                autoIncrementable = 1;
            }




            var fecha_creacion = DateTime.Now;


            //var persona = db.USUARIOS
            // .Join(db.EMPLEADOS, e => e.ID_USUARIO, u => u.ID_USUARIO, (e, u) => new { e, u })
            // .Where(w => w.e.ID_USUARIO == responsable).FirstOrDefault().e.CORREO_USUARIO;



            var persona = "nicolas.hernandezsalazar@gmail.com";


            tareaH.ID_TAREA = tarea;
            tareaH.FECHA_INICIO = fecha_creacion;
            tareaH.DESC_HITO = descripcion;
            tareaH.ID_ESTADO = 1;
            tareaH.ID_HITO = autoIncrementable;

            db.TAREA_HITO.Add(tareaH);

            db.SaveChanges();


            //var message = new MailMessage();

            //string body = "";

            //body = body + "Se a sido la tarea   " + tareaA.ID_TAREA + "+:<br/>";
            //body = body + "Responsable: <br/><br/>";
            //body = body + "Unidad de Desarrollo";

            //message.From = new MailAddress("nicolas.hernandezsalazar@gmail.com");// desde 
            //message.To.Add(new MailAddress(persona.ToString())); //hasta
            ////message.CC.Add(new MailAddress("mllempi@derecho.uchile.cl"));
            //message.IsBodyHtml = true;
            //message.Body = body;
            //message.Subject = "SOLICITUD DE FECHA EXAMEN DE LICENCIATURA";


            //try
            //{
            //    await CorreoController.EnviarCorreoAdjunto(message);
            //}
            //catch (Exception)
            //{
            //    return Json(new { Result = "ERROR" }, JsonRequestBehavior.AllowGet);
            //}


            return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);

            //}
            //catch (Exception ex)
            //{
            //    return Json(new { Result = "ERROR", Message = ex.Message });
            //}
        }

        public ActionResult AsignarTarea() {


            var usuario = Session["usuario"];
            var usuarioAux = db.USUARIOS.Where(w => w.CORREO_USUARIO == usuario).FirstOrDefault().ID_USUARIO;

            ViewBag.responsable = db.EMPLEADOS.Where(w => w.ID_USUARIO == usuarioAux).FirstOrDefault().ID_EMPLEADO;




            var area = db.AREA.ToList();

            ViewBag.area = area;
            var tarea = db.TAREAS.Max(m => m.ID_TAREA);
            ViewBag.tarea = tarea;

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> IngresoResponsable(int responsable, int tarea , int asignador)
        {
            //try
            //{
            TAREA_ASIGNADA tareaA = new TAREA_ASIGNADA();

            decimal autoIncrementable = 0;
            decimal contador;



            try
            {
                contador = db.TAREA_ASIGNADA.OrderByDescending(o => o.ID_TAREA_ASIGNADA).FirstOrDefault().ID_TAREA_ASIGNADA;

                autoIncrementable = contador + 1;
            }
            catch (Exception)
            {
                autoIncrementable = 1;
            }


        


            /////COMENTARIO

           

            var persona = db.USUARIOS
                .Join(db.EMPLEADOS, e => e.ID_USUARIO, u => u.ID_USUARIO, (e, u) => new { e, u })
                .Where(w => w.u.ID_EMPLEADO == responsable).FirstOrDefault().e.CORREO_USUARIO;

            tareaA.ID_TAREA = tarea;
            tareaA.ID_USUARIO = responsable;
            tareaA.ID_ASIGNADOR = asignador;
            tareaA.ID_TAREA_ASIGNADA = autoIncrementable;
            tareaA.ID_ESTADO = 1;
            db.TAREA_ASIGNADA.Add(tareaA);

            db.SaveChanges();


            var message = new MailMessage();

            string body = "";

            body = body + "Estimado se le a asignado la tarea:   " + tareaA.ID_TAREA + "+:<br/>";

            body = body + " ATTE: Unidad de Desarrollo";

            message.From = new MailAddress("nicolas.hernandezsalazar@gmail.com");// desde 
            message.To.Add(new MailAddress(persona.ToString())); //hasta
            //message.CC.Add(new MailAddress("mllempi@derecho.uchile.cl"));
            message.IsBodyHtml = true;
            message.Body = body;
            message.Subject = "ASIGNACION DE TAREA EN SISTEMA";


            try
            {
                await CorreoController.EnviarCorreoAdjunto(message);
            }
            catch (Exception)
            {
                return Json(new { Result = "ERROR" }, JsonRequestBehavior.AllowGet);
            }


            return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);

            //}
            //catch (Exception ex)
            //{
            //    return Json(new { Result = "ERROR", Message = ex.Message });
            //}
        }

        public ActionResult _DesplegableResposanble(decimal area)
        {
            ViewBag.empleados = db.EMPLEADOS.Where(w => w.ID_AREA == area);

            return PartialView();
        }

        [HttpPost]

        public async Task<JsonResult> CrearTarea()
        {

            //try
            //{


            /// ingreso tarea

            var usuario = Session["usuario"];
            var fecha_creacion = DateTime.Now;
            var responsable = db.USUARIOS.Where(w => w.CORREO_USUARIO == usuario).FirstOrDefault().ID_USUARIO;

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
            tareita.ID_USUARIO = responsable;
            tareita.ID_TAREA = id;
            tareita.ID_ESTADO = 1;
            db.TAREAS.Add(tareita);
            db.SaveChanges();

            //// codigo de ingreso documentacion

            decimal autoIncrementabledDoc = 0;
            decimal contadorDoc = 0;

            try
            {
                contadorDoc = db.DOCUMENTO.OrderByDescending(o => o.ID_DOCUMENTO).FirstOrDefault().ID_DOCUMENTO;

                autoIncrementabledDoc = contadorDoc + 1;
            }
            catch (Exception)
            {
                autoIncrementabledDoc = 1;
            }

            var nombre_documento = "";
            var id_documento = 0m; //m decimal

            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];

                if (file != null && file.ContentLength > 0 && file.ContentLength <= 50000000)
                {
                    var fileName = Path.GetFileName(file.FileName);

                    DOCUMENTO nuevo_documento = new DOCUMENTO();
                    nuevo_documento.GUID = Guid.NewGuid().ToString();
                    nuevo_documento.TIPO = Path.GetExtension(fileName);
                    nuevo_documento.NOMBRE_DOCUMENTO = fileName;
                    nuevo_documento.FECHA_CREACION = DateTime.Now;
                    nuevo_documento.ID_DOCUMENTO = autoIncrementabledDoc;
                    db.DOCUMENTO.Add(nuevo_documento);
                    db.SaveChanges();

                    nombre_documento = fileName;
                    id_documento = nuevo_documento.ID_DOCUMENTO;



                    ///datos de doc usuario

                    decimal autoIncrementabledDocU = 0;
                    decimal contadorDocU = 0;

                    try
                    {
                        contadorDocU = db.DOCUMENTO_USUARIO.OrderByDescending(o => o.ID_DOCUMENTO).FirstOrDefault().ID_DOCUMENTO_USUARIO;

                        autoIncrementabledDocU = contadorDocU + 1;
                    }
                    catch (Exception)
                    {
                        autoIncrementabledDocU = 1;
                    }


                    DOCUMENTO_USUARIO nuevo_documentoU = new DOCUMENTO_USUARIO();

                    nuevo_documentoU.FECHA_CREACION = DateTime.Now;
                    nuevo_documentoU.ID_DOCUMENTO_USUARIO = autoIncrementabledDoc;
                    nuevo_documentoU.ID_DOCUMENTO = id_documento;
                    nuevo_documentoU.ID_USUARIO = responsable;
                    db.DOCUMENTO_USUARIO.Add(nuevo_documentoU);
                    db.SaveChanges();


                    var ruta = Server.MapPath("~/Uploads/");

                    if (!Directory.Exists(ruta))
                    {
                        Directory.CreateDirectory(ruta);
                    }

                    var path = Path.Combine(ruta, nuevo_documento.GUID + nuevo_documento.TIPO);
                    file.SaveAs(path);
                }

            }



            ////////////////////////////////////////77





            //}


            //catch (Exception ex)
            //{
            //    return Json(new { Result = "ERROR", Message = ex.Message });
            //}

            ////////////////////////
            ///
            var message = new MailMessage();

            string body = "";
            body = usuario + " " + ":<br/><br/>";
            body = body + "Su solicitud a sido ingresada es:<br/>";
            body = body + "Atentamente,<br/><br/>";
            body = body + "Unidad de Desarrollo";

            message.From = new MailAddress("nicolas.hernandezsalazar@gmail.com");// desde 
            message.To.Add(new MailAddress(usuario.ToString())); //hasta
            //message.CC.Add(new MailAddress("mllempi@derecho.uchile.cl"));
            message.IsBodyHtml = true;
            message.Body = body;
            message.Subject = "SOLICITUD DE FECHA EXAMEN DE LICENCIATURA";



            try
            {
                await CorreoController.EnviarCorreoAdjunto(message);
            }
            catch (Exception)
            {
                return Json(new { Result = "ERROR" }, JsonRequestBehavior.AllowGet);
            }


            return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public async Task<JsonResult> RechazarTarea(decimal tarea)
        {
            //try
            //{
            //TAREA_ASIGNADA tareaA = new TAREA_ASIGNADA();


            var tareaR = db.TAREAS.FirstOrDefault(d => d.ID_TAREA == tarea && d.ID_ESTADO == 1);
            tareaR.ID_ESTADO = 3;
            tareaR.FECHA_TERMINO = DateTime.Now;
            db.SaveChanges();



            //var persona = db.USUARIOS
            // .Join(db.EMPLEADOS, e => e.ID_USUARIO, u => u.ID_USUARIO, (e, u) => new { e, u })
            // .Where(w => w.e.ID_USUARIO == responsable).FirstOrDefault().e.CORREO_USUARIO;



            var persona = "nicolas.hernandezsalazar@gmail.com";





            var message = new MailMessage();

            string body = "";

            //body = body + "Se a sido la tarea  " "+:<br/>";
            body = body + "Responsable: <br/><br/>";
            body = body + "Unidad de Desarrollo";

            message.From = new MailAddress("nicolas.hernandezsalazar@gmail.com");// desde 
            message.To.Add(new MailAddress(persona.ToString())); //hasta
            //message.CC.Add(new MailAddress("mllempi@derecho.uchile.cl"));
            message.IsBodyHtml = true;
            message.Body = body;
            message.Subject = "RECHAZO DE TAREA ";


            try
            {
                await CorreoController.EnviarCorreoAdjunto(message);
            }
            catch (Exception)
            {
                return Json(new { Result = "ERROR" }, JsonRequestBehavior.AllowGet);
            }


            return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);

            //}
            //catch (Exception ex)
            //{
            //    return Json(new { Result = "ERROR", Message = ex.Message });
            //}
        }

        

        public ActionResult _ListadoHitos(int tareaId)
        {

            var tarea = db.TAREA_HITO
              .Join(db.TAREA_ASIGNADA, t1 => t1.ID_TAREA, t2 => t2.ID_TAREA, (t1, t2) => new { t1, t2 })
              .Join(db.EMPLEADOS, t1 => t1.t2.ID_USUARIO, t3 => t3.ID_EMPLEADO, (t3, t1) => new { t3, t1 })
              .Join(db.ESTADO_TAREA,t4 => t4.t3.t1.ID_ESTADO , t5 => t5.ID_ESTADO , (t5,t4) => new { t4, t5 })

             .Where(w=> w.t5.t3.t1.ID_TAREA == tareaId)
             .ToList()
             .Select(s => new IndexHito
             {
                 
                responsable = s.t5.t1.SNOMBRE_EMPLEADO + "" + s.t5.t1.SAPELLIDO_EMPLEADO ,
                 estado = s.t4.NOMBRE_ESTADO,
                 fecha_inicio = s.t5.t3.t1.FECHA_INICIO.ToString(),
                  fecha_termino = s.t5.t3.t1.FECHA_TERMINO.ToString(),
                  tarea_id = Int32.Parse(s.t5.t3.t2.ID_TAREA.ToString()),
                 descripcion = s.t5.t3.t1.DESC_HITO,
                 hitoId = Int32.Parse(s.t5.t3.t1.ID_HITO.ToString())
               
                 //deadline = Int32.Parse(s.t.DEADLINE.ToString())
             });

            ViewBag.tarea = tarea;


            return PartialView();

        }


     

    }

    public class IndexHito
    {
        public string responsable { get; set; }
        public string estado { get; set; }
        public string fecha_inicio { get; set; }
        public string fecha_termino { get; set; }
        public int tarea_id { get; set; }
        public string descripcion { get; set; }
        public int deadline { get; set; }
        public int hitoId { get; set; }

    }

    public class indexTarea
    {
        public string responsable { get; set; }
        public int estado { get; set; }
        public string fecha_inicio { get; set; }
        public string fecha_termino { get; set; }
        public int tarea_id { get; set; }
        public string descripcion { get; set; }
        public int deadline { get; set; }

    }

}

