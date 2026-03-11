using System;

class Program
{
    static void Main()
    {
        Console.Write("Digite o número inicial: ");
        int start_num = int.Parse(Console.ReadLine());

        Console.Write("Digite o número final: ");
        int end_num = int.Parse(Console.ReadLine());

        if (start_num < end_num)
        {
            for (int i = start_num; i <= end_num; i++)
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