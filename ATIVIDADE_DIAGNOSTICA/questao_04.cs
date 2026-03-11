using System;

class Program
{
    static void Main()
    {
        string[] students = new string[5];
        double[] grades = new double[5];

        int option = 0;
        int count = 0;

        while (option != 4)
        {
            Console.WriteLine("\n1 - Cadastrar aluno");
            Console.WriteLine("2 - Listar alunos");
            Console.WriteLine("3 - Mostrar média das notas");
            Console.WriteLine("4 - Sair");
            Console.Write("Escolha uma opção: ");

            option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:

                    if (count < 5)
                    {
                        Console.Write("Nome do aluno: ");
                        students[count] = Console.ReadLine();

                        Console.Write("Nota: ");
                        grades[count] = double.Parse(Console.ReadLine());

                        count++;
                    }
                    else
                    {
                        Console.WriteLine("Número máximo de alunos atingido, limite atual de 5.");
                    }

                    break;

                case 2:

                    for (int i = 0; i < count; i++)
                    {
                        Console.WriteLine(students[i] + " - Nota: " + grades[i]);
                    }

                    break;

                case 3:

                    double sum = 0;

                    for (int i = 0; i < count; i++)
                    {
                        sum += grades[i];
                    }

                    if (count > 0)
                        Console.WriteLine("Média: " + (sum / count));
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