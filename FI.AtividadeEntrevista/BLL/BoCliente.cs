using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoCliente
    {
        private readonly DAL.DaoCliente _dao = new DAL.DaoCliente();

        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public long Incluir(DML.Cliente cliente)
        {
            ValidarCPF(cliente.CPF);
            if (_dao.ExisteCPF(cliente.CPF))
                throw new ArgumentException("CPF já cadastrado.");

            return _dao.Incluir(cliente);
        }

        /// <summary>
        /// Altera um cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public void Alterar(DML.Cliente cliente)
        {
            ValidarCPF(cliente.CPF);
            if (_dao.ExisteCPFOutro(cliente.CPF, cliente.Id))
                throw new ArgumentException("CPF já cadastrado para outro cliente.");

            _dao.Alterar(cliente);
        }

        /// <summary>
        /// Consulta o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public DML.Cliente Consultar(long id)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Consultar(id);
        }

        /// <summary>
        /// Excluir o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            cli.Excluir(id);
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<DML.Cliente> Listar()
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Listar();
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<DML.Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Pesquisa(iniciarEm,  quantidade, campoOrdenacao, crescente, out qtd);
        }

        /// <summary>
        /// VerificaExistencia
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        public bool VerificarExistencia(string CPF)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.VerificarExistencia(CPF);
        }

        private void ValidarCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                throw new ArgumentException("CPF é obrigatório.");

            var apenasDigitos = new string(cpf.Where(char.IsDigit).ToArray());
            if (!CpfValido(apenasDigitos))
                throw new ArgumentException("CPF inválido.");

        }

        private bool CpfValido(string digits)
        {
            if (digits?.Length != 11) return false;
            if (new string(digits[0], 11) == digits) return false;

            int[] mult1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mult2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma = 0;
            for (int i = 0; i < 9; i++) soma += (digits[i] - '0') * mult1[i];
            int resto = soma % 11;
            int dv1 = resto < 2 ? 0 : 11 - resto;

            soma = 0;
            for (int i = 0; i < 10; i++) soma += (digits[i] - '0') * mult2[i];
            resto = soma % 11;
            int dv2 = resto < 2 ? 0 : 11 - resto;

            return digits[9] - '0' == dv1 && digits[10] - '0' == dv2;
        }
    }
}
