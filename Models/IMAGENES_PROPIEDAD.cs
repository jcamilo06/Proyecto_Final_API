//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyecto_Final_API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class IMAGENES_PROPIEDAD
    {
        public int ID_IMAGEN { get; set; }
        public int ID_PROPIEDAD { get; set; }
        public string NOMBRE { get; set; }
    
        public virtual PROPIEDADE PROPIEDADE { get; set; }
    }
}
