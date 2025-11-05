using FI.AtividadeEntrevista.DML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.DAL.Beneficiarios
{
    /// <summary>
    /// Classe de acesso a dados de Beneficiario
    /// </summary>
    internal class DaoBeneficiario : AcessoDados
    {
        /// <summary>
        /// Inclui um novo Beneficiário
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal long Incluir(Beneficiario item)
        {
            var parametros = new List<System.Data.SqlClient.SqlParameter>
            {
                new System.Data.SqlClient.SqlParameter("CPF", item.CPF),
                new System.Data.SqlClient.SqlParameter("NOME", item.Nome),
                new System.Data.SqlClient.SqlParameter("IDCLIENTE", item.IdCliente)
            };

            DataSet ds = base.Consultar("FI_SP_IncBeneficiario", parametros);
            return Convert.ToInt64(ds.Tables[0].Rows[0]["ID"]);
        }

        internal void Alterar(Beneficiario item)
        {
            var parametros = new List<System.Data.SqlClient.SqlParameter>
            {
                new System.Data.SqlClient.SqlParameter("ID", item.Id),
                new System.Data.SqlClient.SqlParameter("CPF", item.CPF),
                new System.Data.SqlClient.SqlParameter("NOME", item.Nome)
            };

            base.Executar("FI_SP_AltBeneficiario", parametros);
        }

        internal void Excluir(long id)
        {
            var parametros = new List<System.Data.SqlClient.SqlParameter>
            {
                new System.Data.SqlClient.SqlParameter("ID", id)
            };

            base.Executar("FI_SP_DelBeneficiario", parametros);
        }

        internal Beneficiario Consultar(long id)
        {
            var parametros = new List<System.Data.SqlClient.SqlParameter>
            {
                new System.Data.SqlClient.SqlParameter("ID", id)
            };

            DataSet ds = base.Consultar("FI_SP_ConsBeneficiario", parametros);
            if (ds.Tables[0].Rows.Count == 0) return null;

            var row = ds.Tables[0].Rows[0];
            return new Beneficiario
            {
                Id = Convert.ToInt64(row["ID"]),
                CPF = Convert.ToString(row["CPF"]),
                Nome = Convert.ToString(row["NOME"]),
                IdCliente = Convert.ToInt64(row["IDCLIENTE"])
            };
        }

        internal IEnumerable<Beneficiario> ListarPorCliente(long idCliente)
        {
            var parametros = new List<System.Data.SqlClient.SqlParameter>
            {
                new System.Data.SqlClient.SqlParameter("IDCLIENTE", idCliente),
                new System.Data.SqlClient.SqlParameter("CPF", (object)DBNull.Value)
            };

            DataSet ds = base.Consultar("FI_SP_PesqBeneficiario", parametros);
            return from DataRow row in ds.Tables[0].Rows
                   select new Beneficiario
                   {
                       Id = Convert.ToInt64(row["ID"]),
                       CPF = Convert.ToString(row["CPF"]),
                       Nome = Convert.ToString(row["NOME"]),
                       IdCliente = Convert.ToInt64(row["IDCLIENTE"])
                   };
        }

        internal bool ExistePorCpf(long idCliente, string cpf, long? idIgnorar = null)
        {
            var parametros = new List<System.Data.SqlClient.SqlParameter>
            {
                new System.Data.SqlClient.SqlParameter("IDCLIENTE", idCliente),
                new System.Data.SqlClient.SqlParameter("CPF", cpf),
                new System.Data.SqlClient.SqlParameter("IDIGNORAR", (object)idIgnorar ?? DBNull.Value)
            };

            DataSet ds = base.Consultar("FI_SP_ExisteBeneficiarioPorCpf", parametros);
            return ds.Tables[0].Rows.Count > 0;
        }
    }
}
