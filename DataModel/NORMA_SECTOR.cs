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
    
    public partial class NORMA_SECTOR
    {
        public int IdNormaSector { get; set; }
        public int IdNorma { get; set; }
        public int IdSectorServicio { get; set; }
        public int IdEstado { get; set; }
    
        public virtual NORMA NORMA { get; set; }
        public virtual SECTOR_SERVICIO SECTOR_SERVICIO { get; set; }
    }
}
