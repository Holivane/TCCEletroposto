using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaOpenDSS.Models
{
    public class Trecho
    {
        public string CodTrecho { get; set; }
        public string Parametro { get; set; }
        public double INom { get; set; }
        public double IAtual { get; set; }
        public double ICalculada { get; set; }
        public double Comprimento { get; set; }
        public Barra barra2 { get; set; }
    }
}
