using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaOpenDSS.Models;
using OpenDSSengine;

namespace BibliotecaOpenDSS
{
    public class Eletroposto
    {       
        private DSS DSSobj;
        private Text DSSText;
        private Circuit DSSCircuit;
        private Solution DSSSolution;
        private CtrlQueue DSSConttrolQueue;
        private CmathLib DSSCmath;
        private CktElement DSSCktElement;
        private PDElements DSSPDElement;
        private Bus DSSBus;
        private Loads DSSLoads;
        private Lines DSSLines;
        private LineCodes DSSLineCodes;

        public string FileName;
        public Boolean IsStarted;

        public Eletroposto(string FileName)
        {
           this.DSSobj = new DSS();
            if (!this.DSSobj.Start(0))
            {
                this.IsStarted = false;
            }
            else
            {
                this.IsStarted = true;
                this.FileName = FileName;
                this.DSSText = this.DSSobj.Text;
            }

        }

        public bool RunFile()
        {
            bool retorno = false;

            this.DSSText.Command = "Clear";
            this.DSSText.Command = "Compile " + this.FileName;

            this.DSSCircuit = DSSobj.ActiveCircuit;


            this.DSSSolution = DSSCircuit.Solution;
            this.DSSConttrolQueue = DSSCircuit.CtrlQueue;
            this.DSSCmath = DSSobj.CmathLib;
            this.DSSPDElement = DSSCircuit.PDElements;
            this.DSSCktElement = DSSCircuit.ActiveCktElement;
            this.DSSBus = DSSCircuit.ActiveBus;
            this.DSSLoads = DSSCircuit.Loads;
            this.DSSLines = DSSCircuit.Lines;
            this.DSSLineCodes = DSSCircuit.LineCodes;
         
            this.DSSText.Command = "Set Mode = Snapshot";

            retorno = true;

            return retorno;
        }


        public List<Barra> GetBus()
        {
            List<Barra> buses = new List<Barra>();
            Trecho trecho = new Trecho();
            Barra barra1 = null;
            Barra barra2 = null;

            bool verificararquivoderede = this.RunFile();
            bool solve = this.Solve();
            int LineCount = DSSLines.First;
            
            for (int i = 0; i < this.DSSLines.Count; i++)
            {
                trecho = new Trecho();

                if (DSSCktElement.CurrentsMagAng[0] > 0.1)
                {
                    barra1 = new Barra
                    {
                        CodBarra = DSSLines.Bus1.Split('.')[0]
                    };

                    trecho.barra1 = barra1;


                    barra2 = new Barra
                    {
                        CodBarra = DSSLines.Bus2.Split('.')[0]
                    };
                }

                if (!buses.Contains(barra1))
                {
                    buses.Add(barra1);
                }

                if (!buses.Contains(barra2))
                {
                    buses.Add(barra2);
                }

                LineCount = DSSLines.Next;
            }
            return buses;
        }

        public List<Barra> CalcScore(List<Barra> listabarras, Barra barra, Carga carga)
        {
            //fazer o calculo
            List<Barra> barrasmodificadas = new List<Barra>();
            bool verificararquivoderede = this.RunFile();

            bool solve = this.Solve();
            List<Trecho> TrechosAntes = new List<Trecho>();
            TrechosAntes = this.TodosTrechos(listabarras, barra);

            this.AddLoad(carga);

            solve = this.Solve();
            List<Trecho> TrechosDepois = new List<Trecho>();
            TrechosDepois = this.TodosTrechos(listabarras, barra);

            List<Trecho> trechosmodficados = new List<Trecho>();

            double mtotal = 0;

            int index = 0;
            foreach(Trecho t in TrechosAntes)
            {
                
                if ((1.08 * t.IAtual) < TrechosDepois[index].IAtual && TrechosDepois[index].IAtual > 1)
                {
                    trechosmodficados.Add(TrechosDepois[index]);
                    if ((TrechosDepois[index].INom * 0.9) <= TrechosDepois[index].IAtual)
                    {
                        mtotal = mtotal + (TrechosDepois[index].Comprimento * 1000);
                    }
                }
                
                index++;                
            }
            

            foreach(Barra b in listabarras)
            {
                b.Score = -1;
                if (b.CodBarra == barra.CodBarra)
                {
                    b.Score = 10 - (0.05 * mtotal);

                    if (b.Score < 0)
                    {
                        b.Score = 0;
                    }
                }
                else
                {
                    foreach (Trecho t in trechosmodficados)
                    {

                        if (t.barra1 != null)
                        {
                            if (t.barra1.CodBarra == b.CodBarra)
                            {
                                b.Score = -2;
                            }
                        }
                        if (t.barra2 != null)
                        {
                            if (t.barra2.CodBarra == b.CodBarra)
                            {
                                b.Score = -2;
                            }
                        }

                    }
                }
                barrasmodificadas.Add(b);

            }

            return barrasmodificadas;
        }


