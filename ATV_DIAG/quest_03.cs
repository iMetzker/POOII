using System;

class Program
{
    static void Main()
    {
        int[] numeros = new int[10];
        Console.Write("ARMAZENANDO 10 NÚMEROS E MOSTRANDO O MAIOR\n");

        for (int i = 0; i < 10; i++)
        {
            Console.Write("Digite um número: ");
            numeros[i] = int.Parse(Console.ReadLine());
        }

        int maior = numeros[0];

        for (int i = 1; i < 10; i++)
        {
            if (numeros[i] > maior)
            {
                maior = numeros[i];
            }
        }

        Console.WriteLine("Maior número digitado: " + maior);
    }
}