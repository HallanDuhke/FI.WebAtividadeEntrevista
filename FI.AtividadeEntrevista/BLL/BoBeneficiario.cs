using FI.AtividadeEntrevista.DAL.Beneficiarios;
using FI.AtividadeEntrevista.DML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        private readonly DaoBeneficiario dao = new DaoBeneficiario();

        public long Incluir(Beneficiario item)
        {
            Validar(item, isUpdate: false);
            return dao.Incluir(item);
        }

        public void Alterar(Beneficiario item)
        {
            Validar(item, isUpdate: true);
            dao.Alterar(item);
        }

        public void Excluir(long id)
        {
            dao.Excluir(id);
        }

        public Beneficiario Consultar(long id)
        {
            return dao.Consultar(id);
        }

        public IEnumerable<Beneficiario> ListarPorCliente(long idCliente)
        {
            return dao.ListarPorCliente(idCliente);
        }

        private void Validar(Beneficiario item, bool isUpdate)
        {
            if (item == null) throw new ArgumentException("Beneficiário inválido");
            if (item.IdCliente <= 0) throw new ArgumentException("Cliente inválido para beneficiário");

            string cpfNum = SomenteNumeros(item.CPF);
            if (!CpfValido(cpfNum)) throw new ArgumentException("CPF do beneficiário inválido");
            item.CPF = cpfNum;

            bool existe = dao.ExistePorCpf(item.IdCliente, item.CPF, isUpdate ? item.Id : (long?)null);
            if (existe) throw new ArgumentException("Já existe beneficiário com este CPF para o cliente");
            if (string.IsNullOrWhiteSpace(item.Nome)) throw new ArgumentException("Nome do beneficiário é obrigatório");
        }

        private static string SomenteNumeros(string s) => new string((s ?? "").Where(char.IsDigit).ToArray());

        // Validação de CPF (dígitos verificadores)
        private static bool CpfValido(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return false;
            cpf = SomenteNumeros(cpf);
            if (cpf.Length != 11) return false;
            if (new string(cpf[0], 11) == cpf) return false;

            int[] mult1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mult2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string temp = cpf.Substring(0, 9);
            int soma = 0;
            for (int i = 0; i < 9; i++) soma += (temp[i] - '0') * mult1[i];
            int resto = soma % 11;
            int dig1 = resto < 2 ? 0 : 11 - resto;

            temp += dig1.ToString();
            soma = 0;
            for (int i = 0; i < 10; i++) soma += (temp[i] - '0') * mult2[i];
            resto = soma % 11;
            int dig2 = resto < 2 ? 0 : 11 - resto;

            return cpf.EndsWith($"{dig1}{dig2}");
        }
    }
}
