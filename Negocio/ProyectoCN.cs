using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad;
using Datos;

namespace Negocio
{
    public class ProyectoCN
    {
        private static ProyectoDALC obj = new ProyectoDALC();

        public static void Agregar(Proyecto proyecto)
        {
            obj.Agregar(proyecto);
        }
        // en la capa negocio solicitamos un listado de la entidad
        // proyecto
        // public static List<Proyecto> ListarProyectos()
        // pero para llamar al procedimiento almacenado solicita
        // una lista de la clase especial creada para el procedimiento
        // almacenado spListaProyectos_Result
        public static List<Proyecto> ListarProyectos()
        {
            return obj.ListarProyectos();
        }


        public static Proyecto ObtenerProyecto(int id)
        {
            return obj.ObtenerProyecto(id);
        }

        public static void Editar(Proyecto proyecto)
        {
            obj.Editar(proyecto);
        }

        public static void Eliminar(int id)
        {
            obj.Eliminar(id);
        }

        public static bool ExisteAsignacion(int proyectoid, int empleadoid)
        {
            return obj.ExisteAsignacion(proyectoid, empleadoid);
        }

        public static bool esProyectoActivo(int proyectoid)
        {
            return obj.esProyectoActivo(proyectoid);
        }


        public static void AsignarProyecto(int proyectoid, int empleadoid)
        {
            obj.AsignarProyecto(proyectoid, empleadoid);
        }

        public static List<ProyectoEmpleadoCE> ListarAsignaciones()
        {
            return obj.ListarAsignaciones();
        }

        public static void EliminarAsignacio(int proyectoid, int empleadoid)
        {
            obj.EliminarAsignacio(proyectoid, empleadoid);
        }
    }
}
