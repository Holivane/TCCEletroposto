//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ApiEletroposto.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Barras
    {
        public long id { get; set; }
        public string Barra { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public string Rede { get; set; }
        public string NomedaBarra { get; set; }
        public string NomedaRede { get; set; }
    }
}
