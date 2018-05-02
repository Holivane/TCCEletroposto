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

            //rede rede_id barra barra_id    vnom_kv utm_x   utm_y
            BibliotecaOpenDSS.Uteis.Uteis.LerArquivo("");

       //Holivane 
       //Eletroposto lib = new Eletroposto("C:\\Users\\anand\\Documents\\TCCEletroposto\\TCCEletroposto\\ApiEletroposto\\Content\\Rede\\Sinap_Rede_CAI_teste_sem_trafos.dss");

            //     bool verificararquivoderede = lib.RunFile();


            //     BibliotecaOpenDSS.Models.Carga carga = new Carga
            //     {
            //         Barra = "1115",
            //         Nome = "CargaTeste",
            //         PotenciaTotal = 50000
            //     };


            //     bool solve = lib.Solve();
            //     //TRECHOS
            //     List<Trecho> x = new List<Trecho>();
            //     x = lib.TodosTrechos();

            //     foreach (Trecho t in x)
            //     {
            //         if (t.barra2.CodBarra.Equals("1508"))
            //         {
            //             Console.Out.WriteLine(t.CodTrecho + " --- " + t.IAtual);
            //         }

            //     }



            //     lib.AddLoad(carga);

            //     solve = lib.Solve();

            //     //TRECHOS
            //     List<Trecho> xd = new List<Trecho>();
            //     xd = lib.TodosTrechos();

            //     foreach (Trecho t in xd)
            //     {
            //         if (t.barra2.CodBarra.Equals("1508"))
            //         {
            //             Console.Out.WriteLine(t.CodTrecho + " --- " + t.IAtual);
            //         }


            //     }


            //     List<Barra> b = new List<Barra>();
            //     b = lib.AllScore();

            //     //TRECHOS
            //     List<Carga> c = new List<Carga>();
            //     c = lib.TodasCargas();

            //     foreach (Carga t in c)
            //     {
            //         //Console.Out.WriteLine(t.Nome + " --- " + t.PotenciaTotal);

            //     }


            Console.ReadLine();

        }
    }
}
