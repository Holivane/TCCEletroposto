using ApiEletroposto.Models;
using BibliotecaOpenDSS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ApiEletroposto.Controllers
{
    public class DefaultController : ApiController
    {
        // GET: Default
        public string Get(string filename)
        {
            List<Barra> lista = new List<Barra>();
            lista = BibliotecaOpenDSS.Utils.Utils.LerArquivo(filename);

            apitcceletroEntities db = new apitcceletroEntities();

           // int count = 1;

            Barras barra = null;
            foreach (Barra b in lista)
            {
                barra = new Barras();
                barra.Barra = b.CodBarra;
                barra.Latitude = Convert.ToDecimal(b.Latitudade);
                barra.Longitude = Convert.ToDecimal(b.Longitude);
                barra.Rede = b.Rede;
                //barra.id = count++;
                db.Barras.Add(barra);
            }
            db.SaveChanges();
            return "Dados enviados com sucesso!";
        }
    }
}