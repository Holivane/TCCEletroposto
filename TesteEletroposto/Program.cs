using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaOpenDSS;
using BibliotecaOpenDSS.Models;

namespace TesteEletroposto
{
    class Program
    {
        //ananda
        static void Main(string[] args)
        {
            //Holivane 
            Eletroposto lib = new Eletroposto("C:\\Users\\anand\\Documents\\TCCEletroposto\\TCCEletroposto\\ApiEletroposto\\Content\\Rede\\Sinap_Rede_CAI_teste_sem_trafos.dss");

            bool verificararquivoderede = lib.RunFile();
           

            BibliotecaOpenDSS.Models.Carga carga = new BibliotecaOpenDSS.Models.Carga();


            carga.Barra = "1115";
            carga.Nome = "CargaTeste";
            carga.PotenciaTotal = 50000;

            lib.AddLoad(carga);

            bool resolverrede = lib.Solve();

            List<Barra> xyz = new List<Barra>();
            xyz = lib.AllScore();
            foreach(Barra b in xyz)
            {
                Console.Out.WriteLine(b.barra);
                Console.Out.WriteLine(b.latitudade.ToString());
                Console.Out.WriteLine(b.longitude.ToString());
            }

            Console.Out.WriteLine("Quantidade de barras: " + lib.GetCountBus());
            Console.ReadLine();

        }
    }
}
