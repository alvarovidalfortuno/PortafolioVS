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
    
    public partial class TAREAS
    {
        public decimal ID_TAREA { get; set; }
        public string NOMBRE_TAREA { get; set; }
        public string DESC_TAREA { get; set; }
        public Nullable<System.DateTime> FECHA_INICIO { get; set; }
        public Nullable<decimal> DEADLINE { get; set; }
        public Nullable<System.DateTime> FECHA_TERMINO { get; set; }
        public Nullable<decimal> DIAS_TOTAL { get; set; }
        public Nullable<decimal> ID_USUARIO { get; set; }
        public Nullable<decimal> ID_CLIENTE { get; set; }
        public Nullable<decimal> ID_ESTADO { get; set; }
    }
}
