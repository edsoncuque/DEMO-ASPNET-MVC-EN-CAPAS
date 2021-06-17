using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DepartamentoDALC
    {
        public void Agregar(Departamento dpto)
        {
            using(var db = new ProyectosContext())
            {
                db.Departamento.Add(dpto);
                db.SaveChanges();
            }

        }
        
        public List<Departamento> ListarDepartamentos()
        {
            using (var db = new ProyectosContext())
            {
                // carga difusa
                db.Configuration.LazyLoadingEnabled = false;
                return db.Departamento.ToList();
            }
        }

        public Departamento GetDapartamento(int id)
        {
            using (var db = new ProyectosContext())
            {
                //return db.Departamento.Find(id);
                return db.Departamento.Where(d => d.Departamentoid == id).FirstOrDefault();
            }
        }

        public void Editar(Departamento dpto)
        {
            using (var db = new ProyectosContext())
            {
                var d = db.Departamento.Find(dpto.Departamentoid);
                d.NombreDepartamento = dpto.NombreDepartamento;
                db.SaveChanges();
            }
        }

        public void Eliminar(int id)
        {
            using(var db = new ProyectosContext())
            {
                var dpto = db.Departamento.Find(id);
                db.Departamento.Remove(dpto);
                db.SaveChanges();
            }
        }


    }
}
