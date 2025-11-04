using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL.Validators
{
    public static class CpfValidator
    {
        public static string SomenteNumeros(string s) => new string((s ?? "").Where(char.IsDigit).ToArray());

        public static bool IsValid(string cpf, out string normalized)
        {
            normalized = SomenteNumeros(cpf);
            if (string.IsNullOrWhiteSpace(normalized)) return false;
            if (normalized.Length != 11) return false;
            if (new string(normalized[0], 11) == normalized) return false;

            int[] mult1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mult2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma = 0;
            for (int i = 0; i < 9; i++) soma += (normalized[i] - '0') * mult1[i];
            int resto = soma % 11;
            int dv1 = resto < 2 ? 0 : 11 - resto;

            soma = 0;
            for (int i = 0; i < 10; i++) soma += (normalized[i] - '0') * mult2[i];
            resto = soma % 11;
            int dv2 = resto < 2 ? 0 : 11 - resto;

            return normalized[9] - '0' == dv1 && normalized[10] - '0' == dv2;
        }

        public static void EnsureValidOrThrow(string cpf, out string normalized, string label = "CPF")
        {
            if (string.IsNullOrWhiteSpace(cpf))
                throw new ArgumentException($"{label} é obrigatório.");
            if (!IsValid(cpf, out normalized))
                throw new ArgumentException($"{label} inválido.");
        }
    }
}
