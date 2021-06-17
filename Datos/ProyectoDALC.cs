using Entidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ProyectoDALC
    {
        public void Agregar(Proyecto proyecto)
        {
            using(var db = new ProyectosContext())
            {
                db.Proyecto.Add(proyecto);
                db.SaveChanges();
            }
        }
        // Aca solicita una Entidad
        // public List<Proyecto> ListarProyectos()
        // para llamar al procedimiento solicita la clase
        // splistaproyectos_result creada por el entity framework
        public List<Proyecto> ListarProyectos()
        {
            using(var db = new ProyectosContext())
            {
                // Esto se coloca para que la tabla que tiene tablas asociadas
                // no trate de cargarlas el entity framework
                db.Configuration.LazyLoadingEnabled = false;
                // var toDay = DateTime.Now.Date;
                // Filtrar Registros si la Fecha del Proyecto es Mayor a la Fecha Actual
                // return db.Proyecto.Where(p=> p.Fechafin > toDay).ToList();

                // Forma Normal Llamar Listado con Entity framework
                return db.Proyecto.ToList();

                // Forma de llamar un Procedimiento almacenado con Entity framework
                // var dataForma1 = db.spListaProyectos().ToList();
                // return dataForma1;
                               


            }
        }

        public Proyecto ObtenerProyecto(int id)
        {
            using(var db = new ProyectosContext())
            {
                // llamar SP con parametros seria de la siguiente manera CON ENTITY FRAMEWORK
                // var miProyecto = db.spListaProyectos(id,proyectoid,empleadoid).FirstOrDefault();

                return db.Proyecto.Find(id);

                // si fueran mas parametros a enviar al procedimiento almacenado se haria
                // de la siguiente manera
                //var miProyecto = db.Database.SqlQuery<Proyecto>("sp_ObteberProyecto @ProyectoId,@EmpleadoId",
                //    new SqlParameter("@ProyectoId", proyectoid),
                //    new SqlParameter("@EmpleadoId", empleadoid)
                //    ).FirstOrDefault();

                // USO DE PROCEDIMIENTO ALMACENADO 
                //var miProyecto = db.Database.SqlQuery<Proyecto>("sp_ObteberProyecto @ProyectoId",
                //    new SqlParameter("@ProyectoId", id)
                //    ).FirstOrDefault();

                //return miProyecto;

            }
        }

        public void Editar(Proyecto proyecto)
        {
            using(var db = new ProyectosContext())
            {
                var origen = db.Proyecto.Find(proyecto.Proyectoid);
                origen.NombreProyecto = proyecto.NombreProyecto;
                origen.Fechainicio = proyecto.Fechainicio;
                origen.Fechafin = proyecto.Fechafin;
                db.SaveChanges();

            }
        }

        public void Eliminar(int id)
        {
            using (var db = new ProyectosContext())
            {
                var proyecto = db.Proyecto.Find(id);
                db.Proyecto.Remove(proyecto);
                db.SaveChanges();
            }
        }

        public bool ExisteAsignacion(int proyectoid, int empleadoid)
        {
            using (var db = new ProyectosContext())
            {
                var existeAsignacion = db.ProyectoEmpleado.
                    Any(p => p.ProyectoId == proyectoid && p.EmpleadoId == empleadoid);
                return existeAsignacion;
            }
        }

        public bool esProyectoActivo(int proyectoid)
        {
            using(var db = new ProyectosContext())
            {
                var toDay = DateTime.Now.Date;
                var proyectoActivo = db.Proyecto
                    .Any(p => p.Proyectoid == proyectoid && p.Fechafin > toDay);
                return proyectoActivo;
            }
        }

        public void AsignarProyecto(int proyectoid, int empleadoid)
        {
            var proyectoEmp = new ProyectoEmpleado
            {
                ProyectoId = proyectoid,
                EmpleadoId = empleadoid,
                FechaAlta = DateTime.Now
            };
            using(var db = new ProyectosContext())
            {
                db.ProyectoEmpleado.Add(proyectoEmp);
                db.SaveChanges();
            }
        }

        public List<ProyectoEmpleadoCE> ListarAsignaciones()
        {
            string strSQL = @"SELECT PE.PROYECTOID, P.NOMBREPROYECTO, PE.EMPLEADOID, E.APELLIDOS, E.NOMBRES, PE.FECHAALTA
                            FROM PROYECTOEMPLEADO PE
                            INNER JOIN PROYECTO P ON PE.PROYECTOID = P.PROYECTOID
                            INNER JOIN EMPLEADO E ON PE.EMPLEADOID = E.EMPLEADOID";

            using (var db = new ProyectosContext())
            {
                return db.Database.SqlQuery<ProyectoEmpleadoCE>(strSQL).ToList();
            }
        }

        public void EliminarAsignacio(int proyectoid, int empleadoid)
        {
            using(var db = new ProyectosContext())
            {
                var empProy = db.ProyectoEmpleado.
                    Where(e => e.ProyectoId == proyectoid && e.EmpleadoId == empleadoid)
                    .FirstOrDefault();
                db.ProyectoEmpleado.Remove(empProy);
                db.SaveChanges();
            }
        }


    }
}
