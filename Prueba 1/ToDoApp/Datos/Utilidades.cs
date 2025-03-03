using System;

public static class Utilidades
{
    public static string? PedirDato(string mensaje)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine(mensaje);
            Console.WriteLine("(O ingrese 'X' para cancelar)");

            Console.Write("\n> ");
            string? input = Console.ReadLine()?.Trim();

            if (input?.ToUpper() == "X")
            {
                Console.WriteLine("⚠ Operación cancelada.");
                return null;
            }

            if (!string.IsNullOrWhiteSpace(input))
                return input;
        }
    }
}

