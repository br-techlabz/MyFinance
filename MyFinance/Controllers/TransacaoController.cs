using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models;

namespace MyFinance.Controllers
{
    public class TransacaoController : Controller
    {
        IHttpContextAccessor HttpContextAccessor;

        //rcebe o contexto para acesso as variáveis de sessão
        public TransacaoController(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }


        public IActionResult Index()
        {
            TransacaoModel objTransacao = new TransacaoModel(HttpContextAccessor);
            ViewBag.ListaTransacoes = objTransacao.ListaTransacoes();

            return View();
        }

        public IActionResult Extrato()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }
    }
}