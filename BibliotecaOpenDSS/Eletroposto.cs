using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            DSSobj = new DSS();
            if (!DSSobj.Start(0))
            {
                this.IsStarted = false;
            }
            else
            {
                this.IsStarted = true;
                DSSText = DSSobj.Text;
            }

        }

    }
}
