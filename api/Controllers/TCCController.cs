﻿using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BibliotecaOpenDSS.Models;

namespace api.Controllers
{
    [RoutePrefix("api/eletroposto")]
    public class TCCController : ApiController
    {
        TCCELETROPOSTOEntities db = new TCCELETROPOSTOEntities();

        //private string arquivoderede = @"D:\HOLIVANE\TCC\TCCEletroposto\Canindé\OpenDSS\Sinap_Rede_CAI_teste_sem_trafos.dss";
       private string arquivoderede = HttpRuntime.AppDomainAppPath + "/Sinap_Rede_CAI_teste_sem_trafos_vf.dss";


        [AcceptVerbs("GET")]
        [Route("getbus")]
        public List<Barra> getbus()
        {
            List<Barra> lista = new List<Barra>();
            Barra barra = null;
            foreach(Barras b in db.Barras.Where(c => !c.Rede.Equals("3360") && !c.Rede.Equals("3361")).ToList())
            {
                barra = new Barra
                {
                    Score = -1,
                    Tensaopu = 0,
                    CodBarra = b.Barra,
                    Latitude = Convert.ToDouble(b.Latitude),
                    Longitude = Convert.ToDouble( b.Longitude),
                    Rede = b.Rede,
                    NomeRede = b.NomedaRede,
                    NomeBarra = b.NomedaBarra,
                    NivelCC = Convert.ToDouble(b.NivelCC),
                    TensaoBase = Convert.ToDouble(b.TensaoNominal)
                };
                lista.Add(barra);
            }
            return lista;
        }

        [AcceptVerbs("POST")]
        [Route("calcscore")]
        public List<Barra> calcscore(Projeto projeto)
        {

            BibliotecaOpenDSS.Eletroposto lib = new BibliotecaOpenDSS.Eletroposto(arquivoderede);
            List<Barra> lista = new List<Barra>();

            Barra barrareturn = null;
            foreach (Barras b in db.Barras.Where(c => !c.Rede.Equals("3360") && !c.Rede.Equals("3361")).ToList())
            {
                barrareturn = new Barra
                {
                    Score = -1,
                    Tensaopu = 0,
                    CodBarra = b.Barra,
                    Latitude = Convert.ToDouble(b.Latitude),
                    Longitude = Convert.ToDouble(b.Longitude),
                    Rede = b.Rede,
                    NomeRede = b.NomedaRede,
                    NomeBarra = b.NomedaBarra,
                    NivelCC = Convert.ToDouble(b.NivelCC),
                    TensaoBase = Convert.ToDouble(b.TensaoNominal)
                };

                lista.Add(barrareturn);
            }

            lista = lib.CalcScore(lista, projeto.barra, projeto.carga);
            return lista;
        }

        [AcceptVerbs("GET")]
        [Route("updatebus")]
        public string updatebus()
        {
            List<Barra> lista = new List<Barra>();
            lista = BibliotecaOpenDSS.Uteis.Uteis.LerArquivo("");

            // int count = 1;
           
            Barras barra = null;
            foreach (Barra b in lista)
            {
                barra = new Barras
                {
                    Barra = b.CodBarra,
                    Latitude = Convert.ToDecimal(b.Latitude),
                    Longitude = Convert.ToDecimal(b.Longitude),
                    Rede = b.Rede,
                    NomedaBarra = b.NomeBarra,
                    NomedaRede = b.NomeRede,
                    NivelCC = Convert.ToDecimal(b.NivelCC)
                };

                db.Barras.Add(barra);
            }
            db.SaveChanges();
            return "Dados enviados com sucesso!";
        }

        [AcceptVerbs("GET")]
        [Route("updatecc")]
        public string updatecc()
        {
            List<Barra> lista = new List<Barra>();
            lista = BibliotecaOpenDSS.Uteis.Uteis.LerArqCC("");
            Barras barra = null;
            foreach (Barra b in lista)
            {
                barra = db.Barras.Where(c => c.Barra.Equals(b.CodBarra)).FirstOrDefault();
                if(barra != null)
                {
                    barra.NivelCC = Convert.ToDecimal(b.NivelCC);
                    db.SaveChanges();
                }
                
            }            
            return "Dados enviados com sucesso!";
        }

        [AcceptVerbs("GET")]
        [Route("updatetn")]
        public string updatetn()
        {
            List<Barra> lista = new List<Barra>();
            lista = BibliotecaOpenDSS.Uteis.Uteis.LerArqTN("");
            Barras barra = null;
            foreach (Barra b in lista)
            {
                barra = db.Barras.Where(c => c.Barra.Equals(b.CodBarra)).FirstOrDefault();
                if (barra != null)
                {
                    barra.TensaoNominal = Convert.ToDecimal(b.TensaoBase);
                    db.SaveChanges();
                }

            }
            return "Dados enviados com sucesso!";
        }
    }
}