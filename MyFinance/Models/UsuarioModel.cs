using MyFinance.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }




        public bool ValidarLogin()
        {
            string sql = $"SELECT ID, NOME, DATA_NASCIMENTO FROM USUARIO WHERE EMAIL='{Email}' AND SENHA='{Senha}'";
            DAL objDAL = new DAL();
            DataTable dataTable = objDAL.RetDataTable(sql);

            if (dataTable != null)
            {
                if(dataTable.Rows.Count == 1)
                {
                    Id = int.Parse(dataTable.Rows[0]["ID"].ToString());
                    Nome = dataTable.Rows[0]["NOME"].ToString();
                    DataNascimento = DateTime.Parse(dataTable.Rows[0]["DATA_NASCIMENTO"].ToString());
                    return true;
                }
            }
            return false;
        }
    }
}
