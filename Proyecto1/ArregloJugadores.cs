using System;
//Arreglo dinamico que guarda los jugadores para poder pasarlos al monticulo
public class ArregloJugadores
{
    public Jugador[] datos; //Arreglo donde guarda los jugadores
    public int Cantidad { get; private set; } // Cantidad de jugadores almacenados
    private int capacidad; // Tamaño del arreglo

    public ArregloJugadores(int capacidadInicial = 10)
    {
        capacidad = capacidadInicial;
        datos = new Jugador[capacidad];
        Cantidad = 0;
    }

    public void Agregar(Jugador j)
    {
        // Si el arreglo se llena se crea uno del doble de tamaño
        if (Cantidad == capacidad)
        {
            ExpandirArreglo();
        }
        
        datos[Cantidad] = j;
        Cantidad++;
    }

    private void ExpandirArreglo()
    {
        capacidad = capacidad * 2;
        Jugador[] nuevoArreglo = new Jugador[capacidad];
        
        // Se copian los datos al nuevo arreglo 
        for (int i = 0; i < Cantidad; i++)
        {
            nuevoArreglo[i] = datos[i];
        }
        
        datos = nuevoArreglo; // Reemplaza el arreglo viejo con el nuevo
    }
}