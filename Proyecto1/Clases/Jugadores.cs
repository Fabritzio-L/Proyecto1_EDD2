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
        Console.WriteLine($" {Nombre,-18} | {Seleccion,-12} | {Posicion,-13} | Gls: {Goles,-2} | Asi: {Asistencias,-2} | Min: {MinutosJugados,-4} | Tar: {Tarjetas,-2}");
        
    }
}