using MyFinance.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="O nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage ="O email é obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage ="A senha é obrigatória")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        public string DataNascimento { get; set; }




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
                    DataNascimento = dataTable.Rows[0]["DATA_NASCIMENTO"].ToString();
                    return true;
                }
            }
            return false;
        }

        public void RegistrarUsuario()
        {
            string dataNascimento = DateTime.Parse(DataNascimento).ToString("yyyy/MM/dd");
            string sql = $"INSERT INTO USUARIO(NOME,EMAIL,SENHA,DATA_NASCIMENTO)VALUES('{Nome}','{Email}', '{Senha}', '{dataNascimento}')";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandoSQL(sql);
        }
    }
}
