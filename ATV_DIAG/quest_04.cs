using System;

class Program
{
    static void Main()
    {
        string[] alunos = new string[5];
        double[] notas = new double[5];

        int opcao = 0;
        int contador = 0;

        while (opcao != 4)
        {
            Console.WriteLine("\n1 - Cadastrar aluno");
            Console.WriteLine("2 - Listar alunos");
            Console.WriteLine("3 - Mostrar média das notas");
            Console.WriteLine("4 - Sair");
            Console.Write("Escolha uma opção: ");

            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:

                    if (contador < 5)
                    {
                        Console.Write("Nome do aluno: ");
                        alunos[contador] = Console.ReadLine();

                        Console.Write("Nota: ");
                        notas[contador] = double.Parse(Console.ReadLine());

                        contador++;
                    }
                    else
                    {
                        Console.WriteLine("Limite de alunos atingido.");
                    }

                    break;

                case 2:

                    for (int i = 0; i < contador; i++)
                    {
                        Console.WriteLine(alunos[i] + " - Nota: " + notas[i]);
                    }

                    break;

                case 3:

                    double soma = 0;

                    for (int i = 0; i < contador; i++)
                    {
                        soma += notas[i];
                    }

                    if (contador > 0)
                        Console.WriteLine("Média: " + (soma / contador));
                    else
                        Console.WriteLine("Nenhum aluno cadastrado.");

                    break;

                case 4:

                    Console.WriteLine("Você Saiu.");
                    break;

                default:

                    Console.WriteLine("Opção inválida, tente executar o programa novamente.");
                    break;
            }
        }
    }
}