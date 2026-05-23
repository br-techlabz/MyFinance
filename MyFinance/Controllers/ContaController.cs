using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models;

namespace MyFinance.Controllers
{
    public class ContaController : Controller
    {

        IHttpContextAccessor HttpContextAccessor;

        //rcebe o contexto para acesso as variáveis de sessão
        public ContaController(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }


        public IActionResult Index()
        {
            ContaModel objConta = new ContaModel(HttpContextAccessor);
            ViewBag.ListaConta = objConta.ListaConta();

            return View();
        }
    }
}