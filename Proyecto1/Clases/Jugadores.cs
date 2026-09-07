using System;

// 1. Clase base con los datos de un jugador
public class Jugador
{
    public string Nombre { get; set; }
    public string Seleccion { get; set; }
    public string Posicion { get; set; }
    public int MinutosJugados { get; set; }
    public int Goles { get; set; }
    public int Asistencias { get; set; }
    public int Tarjetas { get; set; }
    public int PartidosDisputados { get; set; }

    // Constructor vacio
    public Jugador() { }

    // Constructor con datos
    public Jugador(string nombre, string seleccion, string posicion, int goles, int asistencias, int minutos, int partidos, int tarjetas)
    {
        Nombre = nombre;
        Seleccion = seleccion;
        Posicion = posicion;
        Goles = goles;
        Asistencias = asistencias;
        MinutosJugados = minutos;
        PartidosDisputados = partidos;
        Tarjetas = tarjetas;
    }


    public void Imprimir()
    {
        Console.WriteLine($"Nombre: {Nombre}");
        Console.WriteLine($"Selección: {Seleccion}");
        Console.WriteLine($"Posición: {Posicion}");
        Console.WriteLine($"Goles: {Goles}");
        Console.WriteLine($"Asistencias: {Asistencias}");
        Console.WriteLine($"Minutos Jugados: {MinutosJugados}");
        Console.WriteLine($"Partidos Disputados: {PartidosDisputados}");
        Console.WriteLine($"Tarjetas: {Tarjetas}");
    }
}