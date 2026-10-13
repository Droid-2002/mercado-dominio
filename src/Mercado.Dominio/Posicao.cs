namespace Mercado.Dominio;

public sealed record Posicao(Ticker Ticker, int Quantidade, decimal PrecoMedio)
{
    public static Posicao Vazia(Ticker ticker) => new(ticker, 0, 0m);

    public (Posicao Posicao, decimal PnlRealizado) Aplicar(Negocio negocio)
    {
        ArgumentNullException.ThrowIfNull(negocio);

        if (negocio.Ticker != Ticker)
            throw new ArgumentException(
                $"Negócio de {negocio.Ticker} não pode ser aplicado na posição de {Ticker}.",
                nameof(negocio));

        return negocio.Lado switch
        {
            Lado.Compra => AplicarCompra(negocio),
            Lado.Venda => AplicarVenda(negocio),
            _ => throw new ArgumentOutOfRangeException(nameof(negocio), negocio.Lado, "Lado desconhecido.")
        };
    }

    private (Posicao, decimal) AplicarCompra(Negocio compra)
    {
        var novaQuantidade = Quantidade + compra.Quantidade;
        var novoPrecoMedio = (Quantidade * PrecoMedio + compra.ValorFinanceiro) / novaQuantidade;

        return (this with { Quantidade = novaQuantidade, PrecoMedio = novoPrecoMedio }, 0m);
    }

    private (Posicao, decimal) AplicarVenda(Negocio venda)
    {
        if (venda.Quantidade > Quantidade)
            throw new InvalidOperationException(
                $"Venda de {venda.Quantidade} {Ticker} maior que a posição de {Quantidade}.");

        var pnlRealizado = venda.Quantidade * (venda.Preco - PrecoMedio);
        var novaQuantidade = Quantidade - venda.Quantidade;
        var novoPrecoMedio = novaQuantidade == 0 ? 0m : PrecoMedio;

        return (this with { Quantidade = novaQuantidade, PrecoMedio = novoPrecoMedio }, pnlRealizado);
    }

    public decimal PnlNaoRealizado(decimal precoFechamento) =>
        Quantidade * (precoFechamento - PrecoMedio);
}