using System;
using System.Collections.Generic;
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
            this.DSSLines = DSSCircuit.Lines;
         
            this.DSSText.Command = "Set Mode = Snapshot";

            retorno = true;

            return retorno;
        }
        
        public void AddLoad(Carga carga)
        {
            //this.DSSText.Command = "NEW " + carga.Nome;
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
        public List<Trecho> CodTrechos()
        {
            List<Trecho> lista = new List<Trecho>();
            Trecho trecho = null;
            int LineCount = DSSLines.First;

            for (int i = 0; i < this.DSSLines.Count; i++)
            {
                trecho = new Trecho();
                DSSLines = DSSCircuit.Lines;
                
                trecho.trecho = DSSLines.Name;
                lista.Add(trecho);

                LineCount = DSSLines.Next;
            }

            return lista;
        }

        //BARRAS
        public List<Barra> AllScore()
        {
            List<Barra> list = new List<Barra>();
            Barra barra = null;

            for (int i = 0; i < this.DSSCircuit.NumBuses; i++)
            {
                barra = new Barra();
                DSSBus = DSSCircuit.Buses[i];
                //Ativar a barra para obter as informações de coordenadas para a geolocalização
                barra.barra = DSSBus.Name;
                list.Add(barra);
            }

            return list;
        }


        public List<Barra> GetCoordinates(List<Barra> list)
        {
            foreach(Barra b in list)
            {
                DSSCircuit.SetActiveBus(b.barra);
                double d = DSSBus.kVBase;
                Bus bb = DSSBus;
                b.longitude = DSSBus.y;
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
