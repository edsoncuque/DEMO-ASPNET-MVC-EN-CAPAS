﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entidad
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ProyectosContext : DbContext
    {
        public ProyectosContext()
            : base("name=ProyectosContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Departamento> Departamento { get; set; }
        public virtual DbSet<Proyecto> Proyecto { get; set; }
        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<ProyectoEmpleado> ProyectoEmpleado { get; set; }
    
        public virtual ObjectResult<spListaProyectos_Result> spListaProyectos()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spListaProyectos_Result>("spListaProyectos");
        }
    }
}
