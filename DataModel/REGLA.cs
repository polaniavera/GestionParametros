//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class REGLA
    {
        public REGLA()
        {
            this.CAMPO_REGLA = new HashSet<CAMPO_REGLA>();
        }
    
        public int IdRegla { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int IdTipoRegla { get; set; }
        public string Operador1 { get; set; }
        public int Valor1 { get; set; }
        public string OperadorConector { get; set; }
        public string Operador2 { get; set; }
        public int Valor2 { get; set; }
        public int IdNotificacion { get; set; }
        public int IdVigencia { get; set; }
        public int IdEstado { get; set; }
    
        public virtual ICollection<CAMPO_REGLA> CAMPO_REGLA { get; set; }
    }
}
