using Mercado.Dominio;

namespace Mercado.Dominio.Testes;

public class PosicaoTests
{
    [Fact]
    public void Aplicar_CenarioDaQuestao8_CalculaPrecoMedioEPnlRealizado()
    {
        var petr4 = new Ticker("PETR4");
        var dia = new DateOnly(2026, 10, 13);
        var posicao = Posicao.Vazia(petr4);

        (posicao, _) = posicao.Aplicar(new Negocio(petr4, Lado.Compra, 100, 10.00m, dia));
        (posicao, _) = posicao.Aplicar(new Negocio(petr4, Lado.Compra, 100, 12.00m, dia));
        (posicao, var pnlRealizado) = posicao.Aplicar(new Negocio(petr4, Lado.Venda, 50, 13.00m, dia));

        Assert.Equal(150, posicao.Quantidade);
        Assert.Equal(11.00m, posicao.PrecoMedio);
        Assert.Equal(100.00m, pnlRealizado);
    }
}