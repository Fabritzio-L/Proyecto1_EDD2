using System;

namespace Proyecto1_EDD2
{
    public class MinHeap
    {
        private ArregloJugadores arreglo;
        private Categoria categoriaActual;

        public MinHeap(Categoria categoria, int capacidadInicial = 10)
        {
            arreglo = new ArregloJugadores(capacidadInicial);
            categoriaActual = categoria;
        }

        private int ObtenerValor(Jugador j)
        {
            switch (categoriaActual)
            {
                case Categoria.Goles: return j.Goles;
                case Categoria.Asistencias: return j.Asistencias;
                case Categoria.MinutosJugados: return j.MinutosJugados;
                case Categoria.PartidosDisputados: return j.PartidosDisputados;
                case Categoria.Tarjetas: return j.Tarjetas;
                default: return 0;
            }
        }

        private void Intercambiar(int indiceA, int indiceB)
        {
            Jugador temporal = arreglo.datos[indiceA];
            arreglo.datos[indiceA] = arreglo.datos[indiceB];
            arreglo.datos[indiceB] = temporal;
        }

        public int Cantidad => arreglo.Cantidad;

        public void Insertar(Jugador nuevoJugador)
        {
            arreglo.Agregar(nuevoJugador);
            bubbleUp(arreglo.Cantidad - 1);
        }

        //Misma lógica que el max heap con la diferencia que sube si el hijo es menor que el padre
        private void bubbleUp(int indice)
        {
            int indicePadre = (indice - 1) / 2;

            while (indice > 0 && ObtenerValor(arreglo.datos[indice]) < ObtenerValor(arreglo.datos[indicePadre]))
            {
                Intercambiar(indice, indicePadre);
                indice = indicePadre;
                indicePadre = (indice - 1) / 2;
            }
        }


        //Extrae al minimo 
        public Jugador ExtraerMinimo()
        {
            if (arreglo.Cantidad == 0)
                return null;

            Jugador minimo = arreglo.datos[0];

            arreglo.datos[0] = arreglo.datos[arreglo.Cantidad - 1];
            arreglo.ReducirCantidad();

            siftDown(0, arreglo.Cantidad);

            return minimo;
        }

        // Desciende buscando al hijo con el menor valor y lo intercambia con el padre si es menor
        private void siftDown(int indice, int cantidadActual)
        {
            int indiceMenor = indice;
            int hijoIzquierdo = (2 * indice) + 1;
            int hijoDerecho = (2 * indice) + 2;
        //Compara hijo izquierdo con el padre y si es menor lo intercambia
            if (hijoIzquierdo < cantidadActual && 
                ObtenerValor(arreglo.datos[hijoIzquierdo]) < ObtenerValor(arreglo.datos[indiceMenor]))
            {
                indiceMenor = hijoIzquierdo;
            }

        //Compara hijo derecho con el padre y si es menor lo intercambia
            if (hijoDerecho < cantidadActual && 
                ObtenerValor(arreglo.datos[hijoDerecho]) < ObtenerValor(arreglo.datos[indiceMenor]))
            {
                indiceMenor = hijoDerecho;
            }

        // Si alguno de los hijos fue menor que el padre actual se intercambian y continua el descenso

            if (indiceMenor != indice)
            {
                Intercambiar(indice, indiceMenor);
                siftDown(indiceMenor, cantidadActual);
            }
        }
    }
}