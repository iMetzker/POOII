namespace PlataformaCursos.Models;

public class Aula
{
    public int Id { get; private set; }
    public string Titulo { get; private set; } = string.Empty;
    public int DuracaoMinutos { get; private set; }

    public int CursoId { get; private set; }
    public Curso? Curso { get; private set; }

    private Aula() { }

    public Aula(string titulo, int duracaoMinutos)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("O título da aula é obrigatório.", nameof(titulo));
        if (duracaoMinutos <= 0)
            throw new ArgumentException("A duração deve ser maior que zero.", nameof(duracaoMinutos));

        Titulo = titulo.Trim();
        DuracaoMinutos = duracaoMinutos;
    }

    public override string ToString() => $"Aula [{Id}] {Titulo} ({DuracaoMinutos} min)";
}
