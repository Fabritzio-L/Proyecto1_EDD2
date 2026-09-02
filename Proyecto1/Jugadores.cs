using System;

// 1. Clase base con los datos de un jugador
public class Jugador
{
    public string Nombre { get; set; }
    public string Seleccion { get; set; }
    public string Posicion { get; set; }
    public int PromedioMinutosJugados { get; set; }
    public int Goles { get; set; }
    public int Asistencias { get; set; }
    public int Tarjetas { get; set; }
    public int PartidosDisputados { get; set; }

    // Constructor vacio
    public Jugador() { }

    // Constructor con datos
    public Jugador(string nombre, string seleccion, string posicion, int minutos, int goles, int asistencias, int tarjetas, int partidos)
    {
        Nombre = nombre;
        Seleccion = seleccion;
        Posicion = posicion;
        PromedioMinutosJugados = minutos;
        Goles = goles;
        Asistencias = asistencias;
        Tarjetas = tarjetas;
        PartidosDisputados = partidos;
    }


    public void Imprimir()
    {
        Console.WriteLine($"{Nombre}|{Seleccion}|{Posicion}| Goles: {Goles}|Asistencias: {Asistencias}|Tarjetas: {Tarjetas}| Min: {PromedioMinutosJugados}");
    }
}