using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace api.Controllers
{
    [RoutePrefix("api/eletroposto")]
    public class TCCController : ApiController
    {
        TCCELETROPOSTOEntities db = new TCCELETROPOSTOEntities();
        
        [AcceptVerbs("GET")]
        [Route("getbus")]
        public List<Barras> getbus()
        {
            return db.Barras.ToList();
        }

        [AcceptVerbs("POST")]
        [Route("addbus")]
        public List<Barras> addbus(markerbase marker)
        {

            //Math.PI
            //Determinar o raio
            //Obter todas as barras dentro de um raio
            
            return db.Barras.ToList();
        }

        [AcceptVerbs("POST")]
        [Route("calcscore")]
        public List<Barras> calcscore(Barras barra)
        {

            //Math.PI
            //Determinar o raio
            //Obter todas as barras dentro de um raio
            
            return db.Barras.ToList();
        }

    }
}