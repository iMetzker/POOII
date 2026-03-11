using System;

class Program
{
    static void Main()
    {
        string continueOption = "S";

        while (continueOption.ToUpper() == "S")
        {
            Console.WriteLine("===== MENU =====");
            Console.WriteLine("1 - Somar dois números");
            Console.WriteLine("2 - Subtrair dois números");
            Console.WriteLine("3 - Multiplicar dois números");
            Console.WriteLine("4 - Dividir dois números");
            Console.Write("Escolha uma opção: ");

            int option = int.Parse(Console.ReadLine());

            Console.Write("Digite o primeiro número: ");
            double num1 = double.Parse(Console.ReadLine());

            Console.Write("Digite o segundo número: ");
            double num2 = double.Parse(Console.ReadLine());

            double result = 0;

            switch (option)
            {
                case 1:
                    result = num1 + num2;
                    break;

                case 2:
                    result = num1 - num2;
                    break;

                case 3:
                    result = num1 * num2;
                    break;

                case 4:
                    if (num2 != 0)
                        result = num1 / num2;
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

            result = Math.Round(result, 2);
            Console.WriteLine("Resultado: " + result);

            do
            {
                Console.Write("\nDeseja fazer outra operação? (S/N): ");
                continueOption = Console.ReadLine().ToUpper();

                if (continueOption != "S" && continueOption != "N")
                {
                    Console.WriteLine("Resposta inválida.");
                }

            } while (continueOption != "S" && continueOption != "N");

            Console.WriteLine();
        }

        Console.WriteLine("Execução da Calculadora Encerrada.");
    }
}