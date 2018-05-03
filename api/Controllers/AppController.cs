using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace api.Controllers
{
    public class AppController : Controller
    {
        // GET: App
        public ActionResult Index()
        {
            return View();
        }

        // GET: Registre-se
        public ActionResult Cadastro()
        {
            return View();
        }

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        // GET: Perfil
        public ActionResult Perfil()
        {
            return View();
        }

        // GET: Alteração de Dados
        public ActionResult AltDados()
        {
            return View();
        }

        // GET: Simulação
        public ActionResult Simulacao()
        {
            return View();
        }
        
        // GET: Sobre
        public ActionResult Sobre()
        {
            return View();
        }
        // GET: Alteração de Dados
        public ActionResult AltSenha()
        {
            return View();
        }
    }
}