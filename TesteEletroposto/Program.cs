using System;
using System.Collections.Generic;
using System.IO;
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
            //Console.WriteLine("___________________________________________");
            //Console.WriteLine();
            //Console.WriteLine("Ler Arquivo CSV");
            //Console.WriteLine();
            //Console.WriteLine("___________________________________________");
            


            //Holivane 
            Eletroposto lib = new Eletroposto("C:\\Users\\anand\\Documents\\TCCEletroposto\\TCCEletroposto\\ApiEletroposto\\Content\\Rede\\Sinap_Rede_CAI_teste_sem_trafos.dss");

            bool verificararquivoderede = lib.RunFile();


            BibliotecaOpenDSS.Models.Carga carga = new Carga
            {
                Barra = "1115",
                Nome = "CargaTeste",
                PotenciaTotal = 50000
            };
                       

            lib.AddLoad(carga);

            //lib.Solve();

            List<Barra> b = new List<Barra>();
            b = lib.AllScore();

            //TRECHOS
            List<Trecho> x = new List<Trecho>();
            x = lib.TodosTrechos();
            
            foreach (Trecho t in x)
            {
                Console.Out.WriteLine(t.CodTrecho + " --- " + t.Comprimento + "---" + t.Parametro + "---" + t.barra2.CodBarra);

            }


            Console.ReadLine();

        }
    }
}
