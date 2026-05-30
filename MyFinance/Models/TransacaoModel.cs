using Microsoft.AspNetCore.Http;
using MyFinance.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class TransacaoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A Data é obrigatória")]
        public string Data { get; set; }       

        public string Tipo { get; set; }

        [Required(ErrorMessage = "O Valor é obrigatório")]
        public double Valor { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória")]
        public string Descricao { get; set; }
        
        public int Conta_Id { get; set; }

        public string NomeConta { get; set; }

        public int PlanoConta_Id { get; set; }

        public string PlanoConta { get; set; }

        public int Usuario_Id { get; set; }

        public IHttpContextAccessor HttpContextAccessor { get; set; }


        public TransacaoModel()
        {

        }

        public TransacaoModel(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public List<TransacaoModel> ListaTransacoes()
        {
            List<TransacaoModel> lista = new List<TransacaoModel>();
            TransacaoModel item;
            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");

            string sql = $"select t.Id,t.Data,t.Tipo,t.Valor,t.Descricao as Historico,t.Conta_Id,c.Nome as Nome_Conta,t.Plano_Contas_Id," +
                $"p.Descricao as Plano_Contas,t.Usuario_Id from transacao as t inner join conta c on t.Conta_Id = c.Id " +
                $"inner join plano_contas p on t.Plano_Contas_Id = p.Id " +
                $"where t.Usuario_Id = {id_usuario_logado} " +
                $"order by t.Data desc limit 20";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new TransacaoModel();
                item.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                item.Data = DateTime.Parse(dt.Rows[i]["Data"].ToString()).ToString("dd/MM/yyyy");
                item.Tipo = dt.Rows[i]["Tipo"].ToString();
                item.Descricao = dt.Rows[i]["Historico"].ToString();
                item.Valor = double.Parse(dt.Rows[i]["Valor"].ToString());
                item.Conta_Id = int.Parse(dt.Rows[i]["Conta_Id"].ToString());
                item.NomeConta = dt.Rows[i]["Nome_Conta"].ToString();
                item.PlanoConta_Id = int.Parse(dt.Rows[i]["Plano_Contas_Id"].ToString());
                item.PlanoConta = dt.Rows[i]["Plano_Contas"].ToString();
                item.Usuario_Id = int.Parse(dt.Rows[i]["Usuario_Id"].ToString());

                lista.Add(item);
            }
            return lista;
        }
    }
}




