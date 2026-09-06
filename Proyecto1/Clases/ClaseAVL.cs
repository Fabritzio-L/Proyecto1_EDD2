using System;

namespace Proyecto1_EDD2
{
    public class NodoAVL
    {
        public Jugador Jugador { get; set; }
        public NodoAVL Izquierdo { get; set; }
        public NodoAVL Derecho { get; set; }
        public int Altura { get; set; }

        public NodoAVL(Jugador jugador)
        {
            Jugador = jugador;
            Izquierdo = null;
            Derecho = null;
            Altura = 1;
        }
    }
}