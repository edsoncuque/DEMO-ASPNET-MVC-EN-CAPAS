using Entidad;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WEB_PROYECTOS.Controllers
{

    //para agregar mas roles de acceso al sistema se realizaria de la siguiente manera
    // [Authorize(Roles = "Admin, Compras, Ventas")] y estos tres roles tendrian acceso a esta opcion del sistema
    // si se coloca el authorize al inicio del controlador se da acceso a todas las funciones del mismo
    // pero si se coloca sobre una accion esta estaria restringida por el rol que hayamos colocado.

    // tambien se puede dar acceso por usuario de la siguiente manera
    // [Authorize(Users="ecuque")]

    [Authorize(Roles = "Admin")]
    public class DepartamentoController : Controller
    {
        // GET: Departamento
        // Consulta que se hace al servidor
        public ActionResult Index()
        {
            var dptos = DepartamentoCN.ListarDepartamentos();
            return View(dptos);
        }
        
        public ActionResult Crear()
        {
            return View();
        }
        // yo Envio informacion desde el formulario
        // al servidor
        [HttpPost]
        public ActionResult Crear(Departamento dpto)
        {
            try
            {
                if (dpto.NombreDepartamento == null)
                {
                    ModelState.AddModelError("", "Debe Ingresar un Nombre de Departamento");
                    return View(dpto);
                }



                DepartamentoCN.Agregar(dpto);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("","Ocurrio un error al agregar el Departamento"+ex.Message);
                return View(dpto);                
            }
        }

        public ActionResult GetDepartamento(int id)
        {
            var dpto = DepartamentoCN.GetDapartamento(id);
            return View(dpto);
        }

        public JsonResult GetDepartamentos()
        {
            var lista = DepartamentoCN.ListarDepartamentos();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Editar(int id)
        {
            var dpto = DepartamentoCN.GetDapartamento(id);
            return View(dpto);
        }
        [HttpPost]
        public ActionResult Editar(Departamento dpto)
        {
            try
            {
                if (dpto.NombreDepartamento == null)
                {
                    ModelState.AddModelError("", "Debe Ingresar un Nombre de Departamento");
                    return View(dpto);
                }
                DepartamentoCN.Editar(dpto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrio un error al agregar el Departamento"+ex.Message);
                return View(dpto);
            }
        }
        // agregar ? al tipo de variable lo convertimos en null
        // o lo seteamos a null su valor inicial
        public ActionResult Eliminar(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var dpto = DepartamentoCN.GetDapartamento(id.Value);
            return View(dpto);
        }
        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            try
            {
                DepartamentoCN.Eliminar(id);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Ocurrio un error al Elimnar el Departamento");
                return View();
            }
        }

    }
}