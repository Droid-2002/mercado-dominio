using System.Text.RegularExpressions;

namespace Mercado.Dominio;

public sealed record Ticker
{
    public string Valor { get; }

    public Ticker(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new ArgumentException("O ticker não pode ser nulo ou vazio.", nameof(valor));

        var normalizado = valor.Trim().ToUpperInvariant();

        if (!Regex.IsMatch(normalizado, "^[A-Z]{4}[0-9]{1,2}$"))
            throw new ArgumentException(
                $"Ticker inválido: '{valor}'. Esperado: 4 letras e 1 ou 2 dígitos (ex.: PETR4, BOVA11).",
                nameof(valor));

        Valor = normalizado;
    }

    public override string ToString() => Valor;
}