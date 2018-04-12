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
    }
}