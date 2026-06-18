using PlataformaCursos.Enums;
using PlataformaCursos.Interfaces;

namespace PlataformaCursos.Pagamentos;

public abstract class Pagamento : IPagamento
{
    public int Id { get; private set; }
    public decimal Valor { get; protected set; }
    public DateTime? DataPagamento { get; private set; }
    public bool Aprovado { get; private set; }

    public abstract FormaPagamento Forma { get; }

    protected Pagamento() { }

    public bool Processar(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor do pagamento deve ser maior que zero.", nameof(valor));

        Valor = valor;
        Aprovado = ExecutarTransacao(valor);
        if (Aprovado)
            DataPagamento = DateTime.Now;

        return Aprovado;
    }

    protected abstract bool ExecutarTransacao(decimal valor);

    public virtual string GerarComprovante()
    {
        if (!Aprovado)
            return $"Pagamento via {Forma} NÃO aprovado.";

        return $"Comprovante — {Forma} | R$ {Valor:F2} | {DataPagamento:dd/MM/yyyy HH:mm} | APROVADO";
    }
}
