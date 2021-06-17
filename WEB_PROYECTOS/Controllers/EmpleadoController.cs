using Entidad;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEB_PROYECTOS.Controllers
{
    public class EmpleadoController : Controller
    {
        // esta directiva permite que cualquiera pueda ingresar a esta opcion del sistema
        //[AllowAnonymous]

        // Authorize restringe el acceso a opciones del sistema a travez de los Roles de los Usuarios
        [Authorize(Roles ="Admin")]
        // GET: Proyecto
        public ActionResult Index()
        {
            var empleados = EmpleadoCN.ListarEmpleados();
            return View(empleados);
        }

        public ActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Empleado empleado)
        {
            try
            {
                if(empleado.Nombres==null)
                    return Json(new { ok = false, msg = "Debe Ingresar el nombre del Empleado" }, JsonRequestBehavior.AllowGet);
                // para demorar los procesos se utiliza
                // system.threading
                // System.Threading.Thread.Sleep(1000);

                EmpleadoCN.Agregar(empleado);
                return Json(new { ok=true, toRedirect= Url.Action("Index") }, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return Json(new { ok = false, msg = "Ocurrio en un Error al agregar un Empleado" }, JsonRequestBehavior.AllowGet);
                //ModelState.AddModelError("", "Ocurrio un error al agregar un Proyecto");
                //return View();
            }
            
        }

        public ActionResult Detalles(int id)
        {
            var empleado = EmpleadoCN.ObtenerEmpleado(id);
            return View(empleado);
        }

        public ActionResult Editar(int id)
        {
            var empleado = EmpleadoCN.ObtenerEmpleado(id);
            return View(empleado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Empleado empleado)
        {
            try
            {
                EmpleadoCN.Editar(empleado);
                return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json(new { ok = false, msg = "Ocurrio en un Error al Editar un Empleado" }, JsonRequestBehavior.AllowGet);
            }
                
            
        }
        [HttpPost]
        public ActionResult Eliminar(int identificador)
        {
            try
            {
                EmpleadoCN.Eliminar(identificador);
                return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json(new { ok = false, msg = "Ocurrio en un Error al Eliminar un Empleado" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ListarEmpleados()
        {
            try
            {
                var lista = EmpleadoCN.ListarEmpleados();
                return Json(new { data = lista}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok =false, msg = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }

    }
}