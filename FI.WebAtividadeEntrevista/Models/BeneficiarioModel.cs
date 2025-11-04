using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FI.WebAtividadeEntrevista.Models
{
    /// <summary>
    /// Classe de Modelo do Beneficiario    
    /// </summary>
    public class BeneficiarioModel
    {
            /// <summary>
            /// Id
            /// </summary>
            public long Id { get; set; }

            /// <summary>
            /// Id do Cliente
            /// </summary>
            [Required]
            public long IdCliente { get; set; }

            /// <summary>
            /// Nome
            /// </summary>
            [Required]
            public string Nome { get; set; }

            /// <summary>
            /// CPF
            /// </summary>
            [Required]
            public string CPF { get; set; }
        
    }
}