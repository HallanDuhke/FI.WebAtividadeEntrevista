using FI.AtividadeEntrevista.BLL.Validators;
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

            // Normaliza e valida CPF via utilitário compartilhado
            if (!CpfValidator.IsValid(item.CPF, out var normalized))
                throw new ArgumentException("CPF do beneficiário inválido");
            item.CPF = normalized;

            bool existe = dao.ExistePorCpf(item.IdCliente, item.CPF, isUpdate ? item.Id : (long?)null);
            if (existe) throw new ArgumentException("Já existe beneficiário com este CPF para o cliente");
            if (string.IsNullOrWhiteSpace(item.Nome)) throw new ArgumentException("Nome do beneficiário é obrigatório");
        }
    }
}
