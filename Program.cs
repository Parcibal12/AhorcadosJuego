using System;
using System.Collections.Generic;
using System.Linq;

class PalabraAhorcado
{
    public string Palabra { get; private set; }
    private HashSet<char> letrasAdivinadas = new HashSet<char>();

    public PalabraAhorcado(string palabra)
    {
        Palabra = palabra.ToUpper();
        RevelarLetrasIniciales();
    }

    private void RevelarLetrasIniciales()
    {
        if (Palabra.Length >= 2)
        {
            letrasAdivinadas.Add(Palabra[0]);
            letrasAdivinadas.Add(Palabra[Palabra.Length - 1]);
        }
    }

    public string ObtenerPalabraOculta()
    {
        string oculta = "";
        foreach (char letra in Palabra)
        {
            oculta += letrasAdivinadas.Contains(letra) ? letra + " " : "_ ";
        }
        return oculta.Trim();
    }

    public bool IntentarLetra(char letra)
    {
        letra = char.ToUpper(letra);
        if (Palabra.Contains(letra))
        {
            letrasAdivinadas.Add(letra);
            return true;
        }
        return false;
    }

    public bool PalabraAdivinada()
    {
        return Palabra.All(letra => letrasAdivinadas.Contains(letra));
    }
}

class Jugador
{
    public int IntentosRestantes { get; private set; }

    public Jugador(int intentos)
    {
        IntentosRestantes = intentos;
    }

    public void ReducirIntento()
    {
        IntentosRestantes--;
    }

    public bool TieneIntentos()
    {
        return IntentosRestantes > 0;
    }
}

class JuegoAhorcado
{
    private List<PalabraAhorcado> palabras;
    private Jugador jugador;

    public JuegoAhorcado()
    {
        palabras = new List<PalabraAhorcado>
        {
            new PalabraAhorcado("ELEFANTE"),
            new PalabraAhorcado("COMPUTADORA"),
            new PalabraAhorcado("GUITARRA"),
            new PalabraAhorcado("LUNA")
        };
        jugador = new Jugador(6);
    }

    public void Iniciar()
    {
        Console.WriteLine("¡Bienvenido al juego de ahorcado!");

        foreach (var palabra in palabras)
        {
            Console.WriteLine($"\nAdivina la palabra: {palabra.ObtenerPalabraOculta()}");

            while (!palabra.PalabraAdivinada() && jugador.TieneIntentos())
            {
                Console.Write("\nIngresa una letra: ");
                char intento = Console.ReadKey().KeyChar;
                Console.WriteLine();

                if (!palabra.IntentarLetra(intento))
                {
                    jugador.ReducirIntento();
                    Console.WriteLine($"Letra incorrecta. Intentos restantes: {jugador.IntentosRestantes}");
                }
                else
                {
                    Console.WriteLine($"¡Bien hecho! Progreso: {palabra.ObtenerPalabraOculta()}");
                }

                if (!jugador.TieneIntentos())
                {
                    Console.WriteLine("\n¡Te quedaste sin intentos! Fin del juego.");
                    return;
                }
            }

            Console.WriteLine($"¡Felicidades! Adivinaste la palabra: {palabra.Palabra}\n");
        }

        Console.WriteLine("¡Ganaste el juego! Todas las palabras fueron adivinadas.");
    }
}

class Program
{
    static void Main()
    {
        JuegoAhorcado juego = new JuegoAhorcado();
        juego.Iniciar();
    }
}
