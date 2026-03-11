using System;

class Program
{
    static void Main()
    {
        Console.Write("Digite o número inicial: ");
        int num_ini = int.Parse(Console.ReadLine());

        Console.Write("Digite o número final: ");
        int num_limite = int.Parse(Console.ReadLine());

        if (num_ini < num_limite)
        {
            for (int i = num_ini; i <= num_limite; i++)
            {
                Console.WriteLine(i);
            }
        }
        else
        {
            Console.WriteLine("O número inicial deve ser menor que o número final, número digitado inválido.");
        }
    }
}