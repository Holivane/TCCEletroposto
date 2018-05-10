using BibliotecaOpenDSS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaOpenDSS.Uteis
{
    public class Uteis
    {
        public static List<Barra> LerArquivo(string FileName)
        {
            List<Barra> ListadeBarras = new List<Barra>();
            try
            {
                //Declaro o StreamReader para o caminho onde se encontra o arquivo 
                FileName = @"C:\Users\anand\Documents\TCCEletroposto\TCCEletroposto\Canindé\VBA\Barrasid_vf.csv";
                StreamReader rd1 = new StreamReader(FileName);
                
                //Declaro uma string que será utilizada para receber a linha completa do arquivo 
                string linha = null;
                //Declaro um array do tipo string que será utilizado para adicionar o conteudo da linha separado 
                string[] linhaseparada = null;
                //realizo o while para ler o conteudo da linha 
                Barra barra = null;
                //int count = 0;

                while ((linha = rd1.ReadLine()) != null)
                {
                    //com o split adiciono a string 'quebrada' dentro do array 
                    linhaseparada = linha.Split(';');
                    //aqui incluo o método necessário para continuar o trabalho 
                    Console.Out.WriteLine(linhaseparada[0]);

                    barra = new Barra
                    {
                        CodBarra = linhaseparada[3],
                        Latitude = Convert.ToDouble(linhaseparada[6]),
                        Longitude = Convert.ToDouble(linhaseparada[5]),
                        Rede = linhaseparada[1],
                        NomeRede = linhaseparada[0],
                        NomeBarra = linhaseparada[2]
                    };

                    ListadeBarras.Add(barra);
                }
                rd1.Close();
                
            }
            catch
            {
                //Console.WriteLine("Erro ao executar Leitura do Arquivo");
            }
            return ListadeBarras;
        }

        public static List<Barra> LerArqCC(string FileName)
        {
            List<Barra> ListadeBarras = new List<Barra>();
            try
            {
                //Declaro o StreamReader para o caminho onde se encontra o arquivo 
                FileName = @"C:\Users\anand\Documents\TCCEletroposto\TCCEletroposto\Canindé\VBA\Barra_NCC.csv";
                StreamReader rd1 = new StreamReader(FileName);

                //Declaro uma string que será utilizada para receber a linha completa do arquivo 
                string linha = null;
                //Declaro um array do tipo string que será utilizado para adicionar o conteudo da linha separado 
                string[] linhaseparada = null;
                //realizo o while para ler o conteudo da linha 
                Barra barra = null;
                //int count = 0;

                while ((linha = rd1.ReadLine()) != null)
                {
                    //com o split adiciono a string 'quebrada' dentro do array 
                    linhaseparada = linha.Split(';');
                    //aqui incluo o método necessário para continuar o trabalho 
                    Console.Out.WriteLine(linhaseparada[0]);

                    barra = new Barra
                    {
                        CodBarra = linhaseparada[0],
                        NivelCC = Convert.ToDouble(linhaseparada[1])
     
                    };

                    ListadeBarras.Add(barra);
                }
                rd1.Close();

            }
            catch
            {
                //Console.WriteLine("Erro ao executar Leitura do Arquivo");
            }
            return ListadeBarras;
        }

        public static List<Barra> LerArqTN(string FileName)
        {
            List<Barra> ListadeBarras = new List<Barra>();
            try
            {
                //Declaro o StreamReader para o caminho onde se encontra o arquivo 
                FileName = @"C:\Users\anand\Documents\TCCEletroposto\TCCEletroposto\Canindé\VBA\Barrasid_vf.csv";
                StreamReader rd1 = new StreamReader(FileName);

                //Declaro uma string que será utilizada para receber a linha completa do arquivo 
                string linha = null;
                //Declaro um array do tipo string que será utilizado para adicionar o conteudo da linha separado 
                string[] linhaseparada = null;
                //realizo o while para ler o conteudo da linha 
                Barra barra = null;
                //int count = 0;

                while ((linha = rd1.ReadLine()) != null)
                {
                    //com o split adiciono a string 'quebrada' dentro do array 
                    linhaseparada = linha.Split(';');
                    //aqui incluo o método necessário para continuar o trabalho 
                    Console.Out.WriteLine(linhaseparada[0]);

                    barra = new Barra
                    {
                        CodBarra = linhaseparada[3],
                        TensaoBase = Convert.ToDouble(linhaseparada[4])
                    };

                    ListadeBarras.Add(barra);
                }
                rd1.Close();

            }
            catch
            {
                //Console.WriteLine("Erro ao executar Leitura do Arquivo");
            }
            return ListadeBarras;
        }
    }
}
