//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ROL_USUARIO
    {
        public decimal ID_ROL_USER { get; set; }
        public decimal ID_ROL { get; set; }
        public decimal ID_EMPLEADO { get; set; }
    
        public virtual EMPLEADOS EMPLEADOS { get; set; }
        public virtual ROLES ROLES { get; set; }
    }
}