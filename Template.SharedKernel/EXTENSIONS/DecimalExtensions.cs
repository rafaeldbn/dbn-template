using System;
using System.Globalization;

namespace $ext_safeprojectname$.SharedKernel.Extensions
{
    public static class DecimalExtensions
    {
        public static string GetBrValue(this decimal valor)
        {
            var culture = new CultureInfo("pt-BR");

            var (valorFormatado, sufixo) = GetValorEhSufixo(valor);
            return $"{valorFormatado.ToString(culture)} {sufixo}";
        }

        private static (decimal valor, string sufixo) GetValorEhSufixo(decimal valor)
        {

            var (value, sufixo) = valor switch
            {
                >= 1000000000 => (valor / 1000000000m, "bi"),
                >= 1000000 => (valor / 1000000m, "mi"),
                >= 1000 => (valor / 1000m, "mil"),
                _ => (valor, string.Empty)
            };

            value = Math.Round(value, 2);

            return (value, sufixo);
        }
    }
}
