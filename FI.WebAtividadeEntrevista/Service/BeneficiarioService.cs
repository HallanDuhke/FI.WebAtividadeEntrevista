using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using FI.WebAtividadeEntrevista.Models;
using FI.WebAtividadeEntrevista.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FI.WebAtividadeEntrevista.Service
{
    public class BeneficiarioService : IBeneficiarioService
    {
        private readonly BoBeneficiario _bo;

        public BeneficiarioService(BoBeneficiario bo)
        {
            _bo = bo;
        }

        public IEnumerable<BeneficiarioModel> Listar(long idCliente)
        {
            return _bo.ListarPorCliente(idCliente).Select(ToModel);
        }

        public BeneficiarioModel Incluir(BeneficiarioModel model)
        {
            var entity = ToEntity(model);
            var id = _bo.Incluir(entity);

            return new BeneficiarioModel
            {
                Id = id,
                CPF = FormatarCpf(model.CPF),
                Nome = model.Nome,
                IdCliente = model.IdCliente
            };
        }

        public void Alterar(BeneficiarioModel model)
        {
            _bo.Alterar(ToEntity(model));
        }

        public void Excluir(long id)
        {
            _bo.Excluir(id);
        }

        private static BeneficiarioModel ToModel(Beneficiario dml)
        {
            return new BeneficiarioModel
            {
                Id = dml.Id,
                CPF = FormatarCpf(dml.CPF),
                Nome = dml.Nome,
                IdCliente = dml.IdCliente
            };
        }

        private static Beneficiario ToEntity(BeneficiarioModel model)
        {
            return new Beneficiario
            {
                Id = model.Id,
                CPF = SomenteDigitos(model.CPF),
                Nome = model.Nome,
                IdCliente = model.IdCliente
            };
        }

        private static string SomenteDigitos(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return string.Empty;
            return new string(cpf.Where(char.IsDigit).ToArray());
        }

        private static string FormatarCpf(string cpf)
        {
            var d = SomenteDigitos(cpf);
            if (d.Length != 11) return cpf ?? string.Empty;
            return $"{d.Substring(0, 3)}.{d.Substring(3, 3)}.{d.Substring(6, 3)}-{d.Substring(9, 2)}";
        }
    }
}