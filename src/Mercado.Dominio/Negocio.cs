namespace Mercado.Dominio;

public sealed record Negocio
{
    public Ticker Ticker { get; }
    public Lado Lado { get; }
    public int Quantidade { get; }
    public decimal Preco { get; }
    public DateOnly DataPregao { get; }

    public decimal ValorFinanceiro => Quantidade * Preco;

    public Negocio(Ticker ticker, Lado lado, int quantidade, decimal preco, DateOnly dataPregao)
    {
        ArgumentNullException.ThrowIfNull(ticker);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantidade);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(preco);

        Ticker = ticker;
        Lado = lado;
        Quantidade = quantidade;
        Preco = preco;
        DataPregao = dataPregao;
    }
}