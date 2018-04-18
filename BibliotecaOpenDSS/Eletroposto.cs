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

            DSSCircuit = DSSobj.ActiveCircuit;


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
        
        public void AddLoad(Carga carga)
        {
            this.DSSText.Command = "New Load.xxxx_ixxx phases=3 model=5 bus1=1508.1.2.3 conn=delta kv=13.80000019 vminpu=0.800 kw=500 kvar=0 daily=755";
        }

        public float ConvertPower(float power)
        {
            return power * 100;
        }

        public int GetCountBus()
        {
            return this.DSSCircuit.NumBuses;
        }
        
        //TRECHOS
        public List<Trecho> TodosTrechos()
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

                barra1 = new Barra
                {
                    CodBarra = DSSLines.Bus1.Split('.')[0]
                };

                trecho.barra1 = barra1;


                barra2 = new Barra
                {
                    CodBarra = DSSLines.Bus2.Split('.')[0]
                };

                trecho.barra2 = barra2;
                                  
                    
                trecho.Parametro = DSSLines.LineCode;


                lista.Add(trecho);

                LineCount = DSSLines.Next;
            }

            return lista;
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
