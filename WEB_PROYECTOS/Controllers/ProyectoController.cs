using Entidad;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEB_PROYECTOS.Controllers
{
    public class ProyectoController : Controller
    {
        // esta directiva permite que cualquiera pueda ingresar a esta opcion del sistema
        //[AllowAnonymous]

        // Authorize restringe el acceso a opciones del sistema a travez de los Roles de los Usuarios
        [Authorize(Roles = "Admin")]
        // GET: Proyecto
        public ActionResult Index()
        {
            var proyectos = ProyectoCN.ListarProyectos();
            return View(proyectos);
        }

        public ActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Proyecto proyecto)
        {
            try
            {
                if (proyecto.NombreProyecto == null)
                    return Json(new { ok = false, msg = "Debe Ingresar el nombre del Proyecto" }, JsonRequestBehavior.AllowGet);
                // para demorar los procesos se utiliza
                // system.threading
                // System.Threading.Thread.Sleep(1000);

                ProyectoCN.Agregar(proyecto);
                return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return Json(new { ok = false, msg = "Ocurrio en un Error al agregar un Proyecto" }, JsonRequestBehavior.AllowGet);
                //ModelState.AddModelError("", "Ocurrio un error al agregar un Proyecto");
                //return View();
            }

        }

        public ActionResult Detalles(int id)
        {
            var proyecto = ProyectoCN.ObtenerProyecto(id);
            return View(proyecto);
        }

        public ActionResult Editar(int id)
        {
            var proyecto = ProyectoCN.ObtenerProyecto(id);
            return View(proyecto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Proyecto proyecto)
        {
            try
            {
                ProyectoCN.Editar(proyecto);
                return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json(new { ok = false, msg = "Ocurrio en un Error al Editar un Proyecto" }, JsonRequestBehavior.AllowGet);
            }


        }
        [HttpPost]
        public ActionResult Eliminar(int identificador)
        {
            try
            {
                ProyectoCN.Eliminar(identificador);
                return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json(new { ok = false, msg = "Ocurrio en un Error al Eliminar un Proyecto" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ListarProyectos()
        {
            try
            {
                var lista = ProyectoCN.ListarProyectos();
                return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { ok = false, msg = "Ocurrio en un Error al Eliminar un Proyecto "+ex.Message  }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult AsignarProyecto()
        {
            return View(ProyectoCN.ListarAsignaciones());
        }
        [HttpPost]
        public ActionResult AsignarProyecto(int proyectoid, int empleadoid)
        {
            try
            {
                if (ProyectoCN.ExisteAsignacion(proyectoid, empleadoid))
                    return Json(new { ok = false, msg = "Ya Existe una Relacion entre este Proyecto y el Empleado" });
                if (!ProyectoCN.esProyectoActivo(proyectoid))
                    return Json(new { ok = false, msg = "El Proyecto ya no se Encuentra Activo" });

                ProyectoCN.AsignarProyecto(proyectoid, empleadoid);
                return Json(new { ok = true, toRedirect = Url.Action("AsignarProyecto") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json(new { ok = false, msg = "Ocurrio en un Error al Eliminar un Proyecto" }, JsonRequestBehavior.AllowGet);
            }


        }

        [HttpPost]
        public ActionResult EliminarAsignacion(int proyectoid, int empleadoid)
        {
            try
            {
                ProyectoCN.EliminarAsignacio(proyectoid, empleadoid);
                return Json(new { ok = true, toRedirect = Url.Action("AsignarProyecto") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
