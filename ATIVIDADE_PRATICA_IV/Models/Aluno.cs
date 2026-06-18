namespace PlataformaCursos.Models;

public class Aluno
{
    public int Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;

    private readonly List<Matricula> _matriculas = new();
    public IReadOnlyList<Matricula> Matriculas => _matriculas.AsReadOnly();

    private Aluno() { }

    public Aluno(string nome, string email)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome do aluno é obrigatório.", nameof(nome));
        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new ArgumentException("E-mail inválido.", nameof(email));

        Nome = nome.Trim();
        Email = email.Trim();
    }

    public override string ToString() => $"[{Id}] {Nome} <{Email}> — {_matriculas.Count} matrícula(s)";
}
