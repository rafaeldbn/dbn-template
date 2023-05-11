using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace $ext_safeprojectname$.SharedKernel.Extensions;

public static class StringExtensions
{
    public static string ToSnakeCase(this string input)
    {
        if (string.IsNullOrEmpty(input)) { return input; }

        var startUnderscores = Regex.Match(input, @"^_+");
        return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
    }

    public static DateTime? ToDateWithFormats(this string value, params string[] formats)
    {
        if (string.IsNullOrWhiteSpace(value)) return null;
        var culture = CultureInfo.CreateSpecificCulture("pt-BR");

        foreach (var format in formats)
        {
            var success = DateTime.TryParseExact(value, format, culture, DateTimeStyles.None, out DateTime data);
            if (success) return data;
        }

        return null;
    }

    public static string Normalizar(this string text)
    {
        if (string.IsNullOrEmpty(text)) return string.Empty;

        return new string(text
            .Normalize(NormalizationForm.FormD)
            .Where(ch => char.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
            .ToArray()).Trim().ToUpper();
    }
}
