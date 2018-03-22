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

            this.DSSText.Command = "Set Mode = Snapshot";

            retorno = true;

            return retorno;
        }
        
        public void AddLoad(Carga carga)
        {
            this.DSSText.Command = "NEW " + carga.Nome;
        }

        public float ConvertPower(float power)
        {
            return power * 100;
        }

        public int GetCountBus()
        {
            return this.DSSCircuit.NumBuses;
        }

        public List<Barra> AllScore()
        {
            List<Barra> list = new List<Barra>();
            Barra barra = null;

            for (int i = 0; i < this.DSSCircuit.NumBuses; i++)
            {
                barra = new Barra();
                DSSBus = DSSCircuit.Buses[i];

                //DSSBus.
                //Ativar a barra para obter as informações de coordenadas para a geolocalização
                barra.barra = DSSBus.Name;
                barra.latitudade = DSSBus.x;
                barra.longitude = DSSBus.y;
                

                list.Add(barra);
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
