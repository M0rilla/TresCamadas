using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSQL
{
    public abstract class Base : IPessoa
    {
        public Base(string nome, string telefone, string cpf)
        {
            this.Nome = nome;
            this.Telefone = telefone;
            this.CPF = cpf;
        }

        public Base() { }

        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string CPF { get; set; }

        public void SetNome(string nome) { this.Nome = nome; }
        public void SetTelefone(string telefone) { this.Telefone = telefone; }
        public void SetCPF(string cpf) { this.CPF = cpf; }

        public virtual void Gravar() 
        {
            /* Para realizarmos a conexão com o SQL Server criamos um novo arquivo com a extensão
             .udl e configuramos com base no provedor e conexão - SPN (endereço/nome server com autenticação kerberos), autenticação e db - desejada.
             Em seguida abrimos esse arquivo .udl com o bloco de notas e é retornada a string de conexão com o banco. */

            string connectionString = ConfigurationManager.AppSettings["SqlConnection"];
            using (SqlConnection connection = new SqlConnection(
               connectionString))
            {
                string queryString = "INSERT INTO "+ this.GetType().Name +"s (nome, telefone, cpf) VALUES ('"+ this.Nome +"','"+ this.Telefone +"','"+ this.CPF +"');";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
