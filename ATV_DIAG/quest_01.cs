using System;

class Program
{
    static void Main()
    {
        string continuar = "S";

        while (continuar.ToUpper() == "S")
        {
            Console.WriteLine("===== MENU =====");
            Console.WriteLine("1 - Somar dois números");
            Console.WriteLine("2 - Subtrair dois números");
            Console.WriteLine("3 - Multiplicar dois números");
            Console.WriteLine("4 - Dividir dois números");
            Console.Write("Escolha uma opção: ");

            int opcao = int.Parse(Console.ReadLine());

            Console.Write("Digite o primeiro número: ");
            double num1 = double.Parse(Console.ReadLine());

            Console.Write("Digite o segundo número: ");
            double num2 = double.Parse(Console.ReadLine());

            double resultado = 0;

            switch (opcao)
            {
                case 1:
                    resultado = num1 + num2;
                    break;

                case 2:
                    resultado = num1 - num2;
                    break;

                case 3:
                    resultado = num1 * num2;
                    break;

                case 4:
                    if (num2 != 0)
                        resultado = num1 / num2;
                    else
                    {
                        Console.WriteLine("Nenhum número pode ser divido por 0.");
                        continue;
                    }
                    break;

                default:
                    Console.WriteLine("Opção Selecionada Inválida.");
                    continue;
            }

            resultado = Math.Round(resultado, 2);
            Console.WriteLine("Resultado: " + resultado);

            do
            {
                Console.Write("\nDeseja fazer outra operação? (S/N): ");
                continuar = Console.ReadLine().ToUpper();

                if (continuar != "S" && continuar != "N")
                {
                    Console.WriteLine("Resposta inválida.");
                }

            } while (continuar != "S" && continuar != "N");

            Console.WriteLine();
        }

        Console.WriteLine("Execução da Calculadora Encerrada.");
    }
}