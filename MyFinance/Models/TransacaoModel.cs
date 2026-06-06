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

        internal void Insert()
        {
            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = "";
            if (Id == 0)
            {
                sql = $"insert into transacao(DATA,TIPO,VALOR,DESCRICAO,CONTA_ID,PLANO_CONTAS_ID, USUARIO_ID) " +
                    $"VALUES ('{DateTime.Parse(Data).ToString("yyyy/MM/dd")}','{Tipo}',{Valor},'{Descricao}'," +
                    $"{Conta_Id},{PlanoConta_Id},{id_usuario_logado})";
            }
            else
            {
                sql = $"UPDATE TRANSACAO SET DATA = '{DateTime.Parse(Data).ToString("yyyy/MM/dd")}', TIPO = '{Tipo}', " +
                    $"VALOR = {Valor}, DESCRICAO = '{Descricao}',CONTA_ID = {Conta_Id}, PLANO_CONTAS_ID={PlanoConta_Id} " +
                    $"WHERE ID = '{Id}'";
            }

            DAL objDAL = new DAL();
            objDAL.ExecutarComandoSQL(sql);
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

        public TransacaoModel CarregarRegistro(int? id)
        {
            TransacaoModel item = new TransacaoModel();

            string sql = $"select t.Id,t.Data,t.Tipo,t.Valor,t.Descricao as Historico,t.Conta_Id,c.Nome as Nome_Conta,t.Plano_Contas_Id," +
                $"p.Descricao as Plano_Contas,t.Usuario_Id from transacao as t inner join conta c on t.Conta_Id = c.Id " +
                $"inner join plano_contas p on t.Plano_Contas_Id = p.Id " +
                $"where t.Id = {id} ";

            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            item.Id = int.Parse(dt.Rows[0]["ID"].ToString());
            item.Data = DateTime.Parse(dt.Rows[0]["Data"].ToString()).ToString("dd/MM/yyyy");
            item.Tipo = dt.Rows[0]["Tipo"].ToString();
            item.Descricao = dt.Rows[0]["Historico"].ToString();
            item.Valor = double.Parse(dt.Rows[0]["Valor"].ToString());
            item.Conta_Id = int.Parse(dt.Rows[0]["Conta_Id"].ToString());
            item.NomeConta = dt.Rows[0]["Nome_Conta"].ToString();
            item.PlanoConta_Id = int.Parse(dt.Rows[0]["Plano_Contas_Id"].ToString());
            item.PlanoConta = dt.Rows[0]["Plano_Contas"].ToString();
            item.Usuario_Id = int.Parse(dt.Rows[0]["Usuario_Id"].ToString());

            return item;
        }
    }
}




