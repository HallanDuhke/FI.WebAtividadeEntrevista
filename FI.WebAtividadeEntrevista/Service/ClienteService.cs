using System;
using System.Collections.Generic;
using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using FI.WebAtividadeEntrevista.Models;
using FI.WebAtividadeEntrevista.Service.Interface;
using WebAtividadeEntrevista.Models;

namespace FI.WebAtividadeEntrevista.Service
{
    public class ClienteService : IClienteService
    {
        private readonly BoCliente _bo;

        public ClienteService(BoCliente bo)
        {
            _bo = bo ?? throw new ArgumentNullException(nameof(bo));
        }

        public Cliente Incluir(ClienteModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var entity = new Cliente
            {
                CEP = model.CEP,
                CPF = model.CPF,
                Cidade = model.Cidade,
                Email = model.Email,
                Estado = model.Estado,
                Logradouro = model.Logradouro,
                Nacionalidade = model.Nacionalidade,
                Nome = model.Nome,
                Sobrenome = model.Sobrenome,
                Telefone = model.Telefone
            };

            entity.Id = _bo.Incluir(entity);
            return entity;
        }

        public void Alterar(ClienteModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var entity = new Cliente
            {
                Id = model.Id,
                CEP = model.CEP,
                CPF = model.CPF,
                Cidade = model.Cidade,
                Email = model.Email,
                Estado = model.Estado,
                Logradouro = model.Logradouro,
                Nacionalidade = model.Nacionalidade,
                Nome = model.Nome,
                Sobrenome = model.Sobrenome,
                Telefone = model.Telefone
            };

            _bo.Alterar(entity);
        }

        public void Excluir(long id)
        {
            _bo.Excluir(id);
        }

        public Cliente Consultar(long id)
        {
            return _bo.Consultar(id);
        }

        public IList<Cliente> Pesquisa(int startIndex, int pageSize, string campo, bool asc, out int qtd)
        {
            return _bo.Pesquisa(startIndex, pageSize, campo, asc, out qtd);
        }
    }
}