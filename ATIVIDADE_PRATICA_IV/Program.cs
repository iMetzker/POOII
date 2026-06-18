using System.Text;
using PlataformaCursos.Data;
using PlataformaCursos.Models;
using PlataformaCursos.Pagamentos;
using PlataformaCursos.Services;

try { Console.OutputEncoding = Encoding.UTF8; } catch { }

using var db = new PlataformaContext();
Console.WriteLine("Conectando ao banco de dados...");
db.Database.EnsureCreated();

using var plataforma = new Plataforma(db);
SeedDados(plataforma);

Console.WriteLine("=== Plataforma de Cursos Online (SQL Server) ===");

bool executando = true;
while (executando)
{
    ExibirMenu();
    var opcao = Console.ReadLine();
    if (opcao is null) break;

    switch (opcao.Trim())
    {
        case "1": CadastrarAluno(); break;
        case "2": CadastrarCurso(); break;
        case "3": AdicionarAula(); break;
        case "4": MatricularAluno(); break;
        case "5": RegistrarProgresso(); break;
        case "6": ListarAlunos(); break;
        case "7": ListarCursos(); break;
        case "8": ListarMatriculas(); break;
        case "0": executando = false; break;
        default: Aviso("Opção inválida."); break;
    }
}

Console.WriteLine("\nEncerrando. Até logo!");

void ExibirMenu()
{
    Console.WriteLine(
        """

        ----------------------------------------
        1 - Cadastrar aluno
        2 - Cadastrar curso
        3 - Adicionar aula a um curso
        4 - Matricular aluno em curso
        5 - Registrar progresso (concluir aula)
        6 - Listar alunos
        7 - Listar cursos
        8 - Listar matrículas
        0 - Sair
        ----------------------------------------
        """);
    Console.Write("Escolha: ");
}

void CadastrarAluno()
{
    try
    {
        var nome = Ler("Nome do aluno");
        var email = Ler("E-mail");
        var aluno = plataforma.CadastrarAluno(nome, email);
        Sucesso($"Aluno cadastrado: {aluno}");
    }
    catch (Exception ex) { Aviso(ex.Message); }
}

void CadastrarCurso()
{
    try
    {
        var titulo = Ler("Título do curso");
        Console.Write("Tipo (1 - Gratuito, 2 - Pago): ");
        var tipo = Console.ReadLine()?.Trim();

        Curso curso = tipo switch
        {
            "1" => plataforma.CadastrarCurso(new CursoGratuito(titulo)),
            "2" => plataforma.CadastrarCurso(new CursoPago(titulo, LerDecimal("Valor (R$)"))),
            _ => throw new ArgumentException("Tipo de curso inválido.")
        };
        Sucesso($"Curso cadastrado: {curso}");
    }
    catch (Exception ex) { Aviso(ex.Message); }
}

void AdicionarAula()
{
    try
    {
        ListarCursos();
        var curso = plataforma.BuscarCurso(LerInt("ID do curso"))
                    ?? throw new ArgumentException("Curso não encontrado.");
        var titulo = Ler("Título da aula");
        var duracao = LerInt("Duração (min)");
        plataforma.AdicionarAula(curso, new Aula(titulo, duracao));
        Sucesso($"Aula adicionada. Curso agora tem {curso.TotalAulas} aula(s).");
    }
    catch (Exception ex) { Aviso(ex.Message); }
}

void MatricularAluno()
{
    try
    {
        ListarAlunos();
        var aluno = plataforma.BuscarAluno(LerInt("ID do aluno"))
                    ?? throw new ArgumentException("Aluno não encontrado.");
        ListarCursos();
        var curso = plataforma.BuscarCurso(LerInt("ID do curso"))
                    ?? throw new ArgumentException("Curso não encontrado.");

        Pagamento? pagamento = null;
        if (curso.EhPago)
        {
            Console.WriteLine($"Curso pago: R$ {curso.Valor:F2}");
            pagamento = ProcessarPagamento(curso.Valor);
            Console.WriteLine($"  {pagamento.GerarComprovante()}");
        }

        var matricula = plataforma.Matricular(aluno, curso, pagamento);
        Sucesso($"Matrícula realizada: {matricula}");
    }
    catch (Exception ex) { Aviso(ex.Message); }
}

