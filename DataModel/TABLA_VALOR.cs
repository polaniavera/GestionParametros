//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class TABLA_VALOR
    {
        public int IdTablaValor { get; set; }
        public int IdTablaRegistro { get; set; }
        public int IdCampo { get; set; }
        public string ValorAlfanumerico { get; set; }
        public Nullable<int> ValorEntero { get; set; }
        public Nullable<System.DateTime> ValorFecha { get; set; }
        public Nullable<bool> ValorBoleano { get; set; }
        public Nullable<decimal> ValorDinero { get; set; }
        public Nullable<decimal> ValorTasa { get; set; }
    
        public virtual TABLA_CAMPO TABLA_CAMPO { get; set; }
        public virtual TABLA_REGISTRO TABLA_REGISTRO { get; set; }
    }
}
