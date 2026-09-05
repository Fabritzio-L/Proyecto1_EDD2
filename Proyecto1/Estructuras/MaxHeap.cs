using System;
//Para hacer referencia a lo que se quiera comparar en el Heap
public enum Categoria
{
    Goles,
    Asistencias,
    MinutosJugados,
    PartidosDisputados,
    Tarjetas

}

namespace Proyecto1_EDD2
{
    public class MaxHeap
    {
        private ArregloJugadores arreglo;
        private Categoria categoriaActual;

        //Se pide la categoria al construir el heap
        public MaxHeap(Categoria categoria, int capacidadInicial = 10)
        {
            // Uso del arreglo jugadores
            arreglo = new ArregloJugadores(capacidadInicial);
            categoriaActual = categoria;
        }


        //Metodo para obtener el valor segun la categoria que usa el heap
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
            arreglo.Agregar(nuevoJugador);      // Lo inserta al final del arreglo
            bubbleUp(arreglo.Cantidad - 1);       // Lo coloca en la posicion correcta
        }

        //Acomoda al heap de abajo hacia arriba comparando con el padre y subiendo si es mayor
        private void bubbleUp(int indice)
        {
            int indicePadre = (indice - 1) / 2;

            while (indice > 0 && ObtenerValor(arreglo.datos[indice]) > ObtenerValor(arreglo.datos[indicePadre]))
            {
                Intercambiar(indice, indicePadre);
                indice = indicePadre;
                indicePadre = (indice - 1) / 2;
            }
        }

        public Jugador ExtraerMaximo()
        {
            if (arreglo.Cantidad == 0)
                return null;

            // Se toma a la raíz
            Jugador maximo = arreglo.datos[0];

            // Coloca al ultimo elemento como la nueva raiz y reduce la cantidad de elementos
            arreglo.datos[0] = arreglo.datos[arreglo.Cantidad - 1];
            arreglo.ReducirCantidad();

            // Reacomoda la nueva raíz hacia abajo
            siftDown(0, arreglo.Cantidad);

            return maximo;
        }

        private void siftDown(int indice, int cantidadActual)
        {
            int indiceMayor = indice;
            int hijoIzquierdo = (2 * indice) + 1;
            int hijoDerecho = (2 * indice) + 2;

            // Comparar con hijo izquierdo usando ObtenerValor
            if (hijoIzquierdo < cantidadActual && 
                ObtenerValor(arreglo.datos[hijoIzquierdo]) > ObtenerValor(arreglo.datos[indiceMayor]))
            {
                indiceMayor = hijoIzquierdo;
            }

            // Comparar con hijo derecho usando ObtenerValor
            if (hijoDerecho < cantidadActual && 
                ObtenerValor(arreglo.datos[hijoDerecho]) > ObtenerValor(arreglo.datos[indiceMayor]))
            {
                indiceMayor = hijoDerecho;
            }

            // Si alguno de los hijos fue mayor que el padre actual se intercambian y continua el descenso
            if (indiceMayor != indice)
            {
                Intercambiar(indice, indiceMayor);
                siftDown(indiceMayor, cantidadActual);
            }
        }

    }
}