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
    
    public partial class FORMATO_ACUERDO
    {
        public int IdFormatocAuerdo { get; set; }
        public int IdAcuerdo { get; set; }
        public int IdFormatoPlantilla { get; set; }
        public int IdEstado { get; set; }
    
        public virtual ACUERDO ACUERDO { get; set; }
        public virtual FORMATO_PLANTILLA FORMATO_PLANTILLA { get; set; }
    }
}
