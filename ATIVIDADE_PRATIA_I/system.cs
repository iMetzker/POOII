using System;
using System.Collections.Generic;
using System.Linq;

class Program
{

    static List<string> nomes = new List<string>();
    static List<string> grupos = new List<string>();
    static List<double> cargas = new List<double>();
    static List<int> repeticoes = new List<int>();

    static void Main()
    {
        int opcao;
        do
        {
            ExibirMenu();
            if (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.WriteLine("Opção inválida! Digite um número.");
                continue;
            }

            switch (opcao)
            {
                case 1: AdicionarExercicio(); break;
                case 2: ListarExercicios(); break;
                case 3: BuscarPorNome(); break;
                case 4: FiltrarPorGrupo(); break;
                case 5: CalcularCargaTotal(); break;
                case 6: ExibirMaisPesado(); break;
                case 7: RemoverExercicio(); break;
                case 0: Console.WriteLine("Saindo..."); break;
                default: Console.WriteLine("Opção inexistente."); break;
            }
            
            if (opcao != 0) { Console.WriteLine("\nPressione qualquer tecla para continuar..."); try { Console.ReadKey(); } catch (InvalidOperationException) { } }

        } while (opcao != 0);
    }

    static void ExibirMenu()
    {
        try
        {
            Console.Clear();
        }
        catch (IOException)
        {
            // Console.Clear() não suportado, ignorar
        }
        Console.WriteLine("### CADASTRO DE TREINOS ###");
        Console.WriteLine("1 - Adicionar exercício");
        Console.WriteLine("2 - Listar todos os exercícios");
        Console.WriteLine("3 - Buscar exercício por nome");
        Console.WriteLine("4 - Filtrar por grupo muscular");
        Console.WriteLine("5 - Calcular carga total do treino");
        Console.WriteLine("6 - Exibir exercício mais pesado");
        Console.WriteLine("7 - Remover exercício");
        Console.WriteLine("0 - Sair");
        Console.Write("Escolha uma das opções: ");
    }

    static void AdicionarExercicio()
    {
        string nome;
        do {
            Console.Write("Nome do exercício: ");
            nome = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(nome));

        Console.Write("Grupo muscular: ");
        string grupo = Console.ReadLine();

        Console.Write("Carga (kg): ");
        double carga;
        while (!double.TryParse(Console.ReadLine(), out carga) || carga < 0)
        {
            Console.Write("Carga inválida (>=0). Digite novamente: ");
        }

        Console.Write("Repetições: ");
        int reps;
        while (!int.TryParse(Console.ReadLine(), out reps) || reps < 1)
        {
            Console.Write("Repetições inválidas (>=1). Digite novamente: ");
        }

        nomes.Add(nome);
        grupos.Add(grupo);
        cargas.Add(carga);
        repeticoes.Add(reps);
        Console.WriteLine("Exercício adicionado com sucesso");
    }

    static void ListarExercicios()
    {
        if (nomes.Count == 0) { Console.WriteLine("Nenhum exercício cadastrado."); return; }

        for (int i = 0; i < nomes.Count; i++)
            Console.WriteLine($"{nomes[i]} - {grupos[i]} - {cargas[i]}kg - {repeticoes[i]} reps");
    }

    static void BuscarPorNome()
    {
        Console.Write("Digite o nome para busca: ");
        string busca = Console.ReadLine();
        

        int index = nomes.FindIndex(n => n.Equals(busca, StringComparison.OrdinalIgnoreCase));

        if (index != -1)
            Console.WriteLine($"Encontrado: {nomes[index]} | Grupo: {grupos[index]} | Carga: {cargas[index]}kg");
        else
            Console.WriteLine("Exercício não encontrado 🔎");
    }

    static void FiltrarPorGrupo()
    {
        Console.Write("Filtrar por qual grupo muscular? ");
        string grupoBusca = Console.ReadLine();

        var filtrados = nomes.Where((n, i) => grupos[i].Equals(grupoBusca, StringComparison.OrdinalIgnoreCase));

        if (filtrados.Any())
            foreach (var item in filtrados) Console.WriteLine($"- {item}");
        else
            Console.WriteLine("Nenhum exercício para este grupo.");
    }

    static void CalcularCargaTotal()
    {
        double total = cargas.Sum();
        Console.WriteLine($"Carga total do treino: {total} kg");
    }

    static void ExibirMaisPesado()
    {
        if (cargas.Count == 0) return;

        double maxCarga = cargas.Max();
        int index = cargas.IndexOf(maxCarga);

        Console.WriteLine($"Mais pesado: {nomes[index]} com {maxCarga} kg");
    }

    static void RemoverExercicio()
    {
        Console.Write("Nome do exercício a remover: ");
        string nomeRemover = Console.ReadLine();

        int index = nomes.FindIndex(n => n.Equals(nomeRemover, StringComparison.OrdinalIgnoreCase));

        if (index != -1)
        {
            nomes.RemoveAt(index);
            grupos.RemoveAt(index);
            cargas.RemoveAt(index);
            repeticoes.RemoveAt(index);
            Console.WriteLine("Exercício deletado com sucesso ✔️");
        }
        else Console.WriteLine("Exercício não encontrado 🔎");
    }
}