using Entidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class EmpleadoDALC
    {
        public void Agregar(Empleado empleado)
        {
            using(var db = new ProyectosContext())
            {
                db.Empleado.Add(empleado);
                db.SaveChanges();
            }
        }
        public List<EmpleadoCE> ListarEmpleados()
        {
            string sql = @"select e.Empleadoid, e.Nombres, e.Apellidos, e.Email, e.Direccion, e.Celular,
                        e.Departamentoid, d.NombreDepartamento
                        from Empleado e
                        inner join Departamento d on e.Departamentoid=d.Departamentoid";
            using(var db = new ProyectosContext())
            {
                //return db.Empleado.ToList(sql).;
                return db.Database.SqlQuery<EmpleadoCE>(sql).ToList();

            }
        }

        public EmpleadoCE ObtenerEmpleado(int id)
        {
            string sql = @"select e.Empleadoid, e.nombres, e.apellidos, e.email, e.direccion, e.celular,
                        e.departamentoid, d.nombredepartamento
                        from empleado e
                        inner join departamento d on e.departamentoid=d.departamentoid
                        where e.empleadoid = @empleadoid";


            using (var db = new ProyectosContext())
            {
                //return db.Empleado.Find(id);
                // si se necesitara mandar mas de un parametro a al consulta sql
                //return db.Database.SqlQuery<Empleado>(sql, 
                //    new SqlParameter("@empleadoid", id),
                //    new SqlParameter("@nombre",nombre)).FirstOrDefault();
                return db.Database.SqlQuery<EmpleadoCE>(sql,
                    new SqlParameter("@empleadoid", id)).FirstOrDefault();
            }
        }

        public void Editar(Empleado empleado)
        {
            using (var db = new ProyectosContext())
            {
                var origen = db.Empleado.Find(empleado.Empleadoid);
                origen.Nombres = empleado.Nombres;
                origen.Apellidos = empleado.Apellidos;
                origen.Email = empleado.Email;
                origen.Direccion = empleado.Direccion;
                origen.Celular = empleado.Celular;
                origen.Departamentoid = empleado.Departamentoid;
                db.SaveChanges();
            }
        }

        public void Eliminar(int id)
        {
            using(var db = new ProyectosContext())
            {
                var empleado = db.Empleado.Find(id);
                db.Empleado.Remove(empleado);
                db.SaveChanges();
            }
        }

    }
}
