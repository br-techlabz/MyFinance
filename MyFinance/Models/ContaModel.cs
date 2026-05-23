using Microsoft.AspNetCore.Http;
using MyFinance.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class ContaModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Double Saldo { get; set; }
        public int Usuario_Id { get; set; }

        IHttpContextAccessor HttpContextAccessor;

        public ContaModel()
        {

        }

        //rcebe o contexto para acesso as variáveis de sessão
        public ContaModel(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public List<ContaModel> ListaConta()
        {
            List<ContaModel> lista = new List<ContaModel>();
            ContaModel item;

            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");

            string sql = $"SELECT Id, Nome, Saldo, Usuario_Id from CONTA WHERE Usuario_Id = {id_usuario_logado}";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ContaModel();
                item.Id = int.Parse(dt.Rows[i]["Id"].ToString());
                item.Nome = dt.Rows[i]["Nome"].ToString();
                item.Saldo = double.Parse(dt.Rows[i]["Saldo"].ToString());
                item.Usuario_Id = int.Parse(dt.Rows[i]["Usuario_Id"].ToString());
                lista.Add(item);
            }

            return lista;
        }
    }
}
