using System.Text.RegularExpressions;

namespace Mercado.Dominio;

public sealed record Ticker
{

    public string Valor { get; }

    public Ticker (String valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new ArgumentException("O ticker não pode ser nulo ou vazio.", nameof(valor));

        if (!Regex.IsMatch(valor, @"^[A-Z]{3,5}$"))
            throw new ArgumentException("O ticker deve conter apenas letras maiúsculas e ter entre 3 e 5 caracteres.", nameof(valor));

        Valor = valor;
    }
     public override string ToString() => Valor;
    
}
