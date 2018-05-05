using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaOpenDSS.Models
{
    public class Barra
    {
        public dynamic Tensaopu { get; set; }
        public double TensaoBase { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Rede { get; set; }
        public string NomeRede { get; set; }
        public string CodBarra { get; set; }
        public string NomeBarra { get; set; }
        public double Score { get; set; }
        public double Mtotal { get; set; }
        public int Hora { get; set; }
    }
}
