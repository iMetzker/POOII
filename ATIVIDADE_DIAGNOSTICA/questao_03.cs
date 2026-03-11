using System;

class Program
{
    static void Main()
    {
        int[] numbers = new int[10];
        Console.Write("ARMAZENANDO 10 NÚMEROS E MOSTRANDO O MAIOR\n");

        for (int i = 0; i < 10; i++)
        {
            Console.Write("Digite um número: ");
            numbers[i] = int.Parse(Console.ReadLine());
        }

        int largest = numbers[0];

        for (int i = 1; i < 10; i++)
        {
            if (numbers[i] > largest)
            {
                largest = numbers[i];
            }
        }

        Console.WriteLine("Maior número digitado: " + largest);
    }
}