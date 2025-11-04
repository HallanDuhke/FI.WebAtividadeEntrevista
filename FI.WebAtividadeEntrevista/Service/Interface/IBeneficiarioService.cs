using FI.WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.WebAtividadeEntrevista.Service.Interface
{
    public interface IBeneficiarioService
    {
        IEnumerable<BeneficiarioModel> Listar(long idCliente);
        BeneficiarioModel Incluir(BeneficiarioModel model);
        void Alterar(BeneficiarioModel model);
        void Excluir(long id);
    }
}
