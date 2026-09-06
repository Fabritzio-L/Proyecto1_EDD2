using System;

namespace Proyecto1_EDD2
{
    public class NodoBPlus
    {
        public int Grado { get; set; }
        public bool EsHoja { get; set; }
        public int CantidadClaves { get; set; } // Cuántos espacios están ocupados actualmente

        // Claves de búsqueda: Usaremos el Nombre del jugador como llave principal (string)
        public string[] Claves { get; set; }

        // Punteros hacia los subárboles 
        // Un nodo tiene máximo "Grado" de hijos
        public NodoBPlus[] Hijos { get; set; }

        // Aquí se guardan los objetos reales
        // Máximo "Grado - 1" de datos
        public Jugador[] Jugadores { get; set; }

        //Punteros para el recorrido de los nodos hoja con una lista doblemente enlazada
        public NodoBPlus Siguiente { get; set; }
        public NodoBPlus Anterior { get; set; }

        // Constructor
        public NodoBPlus(int grado, bool esHoja)
        {
            Grado = grado;
            EsHoja = esHoja;
            CantidadClaves = 0;

            // La capacidad máxima de claves y datos es Grado - 1
            Claves = new string[grado - 1];

            if (esHoja)
            {
                // Si es hoja se prepara el espacio para guardar a los jugadores y el puntero
                Jugadores = new Jugador[grado - 1];
                Anterior = null;
                Siguiente = null;
            }
            else
            {
                // Si es nodo interno se prepara el espacio para los punteros a sus hijos
                Hijos = new NodoBPlus[grado];
            }
        }
    }
}