        public void AddLoad(Carga carga)
        {

            this.DSSText.Command = "New Load." + carga.Nome + " phases=3 model=5 bus1=" + carga.Barra + ".1.2.3 conn=delta kv=13.80000019 vminpu=0.800 kw=" + carga.PotenciaTotal.ToString() + " kvar=0 daily=225";
        }

        //public float ConvertPower(float power)
        //{
        //    return power * 1000;
        //}

        public int GetCountBus()
        {
            return this.DSSCircuit.NumBuses;
        }
        
        //TRECHOS
        public List<Trecho> TodosTrechos(List<Barra> listabarras, Barra barra)
        {
            List<Trecho> lista = new List<Trecho>();
            Trecho trecho = null;
            Barra barra1 = null;
            Barra barra2 = null;

            int LineCount = DSSLines.First;

            for (int i = 0; i < this.DSSLines.Count; i++)
            {
                                
                trecho = new Trecho();

                DSSLines = DSSCircuit.Lines;

                trecho.CodTrecho = DSSLines.Name;

                DSSCircuit.SetActiveElement(trecho.CodTrecho);
                DSSLineCodes.Name = DSSLines.LineCode;

                trecho.INom = DSSLineCodes.NormAmps;
                trecho.IAtual = DSSCktElement.CurrentsMagAng[0];
                trecho.Comprimento = DSSLines.Length;


                barra1 = new Barra();
                trecho.barra1 = this.getBarra(DSSLines.Bus1.Split('.')[0].ToString(), listabarras);


                barra2 = new Barra();
                trecho.barra2 = this.getBarra(DSSLines.Bus2.Split('.')[0].ToString(), listabarras);
                trecho.Parametro = DSSLines.LineCode;
                
                if (trecho.barra1 != null)
                {
                    if (this.getBarra(trecho.barra1.CodBarra, listabarras).Rede == barra.Rede)
                    {
                        lista.Add(trecho);
                    }
                }

                else if (trecho.barra2 != null)
                {
                    if (this.getBarra(trecho.barra2.CodBarra, listabarras).Rede == barra.Rede)
                    {
                        lista.Add(trecho);
                    }
                }


                LineCount = DSSLines.Next;
            }

            return lista;
        }


        public Barra getBarra(String name, List<Barra> list)
        {
            Barra b = null;
            foreach (Barra f in list)
            {
                if (f.CodBarra == name)
                {
                    b = new Barra();
                    b = f;
                }
            }
            return b;
        }

        public List<Trecho> Info(List<Trecho> lista)
        {
            foreach (Trecho t in lista)
            {
                //DSSCircuit.SetActiveElement(t.trecho);
                //t.comprimento = DSSLines.Length;
                Lines ll = DSSLines;

                // parâmetro
                //t.linecode = DSSLines.LineCode;
                
                double amp = DSSLineCodes.NormAmps;

                // barra 2
                string xbarra = DSSLines.Bus2;
                string[] barra2 = xbarra.Split(new char[1] {'.'});
                                
                //this.DSSBus = DSSCircuit.Buses[barra2];
                
            }
            return lista;
        }

        //BARRAS
        public List<Barra> AllScore()
        {
            List<Barra> list = new List<Barra>();
            Barra barra = null;

            StreamWriter writer = new StreamWriter("Barras.txt");

            for (int i = 0; i < this.DSSCircuit.NumBuses; i++)
            {
                barra = new Barra();
                DSSBus = DSSCircuit.Buses[i];
                //Ativar a barra para obter as informações de coordenadas para a geolocalização
                writer.WriteLine(DSSBus.Name);
                barra.CodBarra = DSSBus.Name;
                list.Add(barra);

            }
            writer.Close();
            return list;
        }

        //CARGA
        public List<Carga> TodasCargas()
        {
            List<Carga> lista = new List<Carga>();
            Carga carga = null;
            Barra barra = null;
            int ContarCarga = DSSLoads.First;

            for (int i = 0; i < this.DSSLoads.Count; i++)
            {

                carga = new Carga();

                DSSLoads = DSSCircuit.Loads;

                carga.Nome = DSSLoads.Name;
                DSSCircuit.SetActiveElement(carga.Nome);
                carga.PotenciaTotal = DSSLoads.kW;

                lista.Add(carga);

                ContarCarga = DSSLoads.Next;
            }

            return lista;
        }


        public List<Barra> GetCoordinates(List<Barra> list)
        {
            foreach(Barra b in list)
            {
                // DSSCircuit.SetActiveBus(b.barra);
                double d = DSSBus.kVBase;
                Bus bb = DSSBus;
                //b.longitude = DSSBus.y;
            }
            return list;
        }

        

        public bool Solve()
        {

            bool retorno = false;
            this.DSSSolution.Solve();

            if (this.DSSSolution.Converged)
            {
                retorno = true;
            }
            return retorno;
        }
    }
}
