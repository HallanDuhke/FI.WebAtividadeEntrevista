using System.Collections.Generic;
using FI.AtividadeEntrevista.DML;
using FI.WebAtividadeEntrevista.Models;
using WebAtividadeEntrevista.Models;

namespace FI.WebAtividadeEntrevista.Service.Interface
{
    public interface IClienteService
    {
        Cliente Incluir(ClienteModel model);
        void Alterar(ClienteModel model);
        void Excluir(long id);
        Cliente Consultar(long id);
        IList<Cliente> Pesquisa(int startIndex, int pageSize, string campo, bool asc, out int qtd);
    }
}