Pagamento ProcessarPagamento(decimal valor)
{
    Console.Write("Forma de pagamento (1 - Cartão, 2 - Pix): ");
    var opcao = Console.ReadLine()?.Trim();

    Pagamento pagamento = opcao switch
    {
        "1" => new PagamentoCartao(Ler("Número do cartão"), LerInt("Parcelas (1-12)")),
        "2" => new PagamentoPix(Ler("Chave Pix")),
        _ => throw new ArgumentException("Forma de pagamento inválida.")
    };

    if (!pagamento.Processar(valor))
        throw new InvalidOperationException("Pagamento não aprovado.");

    return pagamento;
}

void RegistrarProgresso()
{
    try
    {
        ListarMatriculas();
        var matricula = plataforma.BuscarMatricula(LerInt("ID da matrícula"))
                        ?? throw new ArgumentException("Matrícula não encontrada.");

        Console.WriteLine($"Aulas de '{matricula.Curso.Titulo}':");
        foreach (var a in matricula.Curso.Aulas)
            Console.WriteLine($"  {a}");

        var aula = matricula.Curso.Aulas.FirstOrDefault(a => a.Id == LerInt("ID da aula concluída"))
                   ?? throw new ArgumentException("Aula não encontrada neste curso.");

        plataforma.ConcluirAula(matricula, aula);
        Sucesso($"Progresso atualizado: {matricula}");
    }
    catch (Exception ex) { Aviso(ex.Message); }
}

void ListarAlunos()
{
    Console.WriteLine("\n-- Alunos --");
    var alunos = plataforma.ListarAlunos();
    if (alunos.Count == 0) { Console.WriteLine("  (nenhum)"); return; }
    foreach (var a in alunos) Console.WriteLine($"  {a}");
}

void ListarCursos()
{
    Console.WriteLine("\n-- Cursos --");
    var cursos = plataforma.ListarCursos();
    if (cursos.Count == 0) { Console.WriteLine("  (nenhum)"); return; }
    foreach (var c in cursos) Console.WriteLine($"  {c}");
}

void ListarMatriculas()
{
    Console.WriteLine("\n-- Matrículas --");
    var matriculas = plataforma.ListarMatriculas();
    if (matriculas.Count == 0) { Console.WriteLine("  (nenhuma)"); return; }
    foreach (var m in matriculas) Console.WriteLine($"  {m}");
}

string Ler(string rotulo)
{
    Console.Write($"{rotulo}: ");
    return Console.ReadLine() ?? string.Empty;
}

int LerInt(string rotulo)
{
    Console.Write($"{rotulo}: ");
    return int.TryParse(Console.ReadLine(), out var v)
        ? v
        : throw new ArgumentException($"'{rotulo}' deve ser um número inteiro.");
}

decimal LerDecimal(string rotulo)
{
    Console.Write($"{rotulo}: ");
    return decimal.TryParse(Console.ReadLine(), out var v)
        ? v
        : throw new ArgumentException($"'{rotulo}' deve ser um número.");
}

void Sucesso(string msg)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"[OK] {msg}");
    Console.ResetColor();
}

void Aviso(string msg)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"[!] {msg}");
    Console.ResetColor();
}

void SeedDados(Plataforma p)
{
    if (p.ListarCursos().Count > 0 || p.ListarAlunos().Count > 0)
        return;

    var csharp = p.CadastrarCurso(new CursoPago("C# do Zero ao Avançado", 199.90m));
    p.AdicionarAula(csharp, new Aula("Introdução e ambiente", 20));
    p.AdicionarAula(csharp, new Aula("Tipos e variáveis", 35));
    p.AdicionarAula(csharp, new Aula("POO na prática", 50));

    var git = p.CadastrarCurso(new CursoGratuito("Git e GitHub Essencial"));
    p.AdicionarAula(git, new Aula("Commits e branches", 25));
    p.AdicionarAula(git, new Aula("Pull requests", 30));

    p.CadastrarAluno("Ana Souza", "ana@email.com");
    p.CadastrarAluno("Bruno Lima", "bruno@email.com");
}
