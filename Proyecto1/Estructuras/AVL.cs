//Estructura para ordenar todos los jugadores segun la categoria 

using System;

namespace Proyecto1_EDD2
{
    public class ArbolAVL
    {
        public NodoAVL Raiz { get; private set; }
        private Categoria categoriaActual;

        public ArbolAVL(Categoria categoria)
        {
            Raiz = null;
            categoriaActual = categoria;
        }

        // Obtiene el valor segun la categoria que se use

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

        // Compara dos jugadores primero por estadística y si son las mismas estadisticas por nombre 
        private int Comparar(Jugador a, Jugador b)
        {
            int valA = ObtenerValor(a);
            int valB = ObtenerValor(b);

            if (valA != valB)
            {
                return valA.CompareTo(valB); // < 0 si A < B; > 0 si A > B
            }

            // Lo ordena alfabéticamente si tienen la misma estadística
            return string.Compare(a.Nombre, b.Nombre, StringComparison.OrdinalIgnoreCase);
        }

        // Metodos de altura y balanceo 

        private int Altura(NodoAVL nodo) => nodo == null ? 0 : nodo.Altura;

        private int FactorBalance(NodoAVL nodo)
        {
            return nodo == null ? 0 : Altura(nodo.Izquierdo) - Altura(nodo.Derecho);
        }

        private void ActualizarAltura(NodoAVL nodo)
        {
            nodo.Altura = 1 + Math.Max(Altura(nodo.Izquierdo), Altura(nodo.Derecho));
        }

        // Metodos de rotaciones para balancear

        // Rotación simple a la derecha caso Izquierda-Izquierda
        private NodoAVL RotarDerecha(NodoAVL y)
        {
            NodoAVL x = y.Izquierdo;
            NodoAVL subarbol = x.Derecho;

            // Reasignar punteros
            x.Derecho = y;
            y.Izquierdo = subarbol;

            // Actualizar alturas 
            ActualizarAltura(y);
            ActualizarAltura(x);

            return x;
        }

        // Rotación simple a la izquierda caso Derecha-Derecha
        private NodoAVL RotarIzquierda(NodoAVL x)
        {
            NodoAVL y = x.Derecho;
            NodoAVL subarbol = y.Izquierdo;

            // Reasignar punteros
            y.Izquierdo = x;
            x.Derecho = subarbol;

            // Actualizar alturas
            ActualizarAltura(x);
            ActualizarAltura(y);

            return y;
        }

        //Inserta jugador y balancea el arbol si es necesario
        public void Insertar(Jugador jugador)
        {
            Raiz = InsertarRecursivo(Raiz, jugador);
        }

        private NodoAVL InsertarRecursivo(NodoAVL nodo, Jugador jugador)
        {
        
            if (nodo == null)
                return new NodoAVL(jugador);

            int comparacion = Comparar(jugador, nodo.Jugador);

            if (comparacion < 0)
                nodo.Izquierdo = InsertarRecursivo(nodo.Izquierdo, jugador);
            else if (comparacion > 0)
                nodo.Derecho = InsertarRecursivo(nodo.Derecho, jugador);
            else
                return nodo; // Elemento exactamente identico

            //Actualizar altura del nodo actual
            ActualizarAltura(nodo);

            //Obtener factor de balance
            int balance = FactorBalance(nodo);

            // Casos de rotación para restaurar el balance

            // Desbalance Izquierda-Izquierda (LL)
            if (balance > 1 && Comparar(jugador, nodo.Izquierdo.Jugador) < 0)
                return RotarDerecha(nodo);

            // Desbalance Derecha-Derecha (RR)
            if (balance < -1 && Comparar(jugador, nodo.Derecho.Jugador) > 0)
                return RotarIzquierda(nodo);

            // Desbalance Izquierda-Derecha (LR)
            if (balance > 1 && Comparar(jugador, nodo.Izquierdo.Jugador) > 0)
            {
                nodo.Izquierdo = RotarIzquierda(nodo.Izquierdo);
                return RotarDerecha(nodo);
            }

            // Desbalance Derecha-Izquierda (RL)
            if (balance < -1 && Comparar(jugador, nodo.Derecho.Jugador) < 0)
            {
                nodo.Derecho = RotarDerecha(nodo.Derecho);
                return RotarIzquierda(nodo);
            }

            return nodo;
        }

        // Recorrido Inorden

        //Si es true lo hace de mayor a menor y si es false lo hace de menor a mayor
        public void MostrarOrdenado(bool descendente = true)
        {
            if (Raiz == null)
            {
                Console.WriteLine("No hay jugadores cargados en el CSV.");
                return;
            }

            InOrderRecursivo(Raiz, descendente);
        }

        private void InOrderRecursivo(NodoAVL nodo, bool descendente)
        {
            if (nodo == null) return;

            if (descendente)
            {
                // Mayor a menor
                InOrderRecursivo(nodo.Derecho, descendente);
                nodo.Jugador.Imprimir();
                InOrderRecursivo(nodo.Izquierdo, descendente);
            }
            else
            {
                // Menor a mayor
                InOrderRecursivo(nodo.Izquierdo, descendente);
                nodo.Jugador.Imprimir();
                InOrderRecursivo(nodo.Derecho, descendente);
            }
        }
    }
}