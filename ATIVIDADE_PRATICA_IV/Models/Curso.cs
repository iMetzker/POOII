namespace PlataformaCursos.Models;

public abstract class Curso
{
    public int Id { get; private set; }
    public string Titulo { get; private set; } = string.Empty;

    private readonly List<Aula> _aulas = new();
    public IReadOnlyList<Aula> Aulas => _aulas.AsReadOnly();

    public int TotalAulas => _aulas.Count;

    protected Curso() { }

    protected Curso(string titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("O título do curso é obrigatório.", nameof(titulo));

        Titulo = titulo.Trim();
    }

    public void AdicionarAula(Aula aula)
    {
        ArgumentNullException.ThrowIfNull(aula);
        _aulas.Add(aula);
    }

    public abstract bool EhPago { get; }

    public abstract decimal Valor { get; }

    public abstract string TipoCurso { get; }

    public override string ToString()
        => $"[{Id}] {Titulo} — {TipoCurso} ({TotalAulas} aula(s))"
           + (EhPago ? $" — R$ {Valor:F2}" : "");
}
