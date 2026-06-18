using PlataformaCursos.Enums;

namespace PlataformaCursos.Pagamentos;

public class PagamentoCartao : Pagamento
{
    public string NumeroCartao { get; private set; } = string.Empty;
    public int Parcelas { get; private set; }

    public override FormaPagamento Forma => FormaPagamento.Cartao;

    private PagamentoCartao() { }

    public PagamentoCartao(string numeroCartao, int parcelas = 1)
    {
        if (string.IsNullOrWhiteSpace(numeroCartao) || numeroCartao.Replace(" ", "").Length < 13)
            throw new ArgumentException("Número de cartão inválido.", nameof(numeroCartao));
        if (parcelas is < 1 or > 12)
            throw new ArgumentException("Parcelas deve estar entre 1 e 12.", nameof(parcelas));

        NumeroCartao = numeroCartao.Replace(" ", "");
        Parcelas = parcelas;
    }

    protected override bool ExecutarTransacao(decimal valor)
    {
        Console.WriteLine($"  > Autorizando cartão final {NumeroCartao[^4..]} em {Parcelas}x...");
        return true;
    }

    public override string GerarComprovante()
    {
        var baseComprovante = base.GerarComprovante();
        if (!Aprovado) return baseComprovante;

        var valorParcela = Valor / Parcelas;
        return $"{baseComprovante} | {Parcelas}x de R$ {valorParcela:F2} | Cartão final {NumeroCartao[^4..]}";
    }
}
