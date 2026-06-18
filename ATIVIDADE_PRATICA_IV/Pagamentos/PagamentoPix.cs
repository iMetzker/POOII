using PlataformaCursos.Enums;

namespace PlataformaCursos.Pagamentos;

public class PagamentoPix : Pagamento
{
    public string ChavePix { get; private set; } = string.Empty;
    public string CodigoTransacao { get; private set; } = string.Empty;

    public override FormaPagamento Forma => FormaPagamento.Pix;

    private PagamentoPix() { }

    public PagamentoPix(string chavePix)
    {
        if (string.IsNullOrWhiteSpace(chavePix))
            throw new ArgumentException("A chave Pix é obrigatória.", nameof(chavePix));
        ChavePix = chavePix.Trim();
    }

    protected override bool ExecutarTransacao(decimal valor)
    {
        CodigoTransacao = $"PIX-{Guid.NewGuid().ToString("N")[..10].ToUpper()}";
        Console.WriteLine($"  > Pix gerado para a chave '{ChavePix}' — transação {CodigoTransacao}");
        return true;
    }

    public override string GerarComprovante()
    {
        var baseComprovante = base.GerarComprovante();
        return Aprovado ? $"{baseComprovante} | Transação {CodigoTransacao}" : baseComprovante;
    }
}
