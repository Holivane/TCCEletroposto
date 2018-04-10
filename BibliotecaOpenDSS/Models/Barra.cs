using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaOpenDSS.Models
{
    public class Barra
    {
        public string CodBarra { get; set; }
        public dynamic Tensaopu { get; set; }
        public double TensaoBase { get; set; }
        public double Longitude { get; set; }
        public double Latitudade { get; set; }
        public string Rede { get; set; }

    }
}
